using Serilog.Events;
using System.Collections.Generic;
using System.Diagnostics;

namespace Seq.Client.EventLog
{
    public static class Extensions
    {
        private static LogEventLevel MapLogLevel(EventLogEntryType type)
        {
            return type switch
            {
                EventLogEntryType.Information  => LogEventLevel.Information,
                EventLogEntryType.Warning      => LogEventLevel.Warning,
                EventLogEntryType.Error        => LogEventLevel.Error,
                EventLogEntryType.SuccessAudit => LogEventLevel.Information,
                EventLogEntryType.FailureAudit => LogEventLevel.Warning,
                _                              => LogEventLevel.Debug,
            };
        }

        public static RawEvents ToDto(this EventLogEntry entry, string logName)
        {
            return new RawEvents
            {
                Events = new[]
                {
                    new RawEvent
                    {
                        Timestamp = entry.TimeGenerated,
                        Level = MapLogLevel(entry.EntryType),
                        MessageTemplate = entry.Message,
                        Properties = new Dictionary<string, object>
                        {
                            { "MachineName", entry.MachineName },
#pragma warning disable 618
                            { "EventId", entry.EventID },
#pragma warning restore 618
                            { "InstanceId", entry.InstanceId },
                            { "Source", entry.Source },
                            { "Category", entry.CategoryNumber },
                            { "EventLogName", logName }
                        }
                    },
                }
            };
        }
    }
}
