using System.Net;
using System.Net.Http;
using System.Web.Http;
using CodingChallenge.API.BusinessLogic.Interfaces;

namespace CodingChallenge.API.BusinessLogic.Controllers
{
    public class BaseApiController : ApiController
    {
        private const string OK = "ok";
        private const string TESTING_NOT_ENABLED = "Testing not enabled!";
        protected const string INTERNAL_SERVER_ERROR = "Internal Server Error Ocurred - See Logs for Info";

        private readonly IAPIConfigurationHelper _apiConfigurationHelper;

        public BaseApiController(IAPIConfigurationHelper apiConfigurationHelper)
        {
            _apiConfigurationHelper = apiConfigurationHelper;
        }

        [HttpGet]
        [Route("api/get/test/")]
        public dynamic Test()
        {
            if (!_apiConfigurationHelper.APIConfiguration.EnableTestApi)
                return Request.CreateResponse(HttpStatusCode.MethodNotAllowed, new HttpError(TESTING_NOT_ENABLED));
            return OK;
        }

        [HttpPost]
        [Route("api/post/test/")]
        public dynamic TestPost()
        {
            if (!_apiConfigurationHelper.APIConfiguration.EnableTestApi)
                return Request.CreateResponse(HttpStatusCode.MethodNotAllowed, new HttpError(TESTING_NOT_ENABLED));
            return OK;
        }
    }
}