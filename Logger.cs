using System;
using System.IO;
using System.Reflection;

namespace WebScraper
{
    public class LogWriter
    {
        private static string _exePath;
        private static string _logPath;
        public LogWriter(string logName)
        {
            _exePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            _logPath = _exePath + "\\" + $"{logName}.txt";
        }
        public void LogWrite(string logMessage)
        {
            try
            {
                FileInfo logFile = new FileInfo(_logPath);
                if(logFile.Length > 2000000)
                {
                   File.Delete(_logPath);
                }
                using (StreamWriter w = File.AppendText(_logPath))
                {
                    Log(logMessage, w);
                }
            }
            catch (Exception ex)
            {
            }
        }

        public void Log(string logMessage, TextWriter txtWriter)
        {
            try
            {
                txtWriter.Write("\r\n");
                txtWriter.WriteLine("{0} {1} :", DateTime.Now.ToLongTimeString(),
                    DateTime.Now.ToLongDateString());
                txtWriter.WriteLine("{0}", logMessage);
                txtWriter.WriteLine("-------------------------------");
            }
            catch (Exception ex)
            {
            }
        }
    }
}