using System.Net.Http;
using System.Threading.Tasks;

namespace CodingChallenge.API.BusinessLogic.Interfaces.Pixabay
{
    public interface IPixabayHttpWrapper : IHttpWrapperBase
    {
        Task<HttpResponseMessage> GetPixabayResponse(string parameters);
    }
}