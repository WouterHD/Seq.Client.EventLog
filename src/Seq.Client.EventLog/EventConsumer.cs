using Serilog;
using Serilog.Events;

namespace Seq.Client.EventLog
{
    static class EventConsumer
    {
        public static void PostRawEvents(RawEvents rawEvents)
        {
            foreach(var a in rawEvents.Events)
            {
                switch(a.Level)
                {
                    case LogEventLevel.Information:
                        Log.Logger.Information(a.MessageTemplate, a.Properties);
                        break;

                    case LogEventLevel.Warning:
                        Log.Logger.Warning(a.MessageTemplate, a.Properties);
                        break;

                    case LogEventLevel.Error:
                        Log.Logger.Error(a.MessageTemplate, a.Properties);
                        break;

                    case LogEventLevel.Debug:
                        Log.Logger.Debug(a.MessageTemplate, a.Properties);
                        break;

                    case LogEventLevel.Verbose:
                        Log.Logger.Verbose(a.MessageTemplate, a.Properties);
                        break;

                    default:
                        break;
                }
            }
        }
    }
}
