using System.Collections.Generic;
using System.Net;
using CodingChallenge.API.Common.Interfaces;
using Newtonsoft.Json;

namespace CodingChallenge.API.Common.Logging
{
    public class CodingChallengeApiLogger : ICodingChallengeApiLogger
    {
        private const string REMOVE = @"\";

        public enum CallType
        {
            Get,
            Post,
            Put,
            Delete,
            UploadValues
        }

        private readonly ILoggingService _log;

        public ILoggingService Log() => _log;

        public CodingChallengeApiLogger(ILoggingService log)
        {
            _log = log;
        }

        public string SerializeObject<T>(T obj) where T : class
        {
            return JsonConvert.SerializeObject(obj, Formatting.Indented);
        }

        public void InitialApiLog(string url, CallType type)
        {
            _log.Info($"Initiated {type} to {url}");
        }

        public void LogRequestObject<T>(T obj, bool verboseLogging = false) where T : class
        {
            if (verboseLogging)
            {
                _log.Info($"Request Object - Type {obj.GetType().FullName}:");
                _log.Info(SerializeObject(obj));
            }
        }

        public void LogRequestUrlParms(Dictionary<string, string> parms, bool verboseLogging = false)
        {
            if (verboseLogging)
            {
                _log.Info("Parameters List:");
                foreach (var parm in parms)
                    _log.Info($"{parm.Key}: {parm.Value}");
            }
        }

        public void LogActualResponse(dynamic obj, HttpStatusCode statusCode, bool verboseLogging = false)
        {
            if (verboseLogging)
            {
                _log.Info("Actual Response:");
                _log.Info($"Http Status Code: {statusCode} ({(int) statusCode})");
                _log.Info(SerializeObject(obj));
            }
        }

        public void LogActualRequest(string obj, string message = "Actual Request:", bool verboseLogging = false)
        {

            if (string.IsNullOrEmpty(obj)) return;
            if (verboseLogging)
            {
                _log.Info(message);
                _log.Info(SerializeObject(obj.Replace(REMOVE,string.Empty)));
            }
        }


        public void Log500Response(bool verboseLogging = false)
        {
            if (verboseLogging)
                _log.Info(
                    $"Internal Server Error From API: {HttpStatusCode.InternalServerError} ({(int) HttpStatusCode.InternalServerError})");
        }

        public void LogExpectedResponse<T>(T obj, bool verboseLogging = false) where T : class
        {
            if (verboseLogging)
            {
                _log.Info("Expected Response:");
                _log.Info($"Expected Object - Type {obj.GetType().FullName}:");
                _log.Info(SerializeObject(obj));
            }
        }
    }
}