using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CreateWordFromWinForm.Model
{
    public class Logger
    {
        //Log file with path
        string errorLogFile = Application.StartupPath + Path.DirectorySeparatorChar + Config.ERROR_LOG_FILE;

        //Different level of log
        private enum LogLevel
        {
            INFO,
            WARNING,
            ERROR
        }

        //Pass false to create new log file
        public Logger(bool append = true)
        {
            // Log file header line
            string logHeader = Config.ERROR_LOG_FILE + " is created.";
            

            if (!File.Exists(errorLogFile))
            {
                WriteLine(DateTime.Now.ToString(Config.FORMAT_LOGGER_DATETIME) + " " + logHeader, false);
            }
            else
            {
                if (append == false)
                    WriteLine(DateTime.Now.ToString(Config.FORMAT_LOGGER_DATETIME) + " " + logHeader, false);
            }
        }

        //For logging errors
        public void Error(string message)
        {
            WriteFormattedLog(LogLevel.ERROR, message);
        }

        //For logging warnings
        public void Warn(string message)
        {
            WriteFormattedLog(LogLevel.WARNING, message);
        }

        //For logging info
        public void Info(string message)
        {
            WriteFormattedLog(LogLevel.INFO, message);
        }

        //Format message based on logging level
        private void WriteFormattedLog(LogLevel level, string text)
        {
            string pretext;
            switch (level)
            {
                case LogLevel.INFO: pretext = DateTime.Now.ToString(Config.FORMAT_LOGGER_DATETIME) + " [INFO]    "; break;
                case LogLevel.WARNING: pretext = DateTime.Now.ToString(Config.FORMAT_LOGGER_DATETIME) + " [WARNING] "; break;
                case LogLevel.ERROR: pretext = DateTime.Now.ToString(Config.FORMAT_LOGGER_DATETIME) + " [ERROR]   "; break;
                default: pretext = ""; break;
            }

            WriteLine(pretext + text);
        }

        //Write the formatted log message
        private void WriteLine(string text, bool append = true)
        {
            try
            {
                using (StreamWriter Writer = new StreamWriter(errorLogFile, append, Encoding.UTF8))
                {
                    if (text != "") Writer.WriteLine(text);
                }
            }
            catch
            {
                throw;
            }
        }
    }
}
