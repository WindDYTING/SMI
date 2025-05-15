using System.Collections.Generic;
using System.Threading.Tasks;

namespace SMI.Core {
    public static class CrawlerExtensions {
        public static Task<IList<ImageResult>> GetReportsImage(this ICrawler crawler, int notionKind, string queryRange, int fromPageStart = 0)
        {
            return crawler.GetReports<ImageResult>(notionKind, queryRange, fromPageStart);
        }

        public static Task<IList<KeyValueCollections>> GetReportsText(this ICrawler crawler, int notionKind,
            string queryRange, int fromPageStart=0)
        {
            return crawler.GetReports<KeyValueCollections>(notionKind, queryRange, fromPageStart);
        }

        public static Task<IList<ImageResult>> GetRangeReportsImage(this ICrawler crawler, int notionKind,
            string fromDate, string toDate, int fromPageStart = 0)
        {
            return crawler.GetReportsRange<ImageResult>(notionKind, fromDate, toDate, fromPageStart);
        }

        public static Task<IList<KeyValueCollections>> GetRangeReportsText(this ICrawler crawler, int notionKind,
            string fromDate, string toDate, int fromPageStart = 0)
        {
            return crawler.GetReportsRange<KeyValueCollections>(notionKind, fromDate, toDate, fromPageStart);
        }
    }
}
