using System;
using log4net.Util;

namespace CodingChallenge.API.Common.Interfaces
{
    public interface ILoggingService : IService
    {
        #region Properties

        /// <summary>
        ///     Checks whether Debugging message logging is enabled.
        /// </summary>
        bool IsDebugEnabled { get; }

        /// <summary>
        ///     Checks whether Information message logging is enabled.
        /// </summary>
        bool IsInfoEnabled { get; }

        /// <summary>
        ///     Checks whether Warning message logging is enabled.
        /// </summary>
        bool IsWarnEnabled { get; }

        /// <summary>
        ///     Checks whether Error message logging is enabled.
        /// </summary>
        bool IsErrorEnabled { get; }


        /// <summary>
        ///     Checks whether Fatal message logging is enabled.
        /// </summary>
        bool IsFatalEnabled { get; }

        /// <summary>
        ///     Properties bag of Log4Net. We can use this to set the behaviour of logger at runtime or for a specific action.
        /// </summary>
        GlobalContextProperties Properties { get; }

        #endregion

        #region Methods

        /// <summary>
        ///     Write the message as debug message.
        /// </summary>
        /// <param name="message"> Debugging Message </param>
        void Debug(object message);


        /// <summary>
        ///     DebugFormat sets the format of the debug messages and the values that will become part of the message.
        ///     LoggingService will log the message as per the format specified by the caller.
        /// </summary>
        /// <param name="format"> Message Format </param>
        /// <param name="args"> Message values </param>
        void DebugFormat(string format, params object[] args);

        /// <summary>
        ///     Write a general purpose information message in the logger.
        /// </summary>
        /// <param name="message"> Information Message </param>
        void Info(object message);


        /// <summary>
        ///     InfoFormat logs the information message in the format provided by the caller. Apart from format the values that
        ///     will become part of the message will be provided.
        /// </summary>
        /// <param name="format"> Message Format </param>
        /// <param name="args"> Message Values </param>
        void InfoFormat(string format, params object[] args);

        /// <summary>
        ///     Write a warning message in the logger.
        /// </summary>
        /// <param name="message"> Warning Message </param>
        void Warn(object message);

        /// <summary>
        ///     Overloaded method Warn will write the message and also logs the exception associated with it.
        /// </summary>
        /// <param name="message"> Warning Message </param>
        /// <param name="exception"> Exception object. </param>
        void Warn(object message, Exception exception);


        /// <summary>
        ///     Overloaded method Warn logs the warning message in the format provided by the caller. Apart from format the values
        ///     that will be replaced in the message will be provided.
        /// </summary>
        /// <param name="format"> Message Format </param>
        /// <param name="args"> Message Values </param>
        void WarnFormat(string format, params object[] args);

        /// <summary>
        ///     Error method will log the application error messages in the logger.
        /// </summary>
        /// <param name="message"> Error Message </param>
        void Error(object message);

        /// <summary>
        ///     Overloaded method Error logs the error message along with the underlying exception.
        /// </summary>
        /// <param name="message"> Error Message </param>
        /// <param name="exception"> Exception object for logging </param>
        void Error(object message, Exception exception);

        /// <summary>
        ///     ErrorFormat method will log the error messages in the format provided by the caller. Apart from format the values
        ///     that will be replaced in the message will be provided.
        /// </summary>
        /// <param name="format"> Error Format </param>
        /// <param name="args"> Message Values </param>
        void ErrorFormat(string format, params object[] args);

        /// <summary>
        ///     Fatal method logs the fatal messages that are critical to both application as well as for the business.
        /// </summary>
        /// <param name="message"> Fatal Message for logging </param>
        void Fatal(object message);

        /// <summary>
        ///     Overloaded method Fatal will log the fatal message along with the underlying exception.
        /// </summary>
        /// <param name="message"> Fatal Message </param>
        /// <param name="exception"> Exception object for logging </param>
        void Fatal(object message, Exception exception);

        /// <summary>
        ///     FatalFormat method will log the fatal messages in the format provided by the caller. Apart from format the values
        ///     that will be replaced in the message will be provided.
        /// </summary>
        /// <param name="format"> Message Format </param>
        /// <param name="args"> Message Values </param>
        void FatalFormat(string format, params object[] args);

        #endregion
    }
}
