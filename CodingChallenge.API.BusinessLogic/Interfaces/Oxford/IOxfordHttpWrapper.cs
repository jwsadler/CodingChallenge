using System.Net.Http;
using System.Threading.Tasks;

namespace CodingChallenge.API.BusinessLogic.Interfaces.Oxford
{
    public interface IOxfordHttpWrapper : IHttpWrapperBase
    {
        Task<HttpResponseMessage> GetOxfordResponse(string parameters);
    }
}