using System;
using SMI.CrawlerStrategies;
using SMI.Models;

namespace SMI {
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
