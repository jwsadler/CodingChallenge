using System.CodeDom.Compiler;
using System.Linq;
using CodingChallenge.API.BusinessLogic.Interfaces;
using CodingChallenge.API.BusinessLogic.Interfaces.Pixabay;
using CodingChallenge.API.BusinessLogic.Models;
using CodingChallenge.API.BusinessLogic.Models.PixaBay;

namespace CodingChallenge.API.BusinessLogic.HttpServices.Pixabay
{
    public class PixabayApiServices : IPixabayApiService
    {
        private readonly IPixabayApiWrapper _pixabayApiWrapper;
        private readonly IAPIConfigurationHelper _apiConfigurationHelper;

        public PixabayApiServices(IPixabayApiWrapper pixabayApiWrapper, IAPIConfigurationHelper apiConfigurationHelper)
        {
            _pixabayApiWrapper = pixabayApiWrapper;
            _apiConfigurationHelper = apiConfigurationHelper;
        }

        public PixabayResponseModel Pixabay(CodingChallengeRequestModel pixabayRequest, bool testing = false)
        {
            var result =  _pixabayApiWrapper.PixabayApi(pixabayRequest, testing).Result;

            result.MaxRequested = _apiConfigurationHelper.APIConfiguration.PixabayAPI.MaxNumberOfImages;

            result.Hits = result.Hits.Take(result.MaxRequested).ToList();

            return result;
        }
    }
}