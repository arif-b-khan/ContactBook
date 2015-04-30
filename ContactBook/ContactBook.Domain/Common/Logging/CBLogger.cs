using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLog;

namespace ContactBook.Domain.Common.Logging
{
    public class CBLogger 
    {
        static Lazy<Logger> lazyLogger;

        static CBLogger()
        {
            string appName = ConfigurationManager.AppSettings.Get("AppName");
            string loggerName = ConfigurationManager.AppSettings.Get("LoggerName");
            lazyLogger = new Lazy<Logger>(() =>
            {
                if (string.IsNullOrEmpty(loggerName))
                {
                    return LogManager.GetLogger(loggerName);
                }
                return LogManager.GetLogger("TempLogger");
            }, true);

            if (string.IsNullOrEmpty(appName))
            {
                GlobalDiagnosticsContext.Set("ApplicationName", appName);
            }
            else
            {
                GlobalDiagnosticsContext.Set("ApplicationName", "TempAppName");
            }
        }

        public static Logger Instance
        {
            get
            {
                return lazyLogger.Value;
            }
        }
    }
}
