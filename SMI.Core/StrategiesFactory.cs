using System;
using SMI.Core.CrawlerStrategies;

namespace SMI.Core {
    public static class StrategiesFactory {
        public static ICrawlerStrategy GetStrategy(Type type)
        {
            if (type == typeof(KeyValueCollections))
                return new KeyValueResultStrategy();
            else
                return new ImageResultStrategy();
        } 
    }
}
