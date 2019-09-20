using System;
using System.Collections.Concurrent;
using System.IO;

namespace Infrastructure.Logging
{
    /// <summary>
    /// A logger that writes the logs to file
    /// </summary>
    public class FileLogger : ILogger
    {
        #region Static Properties

        /// <summary>
        /// A list of file locks based on path
        /// </summary>
        protected static ConcurrentDictionary<string, object> FileLocks = new ConcurrentDictionary<string, object>();

        /// <summary>
        /// The lock to lock the list of locks
        /// </summary>
        protected static object FileLockLock = new object();

        #endregion

        #region Protected Members

        /// <summary>
        /// The category for this logger
        /// </summary>
        protected readonly string mCategoryName;

        /// <summary>
        /// The file path to write to
        /// </summary>
        protected readonly string mFilePath;

        /// <summary>
        /// The directory the file is in
        /// </summary>
        protected readonly string mDirectory;


        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="filePath">The file path to write to</param>
        public FileLogger(string filePath)
        {
            // Get absolute path
            filePath = Path.GetFullPath(filePath);

            // Set members
            mFilePath = filePath;
            mDirectory = Path.GetDirectoryName(filePath);
        }

        #endregion

        /// <summary>
        /// Logs the message to file
        /// </summary>
        public void Log(string message, LogLevel level)
        {
            var currentTime = DateTimeOffset.Now.ToString("yyyy-MM-dd hh:mm:ss");


            // Prepend the time to the log if desired
            var timeLogString = $"[{currentTime}] ";

            // Write the message
            var output = $"{timeLogString}{message}{Environment.NewLine}";

            // Normalize path
            // TODO: Make use of configuration base path
            var normalizedPath = mFilePath.ToUpper();

            var fileLock = default(object);

            // Double safety even though the FileLocks should be thread safe
            lock (FileLockLock)
            {
                // Get the file lock based on the absolute path
                fileLock = FileLocks.GetOrAdd(normalizedPath, path => new object());
            }

            // Lock the file
            lock (fileLock)
            {
                // Ensure folder
                if (!Directory.Exists(mDirectory))
                    Directory.CreateDirectory(mDirectory);

                // Open the file
                using (var fileStream = new StreamWriter(File.Open(mFilePath, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite)))
                {
                    // Go to end
                    fileStream.BaseStream.Seek(0, SeekOrigin.End);


                    // Write the message to the file
                    fileStream.Write(output);
                }
            }
        }
    }
}
