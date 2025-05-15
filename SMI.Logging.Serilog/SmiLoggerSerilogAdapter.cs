using SMI.Core;
using SMI.Core.Logging;

namespace SMI.Logging.Serilog {
    public class SmiLoggerSerilogAdapter : ILogger {
        private readonly global::Serilog.ILogger _adaptee;

        public SmiLoggerSerilogAdapter(global::Serilog.ILogger adaptee)
        {
            _adaptee = adaptee;
        }

        public void Log(LoggingLevel level, string msg, Exception? ex = null)
        {
            _adaptee.ForContext("SourceContext", "SMI")
                .Write(level switch {
                    LoggingLevel.Err => global::Serilog.Events.LogEventLevel.Error,
                    LoggingLevel.Warn => global::Serilog.Events.LogEventLevel.Warning,
                    LoggingLevel.Info => global::Serilog.Events.LogEventLevel.Information,
                    LoggingLevel.Debug => global::Serilog.Events.LogEventLevel.Debug,
                    _ => throw new ArgumentOutOfRangeException(nameof(level), level, null)
                }, msg, ex);
        }
    }
}
