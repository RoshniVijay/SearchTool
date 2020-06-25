using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ImageSearch.Common
{
    public static class Logger
    {
        private static readonly object lockObject = new object();
        private static readonly string logFilePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "Log.txt";

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
