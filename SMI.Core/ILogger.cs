#nullable enable
using System;
using SMI.Core.Logging;

namespace SMI.Core {
    public interface ILogger
    {
        void Log(LoggingLevel level, string msg, Exception? ex = null);
    }
}
