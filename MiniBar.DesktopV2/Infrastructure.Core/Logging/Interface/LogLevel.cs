using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Logging
{
    
    /// <summary>
    /// The severity of the log message
    /// </summary>
    public enum LogLevel
    {
        /// <summary>
        /// Developer-specific information
        /// </summary>
        Debug = 1,

        /// <summary>
        /// Exception information
        /// </summary>
        Exception = 2,

        /// <summary>
        /// General information
        /// </summary>
        Informative = 3,

        /// <summary>
        /// A warning
        /// </summary>
        Warning = 4
    }
}

