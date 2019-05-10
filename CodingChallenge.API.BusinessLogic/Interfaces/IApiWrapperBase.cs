using System.Net;
using CodingChallenge.API.Common.Interfaces;

namespace CodingChallenge.API.BusinessLogic.Interfaces
{
    public interface IApiWrapperBase : IService
    {
        HttpStatusCode HttpStatusCode { get; set; }

        bool VerboseLogging { get; set; }

        void SetupContext();
    }
}