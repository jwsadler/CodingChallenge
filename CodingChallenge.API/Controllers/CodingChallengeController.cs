using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CodingChallenge.API.BusinessLogic.Controllers;
using CodingChallenge.API.BusinessLogic.Interfaces;
using CodingChallenge.API.BusinessLogic.Interfaces.Oxford;
using CodingChallenge.API.BusinessLogic.Interfaces.Pixabay;
using CodingChallenge.API.BusinessLogic.Models;
using CodingChallenge.API.BusinessLogic.Models.Oxford;
using CodingChallenge.API.BusinessLogic.Models.PixaBay;
using CodingChallenge.API.Common.ActionFilter;
using CodingChallenge.API.Common.Helpers;
using CodingChallenge.API.Common.Interfaces;

namespace CodingChallenge.API.Controllers
{
    public class CodingChallengeController : BaseApiController
    {
        private readonly IAPIConfigurationHelper _apiConfigurationHelper;
        private readonly IPixabayApiService _pixabayApiService;
        private readonly ILoggingService _loggingService;
        private readonly IEnumerable<IValidationService> _validationServices;
        private readonly IOxfordApiService _oxfordApiService;
        public CodingChallengeController(ILoggingService loggingService, IPixabayApiService pixabayApiService,
            IEnumerable<IValidationService> validationServices, IAPIConfigurationHelper apiConfigurationHelper, IOxfordApiService oxfordApiService) : base(apiConfigurationHelper)
        {
            _loggingService = loggingService;
            _pixabayApiService = pixabayApiService;
            _validationServices = validationServices;
            _apiConfigurationHelper = apiConfigurationHelper;
            _oxfordApiService = oxfordApiService;
        }

        [HttpPost]
        [ModelValidateFilter]
        [Route("api/post/GetData/")]
        public HttpResponseMessage Index([FromBody] CodingChallengeRequestModel model)
        {
            try
            {
                return CodingChallengeResponseModel(model, _pixabayApiService.Pixabay(model), _oxfordApiService.Oxford(model));
            }
            catch (Exception e)
            {
                _loggingService.Error(e.GetInnerMostException().Message, e);
                return Request.CreateResponse(HttpStatusCode.InternalServerError,
                    new
                    {
                        Ok = false,
                        Messages = new List<string> {e.GetInnerMostException().Message, e.ToString()},
                        Request = model
                    });
            }
        }

        private HttpResponseMessage CodingChallengeResponseModel(CodingChallengeRequestModel model, PixabayResponseModel pixabayResponse, OxfordResponseModel oxfordResponse)
        {
            var validationMessages = new List<string>();
            if (_validationServices.Any())
            {
                var stop = false;
                foreach (var validationService in _validationServices)
                {
                    validationService.Validate(model, out var messages, out var hardStop);

                    if (messages.Any())
                        validationMessages.AddRange(messages);

                    stop = hardStop || stop;
                }

                if (stop)
                    return Request.CreateResponse(HttpStatusCode.ExpectationFailed,
                        new {Ok = false, Messages = validationMessages.OrderBy(p => p).ToList(), Request = model});
            }

            return Request.CreateResponse(HttpStatusCode.OK,
                new 
                {
                    Images = pixabayResponse,
                    Words = oxfordResponse,
                    Messages = validationMessages,
                    Request = model
                });
        }

        [HttpGet]
        [ModelValidateFilter]
        [Route("api/get/GetData/")]
        public HttpResponseMessage SendChatBotData([FromUri] CodingChallengeRequestModel model)
        {
            try
            {
                return CodingChallengeResponseModel(model, _pixabayApiService.Pixabay(model), _oxfordApiService.Oxford(model));
            }
            catch (Exception e)
            {
                _loggingService.Error(e.GetInnerMostException().Message, e);
                return Request.CreateResponse(HttpStatusCode.InternalServerError,
                    new
                    {
                        Ok = false,
                        Messages = new List<string> {e.GetInnerMostException().Message, e.ToString()},
                        Request = model
                    });
            }
        }
    }
}