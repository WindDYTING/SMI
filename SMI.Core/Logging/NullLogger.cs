using System;

namespace SMI.Core.Logging {
    public class NullLogger : ILogger {
        public void Log(LoggingLevel level, string msg, Exception ex = null)
        {
            
        }
    }
}
