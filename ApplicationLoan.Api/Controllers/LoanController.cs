using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApplicationLoan.Api.ViewModel;
using ApplicationLoan.Domain.Entities;
using ApplicationLoan.Infra.CrossCutting.Helpers.Queue;
using ApplicationLoan.Service.Services;
using ApplicationLoan.Service.Validators;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace ApplicationLoan.Api.Controllers
{
    /// <summary>
    /// Loan Demo API
    /// </summary>
    [Authorize]
    [Produces("application/json")]
    [Route("v{version:apiVersion}/[controller]")]
    public class LoanController : Controller
    {
        private LoanProcessService<LoanProcess> loanProcessService = new LoanProcessService<LoanProcess>();
        private BaseService<LoanRequest> loanRequestService = new BaseService<LoanRequest>();
        private BaseService<Customer> customerService = new BaseService<Customer>();
        private BaseService<Terms> termsService = new BaseService<Terms>();

        private readonly IConfiguration configuration = null;
        private ArrayList arrError = new ArrayList();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="config"></param>
        public LoanController(IConfiguration config)
        {
            configuration = config;
        }

        /// <summary>
        /// POST for Request
        /// </summary>
        /// <param name="requestObject"></param>
        /// <returns></returns>
        [HttpPost]
        [MapToApiVersion("1.0")]
        public async Task<IActionResult> Post([FromBody] LoanRequestViewModel requestObject)
        {
            try
            {
                if (requestObject == null)
                    arrError.Add("No data found!");

                if (string.IsNullOrEmpty(requestObject.name))
                    arrError.Add("Empty Name!");

                if (string.IsNullOrEmpty(requestObject.cpf))
                    arrError.Add("Empty CPF!");

                if (requestObject.terms <= 0)
                    arrError.Add("Invalid Terms!");

                if (requestObject.income <= 0)
                    arrError.Add("Invalid Income!");

                if (requestObject.birthDate == null || requestObject.birthDate == DateTime.MinValue)
                    arrError.Add("Invalid BirthDate!");

                var lstTerms = await termsService.GetByFilterAsync(t => t.Id == t.Id);
                var objTerms = (lstTerms.Where(t => t.Term == Convert.ToInt32(requestObject.terms)).FirstOrDefault() != null ?
                    lstTerms.Where(t => t.Term == Convert.ToInt32(requestObject.terms)).FirstOrDefault() : null);

                if (objTerms == null || string.IsNullOrEmpty(objTerms.Id))
                    arrError.Add($"Invalid Term. It can be: {string.Join(",", lstTerms.Select(t => t.Term))} !");

                if (arrError.Count > 0)
                    return BadRequest(new { errors = arrError });

                #region Customer object

                var cpfParam = requestObject.cpf.Replace(".", "").Replace("-", "").Replace("/", "").Replace(@"\", "");

                var objCustomer = new Customer
                {
                    CpfCnpj = cpfParam,
                    Name = requestObject.name,
                    BirthDate = Convert.ToDateTime(requestObject.birthDate.ToString()),
                    Modificado = DateTime.Now
                };

                var objCustomerExist = await customerService.GetByFilterAsync(c => c.CpfCnpj == cpfParam);
                if (objCustomerExist != null && objCustomerExist.ToList().Count > 0)
                {
                    objCustomer.Id = objCustomerExist.FirstOrDefault().Id;
                    objCustomer.StatusRow = "U"; //Update
                    objCustomer.IdUserUpdate = - 1; //System

                    customerService.Put<CustomerValidator>(objCustomer);
                }
                else
                {
                    objCustomer.Id = Customer.GetNewId();
                    objCustomer.StatusRow = "I"; //Insert
                    objCustomer.IdUserInsert = 1;

                    customerService.Post<CustomerValidator>(objCustomer);
                }

                #endregion Customer object

                var loanRequestId = LoanRequest.GetNewId();

                #region Request object

                var objLoanRequest = new LoanRequest
                {
                    Id = loanRequestId,
                    IdCustomer = objCustomer.Id,
                    VlAmout = Convert.ToDecimal(requestObject.amount),
                    VlIncome = Convert.ToDecimal(requestObject.income),
                    IdTerms = objTerms.Id,
                    Modificado = DateTime.Now,
                    StatusRow = "I", //Insert
                    IdUserInsert = -1, //System
                };

                loanRequestService.Post<LoanRequestValidator>(objLoanRequest);

                objLoanRequest.Customer = objCustomer;
                objLoanRequest.Terms = objTerms;

                #endregion Request object

                #region Process object

                var loanProcessId = LoanProcess.GetNewId();

                var objLoanProcess = new LoanProcess
                {
                    Id = loanProcessId,
                    IdLoanRequest = loanRequestId,
                    IdStatus = Status.Processing,
                    IdTerms = objTerms.Id,
                    VlAmout = Convert.ToDecimal(requestObject.amount),
                    Modificado = DateTime.Now,
                    StatusRow = "I", //Insert
                    IdUserInsert = -1, //System
                };

                loanProcessService.Post<LoanProcessValidator>(objLoanProcess);

                objLoanProcess.LoanRequest = objLoanRequest;

                #endregion Process object

                var sqsMessage = new SQSHelpers();
                sqsMessage.attibutes = new Dictionary<string, string> { { "Id", loanProcessId } };
                var sendMessageOk = await sqsMessage.SendMessage(JsonConvert.SerializeObject(objLoanProcess), configuration["ProcessQueue"].ToString());

                if (sendMessageOk != System.Net.HttpStatusCode.OK)
                    return BadRequest("Temporary error. Try again!");

                return new ObjectResult(new { Id = loanRequestId });
            }
            catch (ArgumentNullException ex)
            {
                arrError.Add(ex);
                return NotFound(new { errors = arrError });
            }
            catch (Exception ex)
            {
                arrError.Add(ex);
                return BadRequest(new { errors = arrError });
            }
        }

        /// <summary>
        /// GET for query process status
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [MapToApiVersion("1.0")]
        public async Task<IActionResult> Get(string id)
        {
            try
            {
                if (string.IsNullOrEmpty(id))
                    arrError.Add("No ID found!");

                if (arrError.Count > 0)
                    return BadRequest(new { errors = arrError });

                var responseLoanProcess = await loanProcessService.SearchByFilterAsync(p=>p.IdLoanRequest == id);

                if (responseLoanProcess == null || string.IsNullOrEmpty(responseLoanProcess.FirstOrDefault().IdLoanRequest))
                    return new ObjectResult($"There isn't document for ID: {id}");

                var responseViewModel = new LoanResponseViewModel
                {
                    id = responseLoanProcess.FirstOrDefault().LoanRequest.Id,
                    status = responseLoanProcess.FirstOrDefault().Status.Description,
                    result = responseLoanProcess.FirstOrDefault().Result,
                    refused_policy = responseLoanProcess.FirstOrDefault().RefusedPolicy,
                    amount = responseLoanProcess.FirstOrDefault().VlAmout,
                    terms = responseLoanProcess.FirstOrDefault().Terms.Term
                };

                return new ObjectResult(responseViewModel);
            }
            catch (ArgumentNullException ex)
            {
                arrError.Add(ex);
                return NotFound(new { errors = arrError });
            }
            catch (Exception ex)
            {
                arrError.Add(ex);
                return BadRequest(new { errors = arrError });
            }
        }
    }
}