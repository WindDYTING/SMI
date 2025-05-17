using System;

namespace SMI.Core {
    public interface IStrategyFactory
    {
        ICrawlerStrategy GetStrategy(Type type);
    }
}
