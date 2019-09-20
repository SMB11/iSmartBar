using Infrastructure.Interface;
using System;

namespace Infrastructure.Logging
{
    public static class LoggingExtensions
    {
        public static void LogWithWorkitemData(this ICompositeLogger logger, string message, LogLevel level, IWorkItem workItem, Exception ex = null)
        {
            if (!String.IsNullOrEmpty(workItem.WorkItemID))
                message = $"{message} | Workitem {workItem.WorkItemName} with ID {workItem.WorkItemID}";
            if (ex != null)
                message = $"{message} | {ex.ToString()}";
            logger.Log(message, level);
        }
    }
}
