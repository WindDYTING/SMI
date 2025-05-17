using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SMI.Core;

public abstract class SmiBotBase<TSubscribe> : IDisposable, IAsyncDisposable
{
    protected IGuarantor Guarantor;
    private readonly IDictionary<string, INewsNotification> _notifications = new Dictionary<string, INewsNotification>();
    protected readonly IList<TSubscribe> Subscribes = new List<TSubscribe>();
    public ILogger Logger { get; set; } = Log.Default;
    public ICrawler CrawlerCore { get; protected set; }
    public ITimerService TimeService { get; protected set; }
    protected List<object> Results { get; } = new();
    public event EventHandler BeforeTimeIsUp;
    public event EventHandler AfterTimeIsUp;
    public event EventHandler<BeforeNotifyKeyValueCollectionsEventArgs> BeforeNotifyKeyValueCollections;
    public event EventHandler<NotifyKeyValueCollectionEventArgs> NotifyEveryKeyValueCollection;
    public event EventHandler<NotifyImageEventArgs> NotifyEveryImage;


    protected SmiBotBase()
    {
        CrawlerCore = new Crawler(Logger);
        CrawlerCore.Options.SetUrl(Router.ShareholdingMeetReportsUri);
        Guarantor = new Guarantor(CrawlerCore);
        Guarantor.MessageReceived += OnMessageReceived;
        TimeService = new TimerService(new TimeOnly(21, 30));
        TimeService.TimeIsUp += OnTimeIsUp;
    }

    protected SmiBotBase(ICrawler crawler)
    {
        CrawlerCore = crawler;
        CrawlerCore.Options.SetUrl(Router.ShareholdingMeetReportsUri);
        Guarantor = new Guarantor(crawler);
        Guarantor.MessageReceived += OnMessageReceived;
        TimeService = new TimerService(new TimeOnly(21, 30));
        TimeService.TimeIsUp += OnTimeIsUp;
    }

    protected SmiBotBase(ICrawler crawler, Uri crawUri)
    {
        CrawlerCore = crawler;
        CrawlerCore.Options.SetUrl(crawUri);
        Guarantor = new Guarantor(crawler);
        Guarantor.MessageReceived += OnMessageReceived;
        TimeService = new TimerService(new TimeOnly(21, 30));
        TimeService.TimeIsUp += OnTimeIsUp;
    }

    public virtual void Subscribe(TSubscribe subscribe)
    {
        if (Subscribes.IndexOf(subscribe) == -1)
        {
            Subscribes.Add(subscribe);
        }
    }

    public virtual void Unsubscribe(TSubscribe subscribe)
    {
        var index = Subscribes.IndexOf(subscribe);
        if (index != -1)
        {
            Subscribes.RemoveAt(index);
        }
    }


    public Task InitAsync()
    {
        return CrawlerCore.InitAsync();
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
            catch (Exception ex)
            {
                Logger.Warn("Has INewNotification occur exception", ex);
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

    protected async void OnTimeIsUp(object sender, EventArgs e)
    {
        try
        {
            OnBeforeTimeIsUp(EventArgs.Empty);
            foreach (var subscribeInfo in Subscribes)
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

    protected abstract Task<IList<object>> CrawlerGetReports(TSubscribe info);

    protected void OnMessageReceived(object sender, MessageReceivedEventArgs e)
    {
        if (e.Result is IList<object> result)
        {
            Results.AddRange(result);
        }
    }

    public virtual void Dispose()
    {
        Guarantor.MessageReceived -= OnMessageReceived;
        TimeService.TimeIsUp -= OnTimeIsUp;
        var crawler = CrawlerCore as IDisposable;
        crawler?.Dispose();
        var guarantor = Guarantor as IDisposable;
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

    public virtual async ValueTask DisposeAsync()
    {
        Guarantor.MessageReceived -= OnMessageReceived;
        TimeService.TimeIsUp -= OnTimeIsUp;
        if (CrawlerCore is IAsyncDisposable crawler)
        {
            await crawler.DisposeAsync();
        }

        var guarantor = Guarantor as IDisposable;
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