using ApplicationLoan.Infra.CrossCutting.Helpers.Auth;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace ApplicationLoan.Infra.CrossCutting.Helpers.Rest
{
    public class RestHelpers
    {
        #region GetAsync
        public (bool success, string message, TResult result) GetAsync<TResult>(RestApi restApi, string relativeUrl, TypeOfAuth typeOfAuth)
        {
            try
            {
                HttpClient httpClient = new HttpClient();
                SetToken(restApi, typeOfAuth, httpClient);

                var result = httpClient.GetAsync($"{GetBaseUrl(restApi)}{relativeUrl}").Result;

                if (!result.IsSuccessStatusCode)
                    return (false, "Falha na conexão com a API.", default(TResult));

                return (true, string.Empty, JsonConvert.DeserializeObject<TResult>(result.Content.ReadAsStringAsync().Result));
            }
            catch (Exception ex)
            {
                return (false, ex.Message, default(TResult));
            }
        }
        #endregion

        #region PostAsync
        public (bool success, string message) PostAsync<TModel>(RestApi restApi, string relativeUrl, TModel model, TypeOfAuth typeOfAuth)
        {
            try
            {
                HttpClient httpClient = new HttpClient();
                SetToken(restApi, typeOfAuth, httpClient);
                HttpResponseMessage result = PostAsync(restApi, relativeUrl, model, httpClient);

                if (!result.IsSuccessStatusCode)
                {
                    var msgRetorno = result.Content.ReadAsStringAsync().Result;
                    if (string.IsNullOrEmpty(msgRetorno))
                        return (false, "Falha na conexão com a API.");
                    else
                        return (false, msgRetorno);
                }

                return (true, string.Empty);
            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }
        }

        public (bool success, string message, TResult result) PostAsync<TModel, TResult>(RestApi restApi, string relativeUrl, TModel model, TypeOfAuth typeOfAuth)
        {
            try
            {
                HttpClient httpClient = new HttpClient();
                SetToken(restApi, typeOfAuth, httpClient);
                HttpResponseMessage result = PostAsync(restApi, relativeUrl, model, httpClient);

                if (!result.IsSuccessStatusCode)
                    return (false, "Falha na conexão com a API.", default(TResult));

                return (true, string.Empty, JsonConvert.DeserializeObject<TResult>(result.Content.ReadAsStringAsync().Result));
            }
            catch (Exception ex)
            {
                return (false, ex.Message, default(TResult));
            }
        }
        #endregion

        #region PutAsync
        public (bool success, string message) PutAsync<TModel>(RestApi restApi, string relativeUrl, TModel model, TypeOfAuth typeOfAuth)
        {
            try
            {
                HttpClient httpClient = new HttpClient();
                SetToken(restApi, typeOfAuth, httpClient);
                HttpResponseMessage result = PutAsync(restApi, relativeUrl, model, httpClient);

                if (!result.IsSuccessStatusCode)
                    return (false, "Falha na conexão com a API.");

                return (true, string.Empty);
            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }
        }

        public (bool success, string message, TResult result) PutAsync<TModel, TResult>(RestApi restApi, string relativeUrl, TModel model, TypeOfAuth typeOfAuth)
        {
            try
            {
                HttpClient httpClient = new HttpClient();
                SetToken(restApi, typeOfAuth, httpClient);
                HttpResponseMessage result = PutAsync(restApi, relativeUrl, model, httpClient);

                if (!result.IsSuccessStatusCode)
                    return (false, "Falha na conexão com a API.", default(TResult));

                return (true, string.Empty, JsonConvert.DeserializeObject<TResult>(result.Content.ReadAsStringAsync().Result));
            }
            catch (Exception ex)
            {
                return (false, ex.Message, default(TResult));
            }
        }
        #endregion

        #region Private Methods
        private HttpResponseMessage PostAsync<TModel>(RestApi restApi, string relativeUrl, TModel model, HttpClient httpClient)
        {
            return httpClient.PostAsync($"{GetBaseUrl(restApi)}{relativeUrl}", new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json")).Result;
        }

        private HttpResponseMessage PutAsync<TModel>(RestApi restApi, string relativeUrl, TModel model, HttpClient httpClient)
        {
            return httpClient.PutAsync($"{GetBaseUrl(restApi)}{relativeUrl}", new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json")).Result;
        }

        private void SetToken(RestApi restApi, TypeOfAuth typeOfAuth, HttpClient httpClient)
        {
            switch (typeOfAuth)
            {
                case TypeOfAuth.BEARER:

                    for (int i = 0; i < 5; i++)
                    {
                        var authToken = new AuthenticationHelpers().ObterAuthToken(GetClientId(restApi), GetClientSecret(restApi));

                        if (authToken != null)
                        {
                            httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", string.Format("Bearer {0}", authToken.Access_token));
                            break;
                        }
                    }
                    break;

                case TypeOfAuth.BASIC:

                    httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", string.Format("Basic {0}", GetAccessKey(restApi)));
                    break;

                case TypeOfAuth.KEY:

                    httpClient.DefaultRequestHeaders.TryAddWithoutValidation("x-api-key", string.Format("{0}", GetAccessKey(restApi)));
                    break;

                default:
                    break;
            }
        }
        
        private string GetBaseUrl(RestApi restApi)
        {
            switch (restApi)
            {
                case RestApi.NOVERDE:
                    //#if DEBUG
                    //                    return "http://localhost:59159/api/";
                    //#else
                    return "https://challenge.noverde.name/";

                default:
                    return string.Empty;
            }
        }

        private string GetClientId(RestApi restApi)
        {
            switch (restApi)
            {
                case RestApi.NOVERDE:
                    return "NA";
                default:
                    return string.Empty;
            }
        }

        private string GetClientSecret(RestApi restApi)
        {
            switch (restApi)
            {
                case RestApi.NOVERDE:
                    return "NA";
                default:
                    return string.Empty;
            }
        }

        private string GetAccessKey(RestApi restApi)
        {
            switch (restApi)
            {
                case RestApi.NOVERDE:
                    return "HQK38AzG516eDueIyvIUL3yKTW1HqMaU1PSPYzVr";
                default:
                    return string.Empty;
            }
        }

        #endregion
    }
}