using System;
using System.IO;
using System.Net;
using CodingChallenge.API.Common.Interfaces;
using log4net;
using log4net.Config;
using log4net.Util;

namespace CodingChallenge.API.Common.Logging
{
    public sealed class LoggingService : ILoggingService, IDisposable
    {
        #region IDisposable Members

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion

        private void Dispose(bool disposing)
        {
            // Check to see if Dispose has already been called.
            if (!_disposed)
            {
                // If disposing equals true, dispose all managed 
                // and unmanaged resources.
                if (disposing)
                {
                    LogManager.Shutdown();
                }
            }
            _disposed = true;
        }

        #region Private Variables

        private static LoggingService _loggingService;
        private readonly ILog _log;
        private bool _disposed;

        #endregion

        #region Constructor

        /// <summary>
        ///     Static constructor, initialized the configuration and set the logger.
        /// </summary>
        public LoggingService(ILog olog)
        {
            XmlConfigurator.ConfigureAndWatch(new FileInfo(AppDomain.CurrentDomain.SetupInformation.ConfigurationFile));
            GlobalContext.Properties["Hostname"] = Dns.GetHostName();
            GlobalContext.Properties["HostIP"] = Dns.GetHostAddresses(Dns.GetHostName())[0].ToString();
            _log = olog;
            _disposed = false;
        }

       
        #endregion

        #region Public Static Methods

        /// <summary>
        ///     Used to get the Instance of <see cref="LoggingService" /> class.
        /// </summary>
        /// <returns> Instance of <see cref="LoggingService" /> class. </returns>
        public static LoggingService GetLoggingService<T>()
        {
            // Initialize the Logger specific to a type. 
            // No check is done for _log object is null or not as it will create always a new instance.
            _loggingService = new LoggingService(LogManager.GetLogger(typeof(T)));

            return _loggingService;
        }

        /// <summary>
        ///     Used to get the Instance of <see cref="LoggingService" /> class.
        /// </summary>
        /// <returns> Instance of <see cref="LoggingService" /> class. </returns>
        public static LoggingService GetLoggingService(Type typeName)
        {
            // Initialize the Logger specific to a type. 
            // No check is done for _log object is null or not as it will create always a new instance.
            _loggingService = new LoggingService(LogManager.GetLogger(typeName));

            return _loggingService;
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///     Checks whether Debugging message logging is enabled.
        /// </summary>
        public bool IsDebugEnabled
        {
            get { return _log.IsDebugEnabled; }
        }

        /// <summary>
        ///     Checks whether Information message logging is enabled.
        /// </summary>
        public bool IsInfoEnabled
        {
            get { return _log.IsInfoEnabled; }
        }

        /// <summary>
        ///     Checks whether Warning message logging is enabled.
        /// </summary>
        public bool IsWarnEnabled
        {
            get { return _log.IsWarnEnabled; }
        }

        /// <summary>
        ///     Checks whether Error message logging is enabled.
        /// </summary>
        public bool IsErrorEnabled
        {
            get { return _log.IsErrorEnabled; }
        }

        /// <summary>
        ///     Checks whether Fatal message logging is enabled.
        /// </summary>
        public bool IsFatalEnabled
        {
            get { return _log.IsFatalEnabled; }
        }

        /// <summary>
        ///     Properties bag of Log4Net. We can use this to set the behaviour of logger at runtime or for a specific action.
        /// </summary>
        public GlobalContextProperties Properties
        {
            get { return GlobalContext.Properties; }
        }

        #endregion

        #region Public Methods

        /// <summary>
        ///     Write the message as debug message.
        /// </summary>
        /// <param name="message"> Debugging Message </param>
        public void Debug(object message)
        {
            _log.Debug(message);
        }

        /// <summary>
        ///     DebugFormat sets the format of the debug messages and the values that will become part of the message.
        ///     LoggingService will log the message as per the format specified by the caller.
        /// </summary>
        /// <param name="format"> Message Format </param>
        /// <param name="args"> Message values </param>
        public void DebugFormat(string format, params object[] args)
        {
            _log.DebugFormat(format, args);
        }

        /// <summary>
        ///     Write a general purpose information message in the logger.
        /// </summary>
        /// <param name="message"> Information Message </param>
        public void Info(object message)
        {
            _log.Info(message);
        }

        /// <summary>
        ///     InfoFormat logs the information message in the format provided by the caller. Apart from format the values that
        ///     will become part of the message will be provided.
        /// </summary>
        /// <param name="format"> Message Format </param>
        /// <param name="args"> Message Values </param>
        public void InfoFormat(string format, params object[] args)
        {
            _log.InfoFormat(format, args);
        }

        /// <summary>
        ///     Write a warning message in the logger.
        /// </summary>
        /// <param name="message"> Warning Message </param>
        public void Warn(object message)
        {
            _log.Warn(message);
        }

        /// <summary>
        ///     Overloaded method Warn will write the message and also logs the exception associated with it.
        /// </summary>
        /// <param name="message"> Warning Message </param>
        /// <param name="exception"> Exception object. </param>
        public void Warn(object message, Exception exception)
        {
            _log.Warn(message, exception);
        }

        /// <summary>
        ///     Overloaded method Warn logs the warning message in the format provided by the caller. Apart from format the values
        ///     that will be replaced in the message will be provided.
        /// </summary>
        /// <param name="format"> Message Format </param>
        /// <param name="args"> Message Values </param>
        public void WarnFormat(string format, params object[] args)
        {
            _log.WarnFormat(format, args);
        }

        /// <summary>
        ///     Error method will log the application error messages in the logger.
        /// </summary>
        /// <param name="message"> Error Message </param>
        public void Error(object message)
        {
            _log.Error(message);
        }

        /// <summary>
        ///     Overloaded method Error logs the error message along with the underlying exception.
        /// </summary>
        /// <param name="message"> Error Message </param>
        /// <param name="exception"> Exception object for logging </param>
        public void Error(object message, Exception exception)
        {
            _log.Error(message, exception);
        }

        /// <summary>
        ///     ErrorFormat method will log the error messages in the format provided by the caller. Apart from format the values
        ///     that will be replaced in the message will be provided.
        /// </summary>
        /// <param name="format"> Error Format </param>
        /// <param name="args"> Message Values </param>
        public void ErrorFormat(string format, params object[] args)
        {
            _log.ErrorFormat(format, args);
        }

        /// <summary>
        ///     Fatal method logs the fatal messages that are critical to both application as well as for the business.
        /// </summary>
        /// <param name="message"> Fatal Message for logging </param>
        public void Fatal(object message)
        {
            _log.Fatal(message);
        }

        /// <summary>
        ///     Overloaded method Fatal will log the fatal message along with the underlying exception.
        /// </summary>
        /// <param name="message"> Fatal Message </param>
        /// <param name="exception"> Exception object for logging </param>
        public void Fatal(object message, Exception exception)
        {
            _log.Fatal(message, exception);
        }

        /// <summary>
        ///     FatalFormat method will log the fatal messages in the format provided by the caller. Apart from format the values
        ///     that will be replaced in the message will be provided.
        /// </summary>
        /// <param name="format"> Message Format </param>
        /// <param name="args"> Message Values </param>
        public void FatalFormat(string format, params object[] args)
        {
            _log.FatalFormat(format, args);
        }

        #endregion
    }
}