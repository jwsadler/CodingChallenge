using CodingChallenge.API.Common.Interfaces;

namespace CodingChallenge.API.Common.Logging
{
    public class Logging : IService
    {
        #region Private members

        /// <summary>
        ///     for logging
        /// </summary>
        private ILoggingService _loggingService;

        #endregion

        public Logging(ILoggingService loggingService)
        {
            _loggingService = loggingService;
        }

        #region Protected  methods

        /// <summary>
        ///     Object used for logging information
        /// </summary>
        protected ILoggingService LoggerService => _loggingService ?? (_loggingService = LoggingService.GetLoggingService(GetType()));

        #endregion
    }
}