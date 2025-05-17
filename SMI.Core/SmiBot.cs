using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SMI.Core {
    public class SmiBot : SmiBotBase<SubscribeInfo>
    {
        protected override async Task<IList<object>> CrawlerGetReports(SubscribeInfo info)
        {
            var returnList = new List<object>();
            if (info.ReceivedNewsType == ReturnType.Text)
            {
                var ret = await CrawlerCore.GetReportsText((int)info.ObservationNews, "1");
                returnList.AddRange(ret);
            }
            else if (info.ReceivedNewsType == ReturnType.Image)
            {
                var ret = await CrawlerCore.GetReportsImage((int)info.ObservationNews, "1");
                returnList.AddRange(ret);
            }

            return returnList;
        }

        public void Subscribe(NotionKind notionKind, ReturnType type = ReturnType.Text)
        {
            var subscribeInfo = new SubscribeInfo
            {
                ObservationNews = notionKind,
                ReceivedNewsType = type
            };
            base.Subscribe(subscribeInfo);
        }

        public void Unsubscribe(NotionKind notionKind, ReturnType type = ReturnType.Text)
        {
            var subscribeInfo = new SubscribeInfo
            {
                ObservationNews = notionKind,
                ReceivedNewsType = type
            };
            base.Unsubscribe(subscribeInfo);
        }


        public async Task<IList<KeyValueCollections>> GetReportsRange(NotionKind notionKind, DateTime startDate, DateTime endDate) {
            Results.Clear();
            Results.AddRange(await CrawlerCore.GetRangeReportsText((int)notionKind, startDate.ToTaiwanDateString(), endDate.ToTaiwanDateString()));
            return Results.FilterType<KeyValueCollections>().ToList();
        }

        public async Task<IList<KeyValueCollections>> GetReports(NotionKind notionKind, QueryRange range) {
            Results.Clear();
            Results.AddRange(await CrawlerCore.GetReportsText((int)notionKind, ((int)range).ToString()));
            return Results.FilterType<KeyValueCollections>().ToList();
        }

        public async Task<IList<ImageResult>> GetReportImagesRange(NotionKind notionKind, DateTime startDate, DateTime endDate) {
            Results.Clear();
            Results.AddRange(await CrawlerCore.GetRangeReportsImage((int)notionKind, startDate.ToTaiwanDateString(), endDate.ToTaiwanDateString()));
            return Results.FilterType<ImageResult>().ToList();
        }

        public async Task<IList<ImageResult>> GetReportImages(NotionKind notionKind, QueryRange range) {
            Results.Clear();
            Results.AddRange(await CrawlerCore.GetReportsImage((int)notionKind, ((int)range).ToString()));
            return Results.FilterType<ImageResult>().ToList();
        }
    }
}
