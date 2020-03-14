using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Xunit;
using Amazon.Lambda.TestUtilities;
using Amazon.Lambda.SQSEvents;

using ApplicationLoan.Process;

namespace ApplicationLoan.Process.Tests
{
    public class FunctionTest
    {
        [Fact]
        public async Task TestSQSEventLambdaFunction()
        {
            var newRequestId = "Test-LoanApp-" + DateTime.Now.ToString("yyyyMMddHHmmssfffff");

            var message = new SQSEvent.SQSMessage
            {
                Body = "{\"IdLoanRequest\":\"18b3c39cba2341d38456438461edc514\",\"IdStatus\":\"3e63f0f13b8643c783d679f1081f854e\",\"Result\":null,\"RefusedPolicy\":null,\"VlAmout\":25000.0,\"IdTerms\":\"3b7c087db0fb4b97912e73c8db48c61b\",\"Terms\":null,\"Status\":null,\"LoanRequest\":{\"IdCustomer\":\"d6b37520f09c45c8a4eeea290652209c\",\"VlAmout\":25000.0,\"IdTerms\":\"3b7c087db0fb4b97912e73c8db48c61b\",\"VlIncome\":3500.0,\"Customer\":{\"CpfCnpj\":\"12345678901\",\"Name\":\"Everton\",\"BirthDate\":\"2000-03-14T14:36:53\",\"Id\":\"d6b37520f09c45c8a4eeea290652209c\",\"Modificado\":\"2020-03-14T11:37:26.6073554-03:00\",\"StatusRow\":\"U\",\"IdUserInsert\":0,\"IdUserUpdate\":-1},\"Terms\":{\"Term\":6,\"Id\":\"3b7c087db0fb4b97912e73c8db48c61b\",\"Modificado\":\"2020-03-12T13:58:36\",\"StatusRow\":\"I\",\"IdUserInsert\":-1,\"IdUserUpdate\":null},\"Id\":\"18b3c39cba2341d38456438461edc514\",\"Modificado\":\"2020-03-14T11:37:35.3043911-03:00\",\"StatusRow\":\"I\",\"IdUserInsert\":-1,\"IdUserUpdate\":null},\"Id\":\"40fdd44532894e31ac0fbd91fa2c80ab\",\"Modificado\":\"2020-03-14T11:37:36.1822393-03:00\",\"StatusRow\":\"I\",\"IdUserInsert\":-1,\"IdUserUpdate\":null}"
            };

            message.Attributes = new Dictionary<string, string>();
            //message.Attributes.Add("Id", newRequestId);
            message.Attributes.Add("Id", "40fdd44532894e31ac0fbd91fa2c80ab");

            var sqsEvent = new SQSEvent
            {
                Records = new List<SQSEvent.SQSMessage>
                {
                    message
                }
            };

            var logger = new TestLambdaLogger();
            var context = new TestLambdaContext
            {
                Logger = logger
            };

            var function = new Function();
            await function.FunctionHandler(sqsEvent, context);

            Assert.Contains("Finished", logger.Buffer.ToString());
        }
    }
}