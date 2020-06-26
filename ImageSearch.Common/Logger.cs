using System.IO;
using System.Reflection;

namespace ImageSearch.Common
{
    /// <summary>
    /// Static logger class to log events to file.
    /// Currently only file supporte. Only default level of logging
    /// </summary>
    public static class Logger
    {
        private static readonly object lockObject = new object();
        private static readonly string logFilePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "Log.txt";

        /// <summary>
        /// Logs the passed string to the file.
        /// </summary>
        /// <param name="textToLog"></param>
        public static void Log(string textToLog)
        {
            lock (lockObject)
            {
                using (StreamWriter streamWriter = new StreamWriter(logFilePath))
                {
                    streamWriter.WriteLine(textToLog);
                    streamWriter.Close();
                }
            }
        }
    }
}
