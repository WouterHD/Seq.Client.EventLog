using Serilog;
using Serilog.Context;
using Serilog.Events;
using Serilog.Parsing;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Seq.Client.EventLog
{
    static class EventConsumer
    {
        public static void PostRawEvents(RawEvents rawEvents)
        {
            foreach(var rawEvent in rawEvents.Events)
            {
                Exception exception = null;
                if (!string.IsNullOrEmpty(rawEvent.Exception))
                {
                    exception = new Exception(rawEvent.Exception);
                }

                var template = new MessageTemplateParser().Parse(rawEvent.MessageTemplate);
                var properties = new List<LogEventProperty>();
                foreach(var prop in rawEvent.Properties)
                {
                    properties.Add(new LogEventProperty(prop.Key, new ScalarValue(prop.Value)));
                }
                
                properties.Add(new LogEventProperty("Timestamp", new ScalarValue(rawEvent.Timestamp)));

                var logEvent = new LogEvent(rawEvent.Timestamp, rawEvent.Level, exception, template, properties);

                Log.Logger.Write(logEvent);
            }
        }
    }
}
