using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Amazon.Lambda.Core;
using Amazon.Lambda.SQSEvents;
using ApplicationLoan.Domain.Entities;
using ApplicationLoan.Infra.CrossCutting.Helpers.Rest;
using ApplicationLoan.Service.Services;
using ApplicationLoan.Service.Validators;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;


// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]

namespace ApplicationLoan.Process
{
    public class Function
    {
        private BaseService<LoanProcess> loanProcessService = new BaseService<LoanProcess>();
        private BaseService<LoanRequest> loanRequestService = new BaseService<LoanRequest>();
        private BaseService<Customer> customerService = new BaseService<Customer>();
        private BaseService<Terms> termsService = new BaseService<Terms>();
        private BaseService<InterestRate> interestRateService = new BaseService<InterestRate>();

        //private readonly IConfiguration configuration = null;

        /// <summary>
        /// Default constructor. This constructor is used by Lambda to construct the instance. When invoked in a Lambda environment
        /// the AWS credentials will come from the IAM role associated with the function and the AWS region will be set to the
        /// region the Lambda function is executed in.
        /// </summary>
        public Function()
        {
            //configuration = Startup.Configuration;
        }


        /// <summary>
        /// This method is called for every Lambda invocation. This method takes in an SQS event object and can be used 
        /// to respond to SQS messages.
        /// </summary>
        /// <param name="evnt"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task FunctionHandler(SQSEvent evnt, ILambdaContext context)
        {
            foreach (var message in evnt.Records)
            {
                await ProcessMessageAsync(message, context);
            }
        }

        private async Task ProcessMessageAsync(SQSEvent.SQSMessage message, ILambdaContext context)
        {
            try
            {
                context.Logger.LogLine($"Processed message {message.Body}");

                var loanProcessId = string.Empty;

                if (message.MessageAttributes != null)
                    loanProcessId = (message.MessageAttributes.ContainsKey("Id") ? message.MessageAttributes["Id"].StringValue : string.Empty);
                else if (message.Attributes != null)
                    loanProcessId = (message.Attributes.ContainsKey("Id") ? message.Attributes["Id"].ToString() : string.Empty);

                if (string.IsNullOrEmpty(loanProcessId))
                {
                    context.Logger.LogLine($"LoanProcessId doens't included on request.");
                    return;
                }

                var loanProcessObjectRequest = JsonConvert.DeserializeObject<LoanProcess>(message.Body);

                #region Process object

                var loanProcessObject = new LoanProcess
                {
                    Id = loanProcessId,
                    IdLoanRequest = loanProcessObjectRequest.LoanRequest.Id,
                    IdStatus = Status.Completed,
                    Modificado = DateTime.Now,
                    StatusRow = "U", //Update
                    IdUserUpdate = -1,
                    VlAmout = loanProcessObjectRequest.VlAmout,
                    IdTerms = loanProcessObjectRequest.LoanRequest.Terms.Id
                };

                var vlTerm = Math.Round(loanProcessObject.VlAmout / loanProcessObjectRequest.LoanRequest.Terms.Term, 2);

                #region Age Policy

                if (DateTime.Now.Year - loanProcessObjectRequest.LoanRequest.Customer.BirthDate.Year < 18)
                    loanProcessObjectRequest.RefusedPolicy = "age";

                #endregion Age Policy

                var objRequestApiNoverde = JObject.FromObject(new { cpf = loanProcessObjectRequest.LoanRequest.Customer.CpfCnpj });

                #region Score Policy

                (bool restSuccessScore, string restMessageReturnScore, JObject resultSCore) = new RestHelpers().PostAsync<JObject, JObject>(RestApi.NOVERDE, "score", objRequestApiNoverde, TypeOfAuth.KEY);
                if (!restSuccessScore)
                    throw new Exception($"Score API: {restMessageReturnScore}");

                var iScore = 0;
                Int32.TryParse(resultSCore["score"].ToString(), out iScore);

                if (iScore < 600)
                    loanProcessObjectRequest.RefusedPolicy = (string.IsNullOrEmpty(loanProcessObjectRequest.RefusedPolicy) ? "score" : loanProcessObjectRequest.RefusedPolicy += ", score");

                #endregion Score Policy

                #region Commitment Policy

                (bool restSuccessCom, string restMessageReturnCom, JObject resultCommitment) = new RestHelpers().PostAsync<JObject, JObject>(RestApi.NOVERDE, "commitment", objRequestApiNoverde, TypeOfAuth.KEY);
                if (!restSuccessCom)
                    throw new Exception($"Commitment API: {restMessageReturnCom}");

                decimal iCommitment = 0;
                decimal.TryParse(resultCommitment["commitment"].ToString(), out iCommitment);

                var vlCommitment = Math.Round(loanProcessObjectRequest.LoanRequest.VlIncome * iCommitment, 2);
                var vlFree = Math.Round(loanProcessObjectRequest.LoanRequest.VlIncome - vlCommitment, 2);

                vlTerm = await CalcTermAsync(loanProcessObjectRequest.LoanRequest.Terms, loanProcessObjectRequest.LoanRequest.VlAmout, vlFree, iScore);

                if (vlTerm <= 0)
                    loanProcessObject.RefusedPolicy = (string.IsNullOrEmpty(loanProcessObjectRequest.RefusedPolicy) ? "commitment" : loanProcessObjectRequest.RefusedPolicy += ", commitment");

                if (!string.IsNullOrEmpty(loanProcessObject.RefusedPolicy))
                    loanProcessObject.Result = LoanProcess.refused;
                else
                    loanProcessObject.Result = LoanProcess.approved;

                loanProcessObject.IdTerms = loanProcessObjectRequest.LoanRequest.Terms.Id;
                loanProcessObject.IdStatus = Status.Completed;
                loanProcessObject.LoanRequest = loanProcessObjectRequest.LoanRequest;

                #endregion Commitment Policy

                loanProcessService.Put<LoanProcessValidator>(loanProcessObject);

                #endregion Process object

                context.Logger.LogLine($"Finished {message.Body}");

                // TODO: Do interesting work based on the new message
                await Task.CompletedTask;
            }
            catch (Exception ex)
            {
                context.Logger.LogLine($"[ERRO] Detalhe: {ex.ToString()}");
                throw ex;
            }
        }

        private async Task<decimal> RecalcTermAsync(Int32 iTerm, decimal vlAmout, decimal vlFree)
        {
            var lstTerms = await termsService.GetByFilterAsync(d => d.Term != iTerm);

            decimal vlTermRecalc = 0;

            foreach (var d in lstTerms.OrderBy(d => d.Term))
            {
                vlTermRecalc = Math.Round(vlAmout / d.Term, 2);

                if (vlTermRecalc < vlFree)
                    break;
                else
                    vlTermRecalc = 0;
            }

            return vlTermRecalc;
        }

        private async Task<decimal> CalcTermAsync(Terms terms, decimal vlAmout, decimal vlFree, int iScore)
        {
            var lstInterestRate = await interestRateService.GetByFilterAsync(d => d.Id == d.Id);
            var lstTerms = await termsService.GetByFilterAsync(d => d.Term >= terms.Term);

            decimal vlTermRecalc = 0;

            foreach (var d in lstTerms.OrderBy(d => d.Term))
            {
                var objInterestRate = lstInterestRate.Where(t => t.IdTerm == d.Id && iScore >= t.StartScore && iScore <= t.EndScore).FirstOrDefault();

                if (objInterestRate != null)
                {
                    vlTermRecalc = Math.Round(vlAmout / d.Term, 2);
                    vlTermRecalc += Math.Round(vlTermRecalc * (objInterestRate.VlInterest / 100), 2);

                    if (vlTermRecalc < vlFree)
                        break;
                    else
                        vlTermRecalc = 0;
                }
            }

            return vlTermRecalc;
        }
    }
}