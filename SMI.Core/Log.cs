using SMI.Core.Logging;

namespace SMI.Core {
    public static class Log
    {
        public static ILogger Null = new NullLogger();
        public static ILogger Console = new ConsoleLogger();
        public static ILogger Default = Console;
    }
}
