using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Logging
{
    /// <summary>
    /// ICompositeLogger that reroutes log messages to child ICompositeLoggers
    /// </summary>
    public interface ICompositeLogger
    {
        /// <summary>
        /// If true, includes the origin of where the log message was logged from
        /// such as the class name, line number and file name
        /// </summary>
        bool IncludeLogOriginDetails { get; set; }

        /// <summary>
        /// Adds the specific logger
        /// </summary>
        /// <param name="logger">The logger</param>
        void AddLogger(ILogger logger);

        /// <summary>
        /// Removes the specified logger
        /// </summary>
        /// <param name="logger">The logger</param>
        void RemoveLogger(ILogger logger);

        /// <summary>
        /// Handles the logged message being passed in
        /// </summary>
        /// <param name="message">The message being log</param>
        /// <param name="level">The level of the log message</param>
        void Log(string message, LogLevel level, string origin = "", string filePath = "", int lineNumber = 0);
    }
}
