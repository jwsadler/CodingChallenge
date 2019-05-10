using CodingChallenge.API.BusinessLogic.Interfaces;
using CodingChallenge.API.BusinessLogic.Interfaces.Oxford;
using CodingChallenge.API.BusinessLogic.Models;
using CodingChallenge.API.BusinessLogic.Models.Oxford;

namespace CodingChallenge.API.BusinessLogic.HttpServices.Oxford
{
    public class OxfordApiServices : IOxfordApiService
    {
        private readonly IOxfordApiWrapper _oxfordApiWrapper;

        public OxfordApiServices(IOxfordApiWrapper oxfordApiWrapper)
        {
            _oxfordApiWrapper = oxfordApiWrapper;
        }

        public OxfordResponseModel Oxford(CodingChallengeRequestModel oxfordRequest, bool testing = false)
        {
            var result = _oxfordApiWrapper.OxfordDictionaryAPI(oxfordRequest, testing).Result;

            return result;
        }
    }
}