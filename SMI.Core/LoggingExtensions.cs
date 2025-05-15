using System;
using SMI.Core.Logging;

namespace SMI.Core {
    public static class LoggingExtensions {
        public static void Err(this ILogger logger, string msg, Exception? ex = null)
        {
            logger.Log(LoggingLevel.Err, msg, ex);
        }

        public static void Warn(this ILogger logger, string msg, Exception? ex = null)
        {
            logger.Log(LoggingLevel.Warn, msg, ex);
        }

        public static void Info(this ILogger logger, string msg, Exception? ex = null)
        {
            logger.Log(LoggingLevel.Info, msg, ex);
        }

        public static void Debug(this ILogger logger, string msg, Exception? ex = null)
        {
            logger.Log(LoggingLevel.Debug, msg, ex);
        }
    }
}
