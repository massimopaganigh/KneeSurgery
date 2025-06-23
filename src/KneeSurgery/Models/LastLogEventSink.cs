using Serilog.Events;

namespace KneeSurgery.Models
{
    public class LastLogEventSink : Serilog.Core.ILogEventSink
    {
        public void Emit(LogEvent logEvent)
        {
            LastLogMessage = logEvent.RenderMessage();
        }

        public static string? LastLogMessage { get; private set; }
    }
}