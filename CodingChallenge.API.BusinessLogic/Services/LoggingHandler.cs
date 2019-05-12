using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using CodingChallenge.API.Common.Helpers;
using CodingChallenge.API.Common.Interfaces;

namespace CodingChallenge.API.BusinessLogic.Services
{
    public class LogRequestAndResponseHandler : DelegatingHandler
    {
        protected override async Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var ccaAPILogger = ContainerHelper.Container.Resolve<ICodingChallengeApiLogger>();

            // log request body
            var requestBody = await request.Content.ReadAsStringAsync();

            ccaAPILogger.LogActualRequest(requestBody,$"Incoming Request to {request.RequestUri.AbsoluteUri}", true);


            // let other handlers process the request
            var result = await base.SendAsync(request, cancellationToken);

            if (result.Content != null)
            {
                // once response body is ready, log it
                var responseBody = await result.Content.ReadAsStringAsync();
                ccaAPILogger.LogActualResponse(requestBody, result.StatusCode, false);
            }

            return result;
        }
    }
}