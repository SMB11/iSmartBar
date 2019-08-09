using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Infrastructure.Logging
{
    /// <summary>
    /// Formats a message when the callers source information is provided first in the arguments
    /// </summary>
    public static class LoggerSourceFormatter
    {
        /// <summary>
        /// Formats the message including the source information pulled out of the state
        /// </summary>
        /// <param name="state">The state information about the log</param>
        /// <param name="exception">The exception</param>
        /// <returns></returns>
        public static string Format(string message, string origin, string filePath, int lineNumber, Exception exception)
        {
            // Get any exception message
            var exceptionMessage = exception?.ToString();

            // If we have an exception ...
            if (exception != null)
                // New line between message and exception
                exceptionMessage = Environment.NewLine + exception;

            // Format the message string
            return $"{message} [{Path.GetFileName(filePath)} > {origin}() > Line {lineNumber}]{exceptionMessage}";
        }
    }
}
