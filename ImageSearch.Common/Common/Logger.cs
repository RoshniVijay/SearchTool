using System.IO;
using System.Reflection;

namespace SearchTool.Common
{
    /// <summary>
    /// Static logger class to log events to file.
    /// Currently only file supported. Only default level of logging
    /// </summary>
    public static class Logger
    {
        private static readonly object LockObject = new object();
        private static readonly string LogFilePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\Log.txt";

        /// <summary>
        /// Logs the passed string to the file.
        /// </summary>
        /// <param name="textToLog"></param>
        public static void Log(string textToLog)
        {
            lock (LockObject)
            {
                using (StreamWriter streamWriter = new StreamWriter(LogFilePath))
                {
                    streamWriter.WriteLine(textToLog);
                    streamWriter.Close();
                }
            }
        }
    }
}
