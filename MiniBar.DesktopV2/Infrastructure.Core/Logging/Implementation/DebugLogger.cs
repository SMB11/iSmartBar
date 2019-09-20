using System.Diagnostics;

namespace Infrastructure.Logging
{
    /// <summary>
    /// Logs the messages to the Debug log
    /// </summary>
    public class DebugLogger : ILogger
    {
        /// <summary>
        /// Logs the given message to the system Console
        /// </summary>
        /// <param name="message">The message to log</param>
        /// <param name="level">The level of the message</param>
        public void Log(string message, LogLevel level)
        {
            // The default category
            var category = default(string);

            // NOTE: The native Debug output has no color
            //       However if you install the VS extension VSColorOutput
            //       then this style will color the outputs nicely
            //
            //       https://github.com/mike-ward/VSColorOutput
            //

            // Color console based on level
            switch (level)
            {
                // Debug
                case LogLevel.Debug:
                    category = "information";
                    break;


                // Warning
                case LogLevel.Warning:
                    category = "warning";
                    break;

                // Error
                case LogLevel.Exception:
                    category = "error";
                    break;
            }

            try
            {

                // Write message to console
                Debug.WriteLine(message, category);
            }
            catch
            {
                return;
            }
        }
    }
}
