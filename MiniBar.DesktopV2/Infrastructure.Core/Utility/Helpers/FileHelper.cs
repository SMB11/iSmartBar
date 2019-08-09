using System;
using System.IO;

namespace Infrastructure.Utility
{
    public static class FileHelper
    {
        public static string GetAvailableFilename(string filePath)
        {
            string available = filePath;
            int i = 1;
            while (File.Exists(available))
            {
                string newFileName = Path.GetFileNameWithoutExtension(filePath) + " (" + i++ + ")";
                available = Path.Combine(Path.GetPathRoot(filePath),  newFileName + Path.GetExtension(filePath));
            }
            return available;
        }

        public static bool TrySaveStreamToFile(Stream stream, string filename, FileMode mode)
        {
            using (FileStream fs = new FileStream(filename, FileMode.CreateNew))
            {
                stream.Position = 0;
                stream.CopyTo(fs);
                stream.Dispose();
                stream = null;
            }
            return true;
        }
    }
}
