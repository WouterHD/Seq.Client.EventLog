using Serilog;
using Serilog.Context;
using Serilog.Events;
using System.Threading.Tasks;

namespace Seq.Client.EventLog
{
    static class EventConsumer
    {
        public static void PostRawEvents(RawEvents rawEvents)
        {
            foreach(var rawEvent in rawEvents.Events)
            {
                PostEvent(rawEvent);
            }
        }

        private static async void PostEvent(RawEvent rawEvent)
        {
            await Task.Run(() => {
                LogContext.PushProperty("Timestamp", rawEvent.Timestamp);
                LogContext.PushProperty("Level", rawEvent.Level);
                foreach (var prop in rawEvent.Properties)
                {
                    LogContext.PushProperty(prop.Key, prop.Value);
                }

                switch (rawEvent.Level)
                {
                    case LogEventLevel.Information:
                        Log.Logger.Information(rawEvent.MessageTemplate, rawEvent.Properties);
                        break;

                    case LogEventLevel.Warning:
                        Log.Logger.Warning(rawEvent.MessageTemplate, rawEvent.Properties);
                        break;

                    case LogEventLevel.Error:
                        Log.Logger.Error(rawEvent.MessageTemplate, rawEvent.Properties);
                        break;

                    case LogEventLevel.Debug:
                        Log.Logger.Debug(rawEvent.MessageTemplate, rawEvent.Properties);
                        break;

                    case LogEventLevel.Verbose:
                        Log.Logger.Verbose(rawEvent.MessageTemplate, rawEvent.Properties);
                        break;

                    default:
                        break;
                }
            });
        }
    }
}
