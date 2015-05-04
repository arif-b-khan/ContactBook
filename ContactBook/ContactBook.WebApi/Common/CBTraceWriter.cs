using ContactBook.Domain.IoC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Tracing;
using ContactBook.Domain.Common.Logging;
using System.Text;

namespace ContactBook.WebApi.Common
{
    public class CBTraceWriter : ITraceWriter
    {
        private static readonly ICBLogger cbLogger = DependencyFactory.Resolve<ICBLogger>();
        private static Lazy<Dictionary<TraceLevel, Action<string>>> logger = new Lazy<Dictionary<TraceLevel, Action<string>>>(() =>
        {
            var retLogger = new Dictionary<TraceLevel, Action<string>>()
            {
                {TraceLevel.Info, cbLogger.Info},
                {TraceLevel.Debug, cbLogger.Debug},
                {TraceLevel.Error, cbLogger.Error},
                {TraceLevel.Warn, cbLogger.Warn},
                {TraceLevel.Fatal, cbLogger.Fatal}
            };
            return retLogger;
        }, true);

        public void Trace(System.Net.Http.HttpRequestMessage request, string category, TraceLevel level, Action<TraceRecord> traceAction)
        {
            if (level != TraceLevel.Off)
            {
                var traceRecord = new TraceRecord(request, category, level);
                traceAction(traceRecord);
                Log(traceRecord);
            }
        }

        private Dictionary<TraceLevel, Action<string>> Logger
        {
            get
            {
                return logger.Value;
            }
        }

        public void Log(TraceRecord record)
        {
            var message = new StringBuilder();

            if (record.Request != null)
            {
                if (record.Request.Method != null)
                    message.Append(record.Request.Method);

                if (record.Request.RequestUri != null)
                    message.Append(" ").Append(record.Request.RequestUri);
            }

            if (!string.IsNullOrWhiteSpace(record.Category))
                message.Append(" ").Append(record.Category);

            if (!string.IsNullOrWhiteSpace(record.Operator))
                message.Append(" ").Append(record.Operator).Append(" ").Append(record.Operation);

            if (!string.IsNullOrWhiteSpace(record.Message))
                message.Append(" ").Append(record.Message);

            if (record.Exception != null && !string.IsNullOrWhiteSpace(record.Exception.GetBaseException().Message))
                message.Append(" ").Append(record.Exception.GetBaseException().Message);
            
            Logger[record.Level](message.ToString());
        }
    }
}