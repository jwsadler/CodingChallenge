using System.Threading.Tasks;
using CodingChallenge.API.BusinessLogic.Models;
using CodingChallenge.API.BusinessLogic.Models.PixaBay;

namespace CodingChallenge.API.BusinessLogic.Interfaces.Pixabay
{
    public interface IPixabayApiWrapper : IApiWrapperBase
    {
        Task<PixabayResponseModel> PixabayApi(CodingChallengeRequestModel emailRequest, bool testing = false);
    }
}