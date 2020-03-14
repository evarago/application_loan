using Amazon.SQS;
using Amazon.SQS.Model;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLoan.Infra.CrossCutting.Helpers.Queue
{
    public class SQSHelpers
    {
        public Dictionary<string, string> attibutes { get; set; }

        public async Task<HttpStatusCode> SendMessage(string messageBody, string queue, int timeOutSec = 300)
        {
            try
            {
                var config = new AmazonSQSConfig();


                config.RegionEndpoint = Amazon.RegionEndpoint.SAEast1;
                config.Timeout = new TimeSpan(0, 0, timeOutSec);
                config.MaxErrorRetry = 4;

                var _client = new AmazonSQSClient("zzzz", "zzzzz", config);

                SendMessageRequest sendMessageRequest = new SendMessageRequest();
                sendMessageRequest.QueueUrl = queue;
                sendMessageRequest.MessageBody = messageBody;

                foreach (var attibute in attibutes)
                {
                    sendMessageRequest.MessageAttributes.Add(
                        attibute.Key,
                        new MessageAttributeValue { DataType = "String", StringValue = attibute.Value }
                    );
                }

                var retornoSQS = _client.SendMessageAsync(sendMessageRequest).Result;

                _client.Dispose();

                return await Task.FromResult(retornoSQS.HttpStatusCode);
            }
            catch (AmazonSQSException ex)
            {
                throw ex;
            }
        }
    }
}