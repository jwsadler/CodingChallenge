using System;
using System.Net.Http.Headers;
using CodingChallenge.API.Common.Interfaces;

namespace CodingChallenge.API.BusinessLogic.Interfaces
{
    public interface IHttpWrapperBase : IHelper
    {
        Uri BaseAddress { get; }
        HttpRequestHeaders DefaultRequestHeaders { get; }
    }
}