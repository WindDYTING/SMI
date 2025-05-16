using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SMI.Core {
    public class SmiBot : IDisposable, IAsyncDisposable {
        private readonly IGuarantor _guarantor;
        private readonly IDictionary<string, INewsNotification> _notifications = new Dictionary<string, INewsNotification>();
        private readonly IList<SubscribeInfo> _subscribes = new List<SubscribeInfo>();

        public ILogger Logger { get; set; } = Log.Default;
        public ICrawler Crawler { get; }
        public ITimerService TimeService { get; }

        public event EventHandler BeforeTimeIsUp;
        public event EventHandler AfterTimeIsUp;
        public event EventHandler<BeforeNotifyKeyValueCollectionsEventArgs> BeforeNotifyKeyValueCollections; 
        public event EventHandler<NotifyKeyValueCollectionEventArgs> NotifyEveryKeyValueCollection;
        public event EventHandler<NotifyImageEventArgs> NotifyEveryImage; 

        public SmiBot()
        {
            Crawler = new Crawler(Logger);
            Crawler.Options.SetUrl(Router.ShareholdingMeetReportsUri);
            _guarantor = new Guarantor(Crawler);
            _guarantor.MessageReceived += OnMessageReceived;
            TimeService = new TimerService(new TimeOnly(21, 30));
            TimeService.TimeIsUp += OnTimeIsUp;
        }

        public SmiBot(ICrawler crawler)
        {
            Crawler = crawler;
            Crawler.Options.SetUrl(Router.ShareholdingMeetReportsUri);
            _guarantor = new Guarantor(crawler);
            _guarantor.MessageReceived += OnMessageReceived;
            TimeService = new TimerService(new TimeOnly(21, 30));
            TimeService.TimeIsUp += OnTimeIsUp;
        }

        public SmiBot(ICrawler crawler, Uri crawUri)
        {
            Crawler = crawler;
            Crawler.Options.SetUrl(crawUri);
            _guarantor = new Guarantor(crawler);
            _guarantor.MessageReceived += OnMessageReceived;
            TimeService = new TimerService(new TimeOnly(21, 30));
            TimeService.TimeIsUp += OnTimeIsUp;
        }

        public Task InitAsync()
        {
            return Crawler.InitAsync();
        }

        public void RaiseNotify(IList<KeyValueCollections> keyValueCollections, IList<ImageResult> images=null)
        {
            var news = new List<object>();
            news.AddRange(keyValueCollections);
            if (images is not null) news.AddRange(images);
            OnNotify(news);
        }

        public void AddNotification(string name, INewsNotification notification)
        {
            _notifications.TryAdd(name, notification);
        }

        public void RemoveNotification(string name, bool dispose=true)
        {
            if (_notifications.Remove(name, out INewsNotification notification))
            {
                if (!dispose) return;
                if (notification is IDisposable asyncDisposable)
                {
                    asyncDisposable.Dispose();
                }
            }
        }

        public void Subscribe(NotionKind notionKind, ReturnType type = ReturnType.Text)
        {
            var subscribeInfo = new SubscribeInfo
            {
                ObservationNews = notionKind,
                ReceivedNewsType = type
            };
            if (_subscribes.IndexOf(subscribeInfo) == -1)
            {
                _subscribes.Add(subscribeInfo);
            }
        }

        public void Unsubscribe(NotionKind notionKind, ReturnType type = ReturnType.Text)
        {
            var subscribeInfo = new SubscribeInfo
            {
                ObservationNews = notionKind,
                ReceivedNewsType = type
            };
            var index = _subscribes.IndexOf(subscribeInfo);
            if (index != -1)
            {
                _subscribes.RemoveAt(index);
            }
        }

        protected virtual void OnBeforeTimeIsUp(EventArgs e) => BeforeTimeIsUp?.Invoke(this, e);
        protected virtual void OnAfterTimeIsUp(EventArgs e) => AfterTimeIsUp?.Invoke(this, e);

        protected virtual void OnNotify(IList<object> news)
        {
            try
            {
                var keyValueCollections = news.FilterType<KeyValueCollections>().ToList();
                var images = news.FilterType<ImageResult>().ToList();
                OnNotify(images, keyValueCollections);

                var remainder = news.Except(keyValueCollections).Except(images);
                if (remainder.Any())
                {
                    Logger.Warn("There are still elements left that have not been notified");
                }
            }
            catch (Exception ex)
            {
                Logger.Err("OnNotify occur exception", ex);
            }
        }

        private void OnNotify(IList<ImageResult> images, IList<KeyValueCollections> keyValueCollections)
        {
            foreach (var kvp in _notifications)
            {
                var (_, notification) = (kvp.Key, kvp.Value);
                try
                {
                    OnNotifyPhotos(notification, images);
                    OnNotifyKeyValueCollections(notification, keyValueCollections);
                }
                catch (Exception ex1)
                {
                    Logger.Warn("Has INewNotification occur exception", ex1);
                }
            }
        }


        protected virtual void OnNotifyPhotos(INewsNotification notification, IList<ImageResult> images)
        {
            foreach (var image in images)
            {
                OnNotifyEveryImage(new NotifyImageEventArgs(image, notification));
            }
        }

        protected virtual void OnNotifyKeyValueCollections(INewsNotification notification, IList<KeyValueCollections> collections)
        {
            var args = new BeforeNotifyKeyValueCollectionsEventArgs(notification, false, collections);
            BeforeNotifyKeyValueCollections?.Invoke(this, args);
            if (args.Handle)
            {
                return;
            }

            foreach (var dict in collections)
            {
                OnNotifyEveryKeyValueCollection(new NotifyKeyValueCollectionEventArgs(notification, dict));
            }
        }

        protected virtual void OnNotifyEveryImage(NotifyImageEventArgs e)
        {
            NotifyEveryImage?.Invoke(this, e);
        }

        protected virtual void OnNotifyEveryKeyValueCollection(NotifyKeyValueCollectionEventArgs e)
        {
            NotifyEveryKeyValueCollection?.Invoke(this, e);
        }

        private async void OnTimeIsUp(object sender, EventArgs e)
        {
            try
            {
                OnBeforeTimeIsUp(EventArgs.Empty);
                foreach (var subscribeInfo in _subscribes)
                {
                    var ret = await CrawlerGetReports(subscribeInfo);
                    OnNotify(ret);
                }
                OnAfterTimeIsUp(EventArgs.Empty);
            }
            catch (Exception ex)
            {
                Logger.Err("OnTimeIsUp event occur exception", ex);
            }
        }

        private async Task<IList<object>> CrawlerGetReports(SubscribeInfo info)
        {
            var returnList = new List<object>();
            if (info.ReceivedNewsType == ReturnType.Text)
            {
                var ret = await Crawler.GetReportsText((int)info.ObservationNews, "1");
                returnList.AddRange(ret);
            }
            else if (info.ReceivedNewsType == ReturnType.Image)
            {
                var ret = await Crawler.GetReportsImage((int)info.ObservationNews, "1");
                returnList.AddRange(ret);
            }

            return returnList;
        }

        private List<object> _results { get; } = new List<object>();

        private void OnMessageReceived(object sender, MessageReceivedEventArgs e)
        {
            if (e.Result is IList<object> result)
            {
                _results.AddRange(result);
            }
        }

        public async Task<IList<KeyValueCollections>> GetReportsRange(NotionKind notionKind, DateTime startDate, DateTime endDate) {
            _results.Clear();
            _results.AddRange(await Crawler.GetRangeReportsText((int)notionKind, startDate.ToTaiwanDateString(), endDate.ToTaiwanDateString()));
            return _results.FilterType<KeyValueCollections>().ToList();
        }

        public async Task<IList<KeyValueCollections>> GetReports(NotionKind notionKind, QueryRange range) {
            _results.Clear();
            _results.AddRange(await Crawler.GetReportsText((int)notionKind, ((int)range).ToString()));
            return _results.FilterType<KeyValueCollections>().ToList();
        }

        public async Task<IList<ImageResult>> GetReportImagesRange(NotionKind notionKind, DateTime startDate, DateTime endDate) {
            _results.Clear();
            _results.AddRange(await Crawler.GetRangeReportsImage((int)notionKind, startDate.ToTaiwanDateString(), endDate.ToTaiwanDateString()));
            return _results.FilterType<ImageResult>().ToList();
        }

        public async Task<IList<ImageResult>> GetReportImages(NotionKind notionKind, QueryRange range) {
            _results.Clear();
            _results.AddRange(await Crawler.GetReportsImage((int)notionKind, ((int)range).ToString()));
            return _results.FilterType<ImageResult>().ToList();
        }

        public void Dispose()
        {
            _guarantor.MessageReceived -= OnMessageReceived;
            TimeService.TimeIsUp -= OnTimeIsUp;
            var crawler = Crawler as IDisposable;
            crawler?.Dispose();
            var guarantor = _guarantor as IDisposable;
            guarantor?.Dispose();
            var timer = TimeService as IDisposable;
            timer?.Dispose();
            foreach (var notification in _notifications.Values)
            {
                if (notification is IDisposable disposable)
                {
                    disposable.Dispose();
                } 
            }

            _notifications.Clear();
        }

        public async ValueTask DisposeAsync()
        {
            _guarantor.MessageReceived -= OnMessageReceived;
            TimeService.TimeIsUp -= OnTimeIsUp;
            if (Crawler is IAsyncDisposable crawler)
            {
                await crawler.DisposeAsync();
            }

            var guarantor = _guarantor as IDisposable;
            guarantor?.Dispose();
            var timer = TimeService as IDisposable;
            timer?.Dispose();
            foreach (var notification in _notifications.Values)
            {
                if (notification is IAsyncDisposable disposable)
                {
                    await disposable.DisposeAsync();
                } 
            }

            _notifications.Clear();
        }
    }
}
