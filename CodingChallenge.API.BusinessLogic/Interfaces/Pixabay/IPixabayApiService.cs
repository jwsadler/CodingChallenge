using CodingChallenge.API.BusinessLogic.Models;
using CodingChallenge.API.BusinessLogic.Models.PixaBay;
using CodingChallenge.API.Common.Interfaces;

namespace CodingChallenge.API.BusinessLogic.Interfaces.Pixabay
{
    public interface IPixabayApiService : IService
    {
        PixabayResponseModel Pixabay(CodingChallengeRequestModel pixabayRequest, bool testing = false);
    }
}