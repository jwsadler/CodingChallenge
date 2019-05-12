using System.Net;

namespace CodingChallenge.API.Common.Interfaces
{
    public interface IStatus
    {
        HttpStatusCode StatusCode { get; set; }
    }
}