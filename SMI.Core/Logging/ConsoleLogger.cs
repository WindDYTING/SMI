using System;

namespace SMI.Core.Logging {
    public class ConsoleLogger : ILogger {
        
        public virtual void Log(LoggingLevel level, string msg, Exception? ex = null)
        {
            var originalColor = Console.ForegroundColor;
            try
            {
                Console.ForegroundColor = DetermineColor(level);
                string logMessage = $"[{level.ToString()}] {DateTime.Now:yyyy/MM/dd HH:mm:ss.fff} {msg}";
                if (ex is not null)
                {
                    logMessage = $"{logMessage} {ex}";
                }
                Console.WriteLine(logMessage);
            }
            finally
            {
                Console.ForegroundColor = originalColor;
            }
        }

        private ConsoleColor DetermineColor(LoggingLevel level)
        {
            return level switch
            {
                LoggingLevel.Err => ConsoleColor.Red,
                LoggingLevel.Warn => ConsoleColor.Yellow,
                LoggingLevel.Info => ConsoleColor.White,
                LoggingLevel.Debug => ConsoleColor.DarkGray,
                _ => throw new ArgumentOutOfRangeException(nameof(level), level, null)
            };
        }
    }
}
