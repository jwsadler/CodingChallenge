using System;
using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using CodingChallenge.API.BusinessLogic.Interfaces;
using CodingChallenge.API.BusinessLogic.Interfaces.Pixabay;
using CodingChallenge.API.BusinessLogic.Models;
using CodingChallenge.API.BusinessLogic.Models.PixaBay;
using CodingChallenge.API.Common.Extensions;
using CodingChallenge.API.Common.Helpers;
using CodingChallenge.API.Common.Interfaces;

namespace CodingChallenge.API.BusinessLogic.HttpServices.Pixabay
{
    //See https://pixabay.com/api/docs/ for more info on Pixabay APIs

    public class PixabayApiWrapper : IPixabayApiWrapper
    {
        private const string SERVICE_RETURNED_THE_FOLLOWING_STATUS = "Service returned the following status: {0}";
        private const string SAFE_SEARCH_TRUE = "&safeSearch=true";
        private const string KEY_REPLACE = "{key}";
        private const string QUERY_REPLACE = "{query}";
        private const string CATEGORY_REPLACE = "{category}";
        private const string TYPE_REPLACE = "{type}";
        private readonly IAPIConfigurationHelper _apiConfigurationHelper;
        private readonly ICodingChallengeApiLogger _codingChallengeApiLogger;
        private readonly IPixabayHttpWrapper _pixabayHttpWrapper;

        public PixabayApiWrapper(IPixabayHttpWrapper pixabayHttpWrapper, IAPIConfigurationHelper apiConfigurationHelper, ICodingChallengeApiLogger codingChallengeApiLogger)
        {
            _pixabayHttpWrapper = pixabayHttpWrapper;
            _apiConfigurationHelper = apiConfigurationHelper;
            _codingChallengeApiLogger = codingChallengeApiLogger;
        }

        public HttpStatusCode HttpStatusCode { get; set; }

        public void SetupContext()
        {
            _pixabayHttpWrapper.DefaultRequestHeaders.Accept.Clear();

            //Service doesn't support TLS 1.0
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls12 |
                                                   SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;

            VerboseLogging = _apiConfigurationHelper.APIConfiguration.APILogging.VerboseLogging;
            _pixabayHttpWrapper.DefaultRequestHeaders.Clear();
            _pixabayHttpWrapper.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(CodingChallengeConstants.APPLICATION_JSON));
        }

        public bool VerboseLogging { get; set; }

        public async Task<PixabayResponseModel> PixabayApi(CodingChallengeRequestModel emailRequest, bool testing = false)
        {
            try
            {
                SetupContext();

                //Would have liked to use string interpolation - but it didn't work.
                var baseUrl = _apiConfigurationHelper.APIConfiguration.PixabayAPI.UrlFormat.Replace(KEY_REPLACE, _apiConfigurationHelper.APIConfiguration.PixabayAPI.APIKey)
                    .Replace(QUERY_REPLACE, emailRequest.Query).Replace(CATEGORY_REPLACE, emailRequest.Category.ToString().ToLower())
                    .Replace(TYPE_REPLACE, emailRequest.Type.ToString().ToLower());

                var parameters = new StringBuilder();
                parameters.Append(baseUrl);
                if (_apiConfigurationHelper.APIConfiguration.PixabayAPI.SafeSearch) parameters.Append(SAFE_SEARCH_TRUE);


                var response = await _pixabayHttpWrapper.GetPixabayResponse(parameters.ToString())
                    .ConfigureAwait(false);

                HttpStatusCode = response.StatusCode;
                if (!response.IsSuccessStatusCode)
                    _codingChallengeApiLogger.Log().Error(string.Format(SERVICE_RETURNED_THE_FOLLOWING_STATUS,
                        response.StatusCode));

                var result = response.DeserializeHttpMessage<PixabayResponseModel>(_codingChallengeApiLogger,testing, VerboseLogging);

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