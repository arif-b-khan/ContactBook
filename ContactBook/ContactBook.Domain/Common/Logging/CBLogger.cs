using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLog;
using ContactBook.Domain.Common.Logging;
  
namespace ContactBook.Domain.Common.Logging
{
    public class CBLogger : ICBLogger 
    {
        static Lazy<Logger> lazyLogger;
        
        const string APPNAME = "ApplicationName";
        const string CONFIG_APPNAME = "AppName";
        const string CONFIG_LOGGERNAME = "LoggerName";

        string messageFormat = "Application Name: {0} \nMessage:{1}";
        static string loggerName = string.Empty;
        static CBLogger()
        {
            string appName = ConfigurationManager.AppSettings.Get(CONFIG_APPNAME);
            loggerName = ConfigurationManager.AppSettings.Get(CONFIG_LOGGERNAME);
            lazyLogger = new Lazy<Logger>(() =>
            {
                if (!string.IsNullOrEmpty(loggerName))
                {
                    return LogManager.GetLogger(loggerName);
                }
                return LogManager.GetLogger("TempLogger");
            }, true);

            if (!string.IsNullOrEmpty(appName))
            {
                GlobalDiagnosticsContext.Set(APPNAME, appName);
            }
            else
            {
                GlobalDiagnosticsContext.Set(APPNAME, "TempAppName");
            }
        }
        
        private static string AppName
        {
            get
            {
                return GlobalDiagnosticsContext.Get(APPNAME);
            }
        }

        public static Logger Instance
        {
            get
            {
                return lazyLogger.Value;
            }
        }

        public void Debug(string message)
        {
            Instance.Debug(string.Format(messageFormat, AppName, message));
        }

        public void Debug(string message, Exception ex)
        {
            Instance.Debug(string.Format(messageFormat, AppName, message), ex);
        }

        public void Error(string message)
        {
            Instance.Error(string.Format(messageFormat, AppName, message));
        }

        public void Error(string message, Exception ex)
        {
            Instance.Error(string.Format(messageFormat, AppName, message), ex);
        }

        public void Warn(string message)
        {
            Instance.Warn(string.Format(messageFormat, AppName, message));
        }

        public void Warn(string message, Exception ex)
        {
            Instance.Warn(string.Format(messageFormat, AppName, message), ex);
        }

        public void Info(string message)
        {
            Instance.Info(string.Format(messageFormat, AppName, message));
        }

        public void Info(string message, Exception ex)
        {
            Instance.Info(string.Format(messageFormat, AppName, message), ex);
        }

        public void Trace(string message)
        {
            Instance.Trace(string.Format(messageFormat, AppName, message));
        }

        public void Trace(string message, Exception ex)
        {
            Instance.Trace(string.Format(messageFormat, AppName, message), ex);
        }

        public void Fatal(string message)
        {
            Instance.Fatal(string.Format(messageFormat, AppName, message));
        }

        public void Fatal(string message, Exception ex)
        {
            Instance.Fatal(string.Format(messageFormat, AppName, message), ex);
        }

    }
}
