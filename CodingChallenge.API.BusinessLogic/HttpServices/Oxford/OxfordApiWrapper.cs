using System;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using CodingChallenge.API.BusinessLogic.Interfaces;
using CodingChallenge.API.BusinessLogic.Interfaces.Oxford;
using CodingChallenge.API.BusinessLogic.Models;
using CodingChallenge.API.BusinessLogic.Models.Oxford;
using CodingChallenge.API.Common.Extensions;
using CodingChallenge.API.Common.Helpers;
using CodingChallenge.API.Common.Interfaces;

namespace CodingChallenge.API.BusinessLogic.HttpServices.Oxford
{
    public class OxfordApiWrapper : IOxfordApiWrapper
    {
        private const string SERVICE_RETURNED_THE_FOLLOWING_STATUS = "Service returned the following status: {0}";
        private const string QUERY_REPLACE = "{query}";
        private const string APP_ID = "app_id";
        private const string APP_KEY = "app_key";

        private readonly IAPIConfigurationHelper _apiConfigurationHelper;
        private readonly ICodingChallengeApiLogger _codingChallengeApiLogger;
        private readonly IOxfordHttpWrapper _oxfordHttpWrapper;

        public OxfordApiWrapper(IOxfordHttpWrapper oxfordHttpWrapper, IAPIConfigurationHelper apiConfigurationHelper, ICodingChallengeApiLogger codingChallengeApiLogger)
        {
            _oxfordHttpWrapper = oxfordHttpWrapper;
            _apiConfigurationHelper = apiConfigurationHelper;
            _codingChallengeApiLogger = codingChallengeApiLogger;
        }

        public HttpStatusCode HttpStatusCode { get; set; }

        public void SetupContext()
        {
            _oxfordHttpWrapper.DefaultRequestHeaders.Accept.Clear();

            //Service doesn't support TLS 1.0
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls12 |
                                                   SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;

            VerboseLogging = _apiConfigurationHelper.APIConfiguration.APILogging.VerboseLogging;

            _oxfordHttpWrapper.DefaultRequestHeaders.Clear();

           _oxfordHttpWrapper.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(CodingChallengeConstants.APPLICATION_JSON));

            _oxfordHttpWrapper.DefaultRequestHeaders.Add(APP_ID, _apiConfigurationHelper.APIConfiguration.OxfordDictionaryAPI.AppId);
            _oxfordHttpWrapper.DefaultRequestHeaders.Add(APP_KEY, _apiConfigurationHelper.APIConfiguration.OxfordDictionaryAPI.APIKey);
        }

        public bool VerboseLogging { get; set; }

        public async Task<OxfordResponseModel> OxfordDictionaryAPI(CodingChallengeRequestModel emailRequest, bool testing = false)
        {
            try
            {
                SetupContext();

                //Would have liked to use string interpolation - but it didn't work.
                var baseUrl = _apiConfigurationHelper.APIConfiguration.OxfordDictionaryAPI.UrlFormat.Replace(QUERY_REPLACE, emailRequest.Query.Split(' ').FirstOrDefault());

                var parameters = new StringBuilder();
                parameters.Append(baseUrl);

                var response = await _oxfordHttpWrapper.GetOxfordResponse(parameters.ToString())
                    .ConfigureAwait(false);

                HttpStatusCode = response.StatusCode;
                if (!response.IsSuccessStatusCode)
                    _codingChallengeApiLogger.Log().Error(string.Format(SERVICE_RETURNED_THE_FOLLOWING_STATUS,
                        response.StatusCode));

                var result = response.DeserializeHttpMessage<OxfordResponseModel>(_codingChallengeApiLogger, testing, VerboseLogging);

                return result;
            }
            catch (Exception ex)
            {
                _codingChallengeApiLogger.Log().Error(ex.GetInnerMostException().Message, ex);
                throw;
            }
        }
    }
}