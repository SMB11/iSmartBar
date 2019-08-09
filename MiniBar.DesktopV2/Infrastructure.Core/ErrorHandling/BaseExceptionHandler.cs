using Infrastructure.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.ErrorHandling
{
    /// <summary>
    /// Handles all exceptions, simply logging them to the logger
    /// </summary>
    public class BaseExceptionHandler : IExceptionHandler
    {
        #region Private

        ICompositeLogger Logger;

        #endregion

        public BaseExceptionHandler(ICompositeLogger logger)
        {
            Logger = logger;
        }

        /// <summary>
        /// Logs the given exception
        /// </summary>
        /// <param name="exception">The exception</param>
        public void HandleError(Exception exception)
        {
            // Log it
            Logger.Log($"Unhandled exception occurred : {exception.ToString()}", LogLevel.Exception);
        }
    }
}
