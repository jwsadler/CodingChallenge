using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using CodingChallenge.API.Common.Helpers;
using CodingChallenge.API.Common.Interfaces;
using Newtonsoft.Json;

namespace CodingChallenge.API.Common.Extensions
{
    public static class HttpClientExtensions
    {
        private const string REMOVE = @"\";
        private const string THERE_WAS_AN_ISSUE_LOGGING_THIS_MESSAGE = "There was an issue logging this message: ";

        public static Task<HttpResponseMessage> DeleteAsJsonAsync<T>(this HttpClient httpClient, string requestUri,
            T data)
            => httpClient.SendAsync(new HttpRequestMessage(HttpMethod.Delete, requestUri)
            { Content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, CCAConstants.APPLICATION_JSON) });

        public static T DeserializeHttpMessage<T>(this HttpResponseMessage response, bool testing = false, bool verboseLogging = false)
            where T : class
        {

            var ccaAPILogger = ContainerHelper.Container.Resolve<ICCAApiLogger>();


            try
            {
                T result;
                if (verboseLogging)
                {
                    if (response.StatusCode == HttpStatusCode.InternalServerError)
                    {
                        ccaAPILogger.Log500Response();
                    }

                    dynamic actualResponseObject;

                    if (testing)
                    {
                        actualResponseObject = JsonConvert.DeserializeObject<dynamic>(
                            response.Content.ReadAsStringAsync().Result.Replace(REMOVE, string.Empty));
                    }
                    else
                    {
                        actualResponseObject = response.Content.ReadAsAsync<dynamic>().Result;
                    }

                    ccaAPILogger.LogActualResponse(actualResponseObject, response.StatusCode, true);

                    var objString = JsonConvert.SerializeObject(actualResponseObject);

                    result = JsonConvert.DeserializeObject<T>(objString);

                    ccaAPILogger.LogExpectedResponse(result, true);
                }
                else
                {
                    if (testing)
                    {
                        result = JsonConvert.DeserializeObject<T>(
                            response.Content.ReadAsStringAsync().Result.Replace(REMOVE, string.Empty));
                    }
                    else
                    {
                        result = response.Content.ReadAsAsync<T>().Result;
                    }
                }

               

                return result;
            }
            catch (Exception ex)
            {
                ccaAPILogger.Log().Error(THERE_WAS_AN_ISSUE_LOGGING_THIS_MESSAGE + ex.GetInnerMostException().Message);
                return null;
            }
        }

        public static void LogHttpMessage(this HttpResponseMessage response, bool testing = false, bool verboseLogging = false)

        {
            var ccaAPILogger = ContainerHelper.Container.Resolve<ICCAApiLogger>();
            try
            {
                if (verboseLogging)
                {
                    dynamic actualResponseObject;

                    if (testing)
                    {
                        actualResponseObject = JsonConvert.DeserializeObject<dynamic>(
                            response.Content.ReadAsStringAsync().Result.Replace(REMOVE, string.Empty));
                    }
                    else
                    {
                        actualResponseObject = response.Content.ReadAsAsync<dynamic>().Result;
                    }

                    ccaAPILogger.LogActualResponse(actualResponseObject, response.StatusCode);
                }
            }
            catch (Exception ex)
            {
                ccaAPILogger.Log().Error(THERE_WAS_AN_ISSUE_LOGGING_THIS_MESSAGE + ex.GetInnerMostException().Message);
            }

        }
    }
}
