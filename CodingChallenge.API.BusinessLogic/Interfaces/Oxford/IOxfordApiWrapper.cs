using System.Threading.Tasks;
using CodingChallenge.API.BusinessLogic.Models;
using CodingChallenge.API.BusinessLogic.Models.Oxford;

namespace CodingChallenge.API.BusinessLogic.Interfaces.Oxford
{
    public interface IOxfordApiWrapper : IApiWrapperBase
    {
        Task<OxfordResponseModel> OxfordDictionaryAPI(CodingChallengeRequestModel emailRequest, bool testing = false);
    }
}