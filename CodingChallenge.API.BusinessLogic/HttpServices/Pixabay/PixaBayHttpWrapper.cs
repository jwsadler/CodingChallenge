using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using CodingChallenge.API.BusinessLogic.Interfaces;
using CodingChallenge.API.BusinessLogic.Interfaces.Pixabay;
using CodingChallenge.API.Common.Helpers;
using CodingChallenge.API.Common.Interfaces;
using CodingChallenge.API.Common.Logging;

namespace CodingChallenge.API.BusinessLogic.HttpServices.Pixabay
{
    public class PixabayHttpWrapper : IPixabayHttpWrapper
    {
        private static readonly HttpClient client;
        private static readonly ICCAApiLogger cca_api_logger;


        static PixabayHttpWrapper()
        {
            var apiConfigHelper = ContainerHelper.Container.Resolve<IAPIConfigurationHelper>();
            cca_api_logger = ContainerHelper.Container.Resolve<ICCAApiLogger>();

            var url = apiConfigHelper?.APIConfiguration.PixabayAPI.BaseUrl;
            VerboseLogging = apiConfigHelper?.APIConfiguration.APILogging.VerboseLogging ?? false;

            if (!string.IsNullOrEmpty(url))
                client = new HttpClient
                {
                    BaseAddress = new Uri(url)
                };
        }

        public static bool VerboseLogging { get; set; }

        public Uri BaseAddress => client.BaseAddress;

        public HttpRequestHeaders DefaultRequestHeaders => client.DefaultRequestHeaders;

        public Task<HttpResponseMessage> GetPixabayResponse(string parameters)
        {
            cca_api_logger.InitialApiLog(
                $"{BaseAddress.OriginalString}{parameters}",
                CCAApiLogger.CallType.Get);

            return client.GetAsync(parameters);
        }
    }
}