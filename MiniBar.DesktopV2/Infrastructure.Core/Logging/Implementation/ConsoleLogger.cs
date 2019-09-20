using System;

namespace Infrastructure.Logging
{
    /// <summary>
    /// Logs the messages to the Console
    /// </summary>
    public class ConsoleLogger : ILogger
    {

        /// <summary>
        /// Logs the given message to the system Console
        /// </summary>
        /// <param name="message">The message to log</param>
        /// <param name="level">The level of the message</param>
        public void Log(string message, LogLevel level)
        {
            // Save old color
            var consoleOldColor = Console.ForegroundColor;

            // Default log color value 
            var consoleColor = ConsoleColor.White;

            // Color console based on level
            switch (level)
            {

                // Debug is blue
                case LogLevel.Debug:
                    consoleColor = ConsoleColor.Blue;
                    break;

                // Warning is yellow
                case LogLevel.Warning:
                    consoleColor = ConsoleColor.DarkYellow;
                    break;

                // Error is red
                case LogLevel.Exception:
                    consoleColor = ConsoleColor.Red;
                    break;

            }

            // Set the desired console color
            Console.ForegroundColor = consoleColor;

            // Write message to console
            Console.WriteLine(message);

            // Reset color
            Console.ForegroundColor = consoleOldColor;
        }

    }
}
