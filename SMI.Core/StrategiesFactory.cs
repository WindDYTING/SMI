using System;
using SMI.Core.CrawlerStrategies;

namespace SMI.Core {
    public static class StrategiesFactory {
        public static ICrawlerStrategy GetDefaultStrategy(Type type)
        {
            if (type == typeof(KeyValueCollections))
                return new KeyValueResultStrategy();
            if (type == typeof(ImageResult))
                return new ImageResultStrategy();

            return null;
        } 
    }
}
