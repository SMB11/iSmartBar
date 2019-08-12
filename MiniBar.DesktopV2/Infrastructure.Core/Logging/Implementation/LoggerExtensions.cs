using Prism.Ioc;
using System;
using System.Runtime.CompilerServices;

namespace Infrastructure.Logging
{
    /// <summary>
    /// Extensions for <see cref="ICompositeLogger"/> loggers
    /// </summary>
    public static class LoggerExtensions
    {
        /// <summary>
        /// Adds a debbug logger to a composite logger
        /// </summary>
        /// <param name="logger">the composite logger</param>
        public static ICompositeLogger AddDebug(this ICompositeLogger logger)
        {
            logger.AddLogger(new DebugLogger());
            return logger;
        }

        /// <summary>
        /// Adds a console logger to a composite logger
        /// </summary>
        /// <param name="logger">the composite logger</param>
        public static ICompositeLogger AddConsole(this ICompositeLogger logger)
        {
            logger.AddLogger(new ConsoleLogger());
            return logger;
        }

        /// <summary>
        /// Adds logging to container registry
        /// </summary>
        /// <param name="containerRegistry">the container registry</param>
        /// <param name="configure">configuration callback that should be used to add loggers</param>
        /// <returns></returns>
        public static IContainerRegistry AddLogging(this IContainerRegistry containerRegistry, Action<ICompositeLogger> configure)
        {
            ICompositeLogger logger = new CompositeLogger();
            configure?.Invoke(logger);
            containerRegistry.RegisterInstance<ICompositeLogger>(logger);
            return containerRegistry;
        }

        /// <summary>
        /// Logs a debug message, including the source of the log
        /// </summary>
        /// <param name="logger">The logger</param>
        /// <param name="message">The message</param>
        /// <param name="exception">The exception</param>
        /// <param name="origin">The callers member/function name</param>
        /// <param name="filePath">The source code file path</param>
        /// <param name="lineNumber">The line number in the code file of the caller</param>
        public static void LogDebugSource(
            this ICompositeLogger logger,
            string message,
            Exception exception = null,
            [CallerMemberName] string origin = "",
            [CallerFilePath] string filePath = "",
            [CallerLineNumber] int lineNumber = 0) => logger.Log(LoggerSourceFormatter.Format(message, origin, filePath, lineNumber, exception), LogLevel.Debug);

        /// <summary>
        /// Logs a informative message, including the source of the log
        /// </summary>
        /// <param name="logger">The logger</param>
        /// <param name="message">The message</param>
        /// <param name="exception">The exception</param>
        /// <param name="origin">The callers member/function name</param>
        /// <param name="filePath">The source code file path</param>
        /// <param name="lineNumber">The line number in the code file of the caller</param>
        public static void LogInformationSource(
            this ICompositeLogger logger,
            string message,
            Exception exception = null,
            [CallerMemberName] string origin = "",
            [CallerFilePath] string filePath = "",
            [CallerLineNumber] int lineNumber = 0) => logger.Log(LoggerSourceFormatter.Format(message, origin, filePath, lineNumber, exception), LogLevel.Informative);

        /// <summary>
        /// Logs a error message, including the source of the log
        /// </summary>
        /// <param name="logger">The logger</param>
        /// <param name="message">The message</param>
        /// <param name="exception">The exception</param>
        /// <param name="origin">The callers member/function name</param>
        /// <param name="filePath">The source code file path</param>
        /// <param name="lineNumber">The line number in the code file of the caller</param>
        public static void LogErrorSource(
            this ICompositeLogger logger,
            string message,
            Exception exception = null,
            [CallerMemberName] string origin = "",
            [CallerFilePath] string filePath = "",
            [CallerLineNumber] int lineNumber = 0) => logger.Log(LoggerSourceFormatter.Format(message, origin, filePath, lineNumber, exception), LogLevel.Exception);

        /// <summary>
        /// Logs a warning message, including the source of the log
        /// </summary>
        /// <param name="logger">The logger</param>
        /// <param name="message">The message</param>
        /// <param name="exception">The exception</param>
        /// <param name="origin">The callers member/function name</param>
        /// <param name="filePath">The source code file path</param>
        /// <param name="lineNumber">The line number in the code file of the caller</param>
        public static void LogWarningSource(
            this ICompositeLogger logger,
            string message,
            Exception exception = null,
            [CallerMemberName] string origin = "",
            [CallerFilePath] string filePath = "",
            [CallerLineNumber] int lineNumber = 0) => logger.Log(LoggerSourceFormatter.Format(message, origin, filePath, lineNumber, exception), LogLevel.Warning);
        
    }
}
