using CodingChallenge.API.BusinessLogic.Models;
using CodingChallenge.API.BusinessLogic.Models.Oxford;
using CodingChallenge.API.Common.Interfaces;

namespace CodingChallenge.API.BusinessLogic.Interfaces.Oxford
{
    public interface IOxfordApiService : IService
    {
        OxfordResponseModel Oxford(CodingChallengeRequestModel oxfordRequest, bool testing = false);
    }
}