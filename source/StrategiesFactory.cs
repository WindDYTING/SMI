using SMI.CrawlerStrategies;

namespace SMI {
    public static class StrategiesFactory {
        public static ICrawlerStrategy GetStrategy(int notionKind)
        {
            return new KeyValueResultStrategy();
        } 
    }
}
