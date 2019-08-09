using Prism.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.CompilerServices;

namespace Infrastructure.Logging
{
    /// <summary>
    /// ICompositeLogger implementation that reroutes log messages to child ICompositeLoggers
    /// Logs details to the Debug by default
    /// </summary>
    public class CompositeLogger : ICompositeLogger
    {
        #region Protected Methods

        /// <summary>
        /// The list of loggers in this factory
        /// </summary>
        protected List<ILogger> mLoggers = new List<ILogger>();

        /// <summary>
        /// A lock for the logger list to keep it thread-safe
        /// </summary>
        protected object mLoggersLock = new object();

        #endregion

        #region Public Properties

        /// <summary>
        /// The level of logging to output
        /// </summary>
        public LogLevel LogOutputLevel { get; set; }

        /// <summary>
        /// If true, includes the origin of where the log message was logged from
        /// such as the class name, line number and file name
        /// </summary>
        public bool IncludeLogOriginDetails { get; set; } = true;

        #endregion

        #region Public Methods

        /// <summary>
        /// Adds the specific logger to this factory
        /// </summary>
        /// <param name="logger">The logger</param>
        public void AddLogger(ILogger logger)
        {
            // Log the list so it is thread-safe
            lock (mLoggersLock)
            {
                // If the logger is not already in the list...
                if (!mLoggers.Contains(logger))
                    // Add the logger to the list
                    mLoggers.Add(logger);
            }
        }

        /// <summary>
        /// Removes the specified logger from this factory
        /// </summary>
        /// <param name="logger">The logger</param>
        public void RemoveLogger(ILogger logger)
        {
            // Log the list so it is thread-safe
            lock (mLoggersLock)
            {
                // If the logger is in the list...
                if (mLoggers.Contains(logger))
                    // Remove the logger from the list
                    mLoggers.Remove(logger);
            }
        }

        /// <summary>
        /// Logs the specific message to all loggers in this factory
        /// </summary>
        /// <param name="message">The message to log</param>
        /// <param name="level">The level of the message being logged</param>
        /// <param name="origin">The method/function this message was logged in</param>
        /// <param name="filePath">The code filename that this message was logged from</param>
        /// <param name="lineNumber">The line of code in the filename this message was logged from</param>
        public void Log(
            string message,
            LogLevel level = LogLevel.Informative,
            [CallerMemberName] string origin = "",
            [CallerFilePath] string filePath = "",
            [CallerLineNumber] int lineNumber = 0)
        {

            // If we should not log the message as the level is too low...
            if ((int)level < (int)LogOutputLevel)
                return;

            message = $"[{level}] {message}";
            // If the user wants to know where the log originated from...
            if (IncludeLogOriginDetails)
                message = $"{message} [{Path.GetFileName(filePath)} > {origin}() > Line {lineNumber}]";

            // Log to all loggers
            mLoggers.ForEach(logger => logger.Log(message, level));

            // Inform listeners
        }

        #endregion
    }
}