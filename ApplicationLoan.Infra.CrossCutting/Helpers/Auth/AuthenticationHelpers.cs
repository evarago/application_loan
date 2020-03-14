using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace ApplicationLoan.Infra.CrossCutting.Helpers.Auth
{
    public class AuthenticationHelpers
    {
        public RetornoToken ObterAuthToken(string clientId, string clientSecret)
        {
            try
            {
                HttpClient httpClient = new HttpClient();

                System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;

                var byteArray = Encoding.ASCII.GetBytes($"{clientId}:{clientSecret}");
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));

                HttpResponseMessage response = new HttpResponseMessage();
                for (int i = 0; i < 5; i++)
                {
                    var content = new StringContent("Content-Type", Encoding.UTF8, "application/json");
                    string url = "https://sso.com/oauth/token";
                    response = httpClient.PostAsync(url, content).Result;
                    if (response.IsSuccessStatusCode)
                        break;
                }
                return JsonConvert.DeserializeObject<RetornoToken>(response.Content.ReadAsStringAsync().Result);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}