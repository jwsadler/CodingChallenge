using System.Collections.Generic;
using System.Net;
using CodingChallenge.API.Common.Logging;

namespace CodingChallenge.API.Common.Interfaces
{
    public interface ICodingChallengeApiLogger : IHelper
    {
        ILoggingService Log();
        string SerializeObject<T>(T obj) where T : class;
        void InitialApiLog(string url, CodingChallengeApiLogger.CallType type);
        void LogRequestObject<T>(T obj, bool verboseLogging = false) where T : class;
        void LogRequestUrlParms(Dictionary<string, string> parms, bool verboseLogging = false);
        void LogActualResponse(dynamic obj, HttpStatusCode statusCode, bool verboseLogging = false);
        void LogActualRequest(string obj, string message = "Actual Request:", bool verboseLogging = false);
        void Log500Response(bool verboseLogging = false);
        void LogExpectedResponse<T>(T obj, bool verboseLogging = false) where T : class;
    }
}