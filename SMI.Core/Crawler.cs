using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PuppeteerSharp;
using SMI.Core.Options;

namespace SMI.Core {
    public class Crawler : ICrawler, IDisposable, IAsyncDisposable
    {
        private readonly IBrowserFetcher _fetcher;
        private IBrowser _browser;
        public IGuarantor Guarantor { get; set; }
        public CrawlerOptions Options { get; }
        public ILogger Logger { get; }
        public IStrategyFactory? CustomStrategyFactory { get; set; }

        public Crawler(CrawlerOptions options, ILogger logger)
        {
            _fetcher = new BrowserFetcher();
            Options = options;
            Logger = logger;
        }

        public Crawler(ILogger logger)
        {
            _fetcher = new BrowserFetcher(SupportedBrowser.Chrome);
            Options = new CrawlerOptions();
            Logger = logger;
        }

        // For Unit Test
        internal Crawler(IBrowser browser, IBrowserFetcher fetcher, IGuarantor guarantor, CrawlerOptions options)
        {
            _browser = browser;
            _fetcher = fetcher;
            Guarantor = guarantor;
            Options = options;
        }

        public async Task InitAsync()
        {
            try
            {
                await _fetcher.DownloadAsync();
                _browser = await Puppeteer.LaunchAsync(
                    new LaunchOptions
                    {
                        Headless = true,
                    }
                );
            } catch(Exception ex)
            {
                Logger.Err(ex.Message, ex);
                throw;
            }
        }

        public virtual async Task<IList<TResult>> GetReportsRange<TResult>(int selectionValue, string from, string to, int fromPage=0) {
            var results = new List<TResult>();
            foreach(var marketType in Constants.MarketTypes) 
            {
                var result = await GetSpecifiedMarketTypeReportsRange<TResult>(selectionValue, from, to, fromPage, marketType);
                results.AddRange(result);
                await Guarantor.WaitGuarantorAsync();
                Guarantor.ClearCache();
            }

            return results;
        }

        public virtual async Task<IList<TResult>> GetSpecifiedMarketTypeReportsRange<TResult>(int selectionValue, string from, string to, int fromPage,
            string marketType)
        {
            await using var page = await _browser.NewPageAsync();
            var record = new Record(selectionValue, marketType, from, to, typeof(TResult));
            try {
                await ActionToGetReportsRange(from, to, page);
                var elements1 = await GetAllReportHandles(selectionValue, marketType, page);
                IElementHandle[] elements = elements1;
                await ClickDetailButtons(elements, fromPage);
                var result = await GetInnerReports<TResult>(selectionValue, page, record, fromPage);
                return result;
            } catch(OperationCanceledException)
            {
                throw;
            } catch(WaitTaskTimeoutException wtex)
            {
                Logger.Warn("Timeout", wtex);
            } catch(Exception ex)
            {
                Logger.Err(ex.Message, ex);
            } finally {
                await Task.Delay(Constants.SwitchPageDelay);
            }

            return Array.Empty<TResult>();
        }

        public async Task<IList<object>> GetSpecifiedMarketTypeReportsRange(int selectionValue, string from, string to, int fromPage,
            string marketType, Type resultType)
        {
            await using var page = await _browser.NewPageAsync();
            var record = new Record(selectionValue, marketType, from, to, resultType);
            try {
                await ActionToGetReportsRange(from, to, page);
                var elements1 = await GetAllReportHandles(selectionValue, marketType, page);
                IElementHandle[] elements = elements1;
                await ClickDetailButtons(elements, fromPage);
                var result = await GetInnerReports<object>(selectionValue, page, record, fromPage);
                return result;
            } catch(OperationCanceledException)
            {
                throw;
            } catch(WaitTaskTimeoutException wtex)
            {
                Logger.Warn("Timeout", wtex);
            } catch(Exception ex)
            {
                Logger.Err(ex.Message, ex);
            } finally {
                await Task.Delay(Constants.SwitchPageDelay);
            }

            return Array.Empty<object>();
        }

        public virtual async Task<IList<TResult>> GetReports<TResult>(int selectionValue, string queryRange, int fromPage=0) {
            var results = new List<TResult>();

            foreach(var marketType in Constants.MarketTypes) {
                var result = await GetSpecifiedMarketTypeReports<TResult>(selectionValue, queryRange, fromPage, marketType);
                results.AddRange(result);
                await Guarantor.WaitGuarantorAsync();
                Guarantor.ClearCache();
            }

            return results;
        }

        public async Task<IList<object>> GetSpecifiedMarketTypeReports(int selectionValue, string queryRange, int fromPage, string marketType, Type resultType)
        {
            var record = new Record(selectionValue, marketType, queryRange, resultType);
            try {
                await using var page = await _browser.NewPageAsync();
                await ActionToGetReports(queryRange, page);
                var elements1 = await GetAllReportHandles(selectionValue, marketType, page);
                IElementHandle[] elements = elements1;
                await ClickDetailButtons(elements, fromPage);
                var ret = await GetInnerReports<object>(selectionValue, page, record, fromPage);
                return ret;
            } catch(OperationCanceledException) {
                throw;
            } catch(WaitTaskTimeoutException wtex) {
                Logger.Warn("Timeout", wtex);
            } catch(Exception ex) {
                Logger.Err(ex.Message, ex);
            } finally {
                await Task.Delay(Constants.SwitchPageDelay);
            }
            return Array.Empty<object>();
        }

        public virtual async Task<IList<TResult>> GetSpecifiedMarketTypeReports<TResult>(int selectionValue, string queryRange, int fromPage, string marketType)
        {
            var record = new Record(selectionValue, marketType, queryRange, typeof(TResult));
            try {
                await using var page = await _browser.NewPageAsync();
                await ActionToGetReports(queryRange, page);
                var elements1 = await GetAllReportHandles(selectionValue, marketType, page);
                IElementHandle[] elements = elements1;
                await ClickDetailButtons(elements, fromPage);
                var ret = await GetInnerReports<TResult>(selectionValue, page, record, fromPage);
                return ret;
            } catch(OperationCanceledException) {
                throw;
            } catch(WaitTaskTimeoutException wtex) {
                Logger.Warn("Timeout", wtex);
            } catch(Exception ex) {
                Logger.Err(ex.Message, ex);
            } finally {
                await Task.Delay(Constants.SwitchPageDelay);
            }
            return Array.Empty<TResult>();
        }

        public virtual async Task ActionToGetReports(string queryRange, IPage page)
        {
            await page.GoToAsync(Options.GoToUrl);
            await page.ClickAsync("#div4 > input[type=radio]:nth-child(5)");
            await page.SelectAsync("#date", queryRange);
        }

        public virtual async Task ActionToGetReportsRange(string from, string to, IPage page)
        {
            await page.GoToAsync(Options.GoToUrl);
            await page.ClickAsync("#div4 > input[type=radio]:nth-child(5)");
            await page.ClickAsync("#noticeDate");
            await page.TypeAsync("#yymmdd1", from);
            await page.TypeAsync("#yymmdd2", to);
        }

        private async Task ClickDetailButtons(IElementHandle[] elements, int fromPage)
        {
            foreach(var e in elements.Skip(fromPage)) {
                var elementOfButton = await e.QuerySelectorAsync(
                    "#div01 > form > table > tbody > tr > td:nth-child(5) > input[type=button]");
                if(elementOfButton != null)
                    await elementOfButton.ClickAsync();
            }
        }

        private async Task<IElementHandle[]> GetAllReportHandles(int selectionValue, string selectionMarketType, IPage page)
        {
            await page.SelectAsync("#typek", selectionMarketType);
            await page.SelectAsync("#noticeKind", (selectionValue + 1).ToString());
            await page.ClickAsync("#search_bar1 > div > input[type=button]");
            var tbody = await page.WaitForSelectorAsync("#div01 > form > table > tbody", new WaitForSelectorOptions {
                Timeout = 3000
            });
            var elements = (await tbody.QuerySelectorAllAsync("#div01 > form > table > tbody > tr"))
                .Skip(1)
                .Where(e => e is not null)
                .ToArray();
            return elements;
        }
        //private async Task<IList<object>> GetInnerReports(int selectionValue, IPage mainPage,
        //    Record record, int fromPage) {
        //    var list = new List<object>();
        //    var pages = await _browser.PagesAsync();
        //    pages = pages.Where(p => p != mainPage)
        //        .Where(p => p.Url.Equals(Router.ShareholdingMeetReportsUri.ToString()))
        //        .ToArray();
        //    var index = 0;
        //    try
        //    {
        //        var strategy = Checks.ThrowsIsNull(StrategiesFactory.GetStrategy(record.ResultType));

        //        foreach (var p in pages)
        //        {
        //            var succeedLoad = await p.WaitPageLoadAsync();
        //            if (!succeedLoad)
        //            {
        //                index++;
        //                continue;
        //            }

        //            await strategy.GetNewsAsync(p, selectionValue);
        //            if (strategy.IsValid()) list.Add(strategy.Results);
        //        }

        //        return list;
        //    }
        //    finally
        //    {
        //        ClosePages(pages);
        //        if (index > 0) 
        //        {
        //            var newFromPage = pages.Length - index + fromPage;
        //            record.FromPage = newFromPage;
        //            Guarantor.EnqueueRecord(record);
        //        }
        //    }
        //}

        private async Task<IList<TResult>> GetInnerReports<TResult>(int selectionValue, IPage mainPage,
            Record record, int fromPage) {
            var list = new List<TResult>();
            var pages = await _browser.PagesAsync();
            pages = pages.Where(p => p != mainPage)
                .Where(p => p.Url.Equals(Router.ShareholdingMeetReportsUri.ToString()))
                .ToArray();
            var index = 0;
            try
            {
                var strategy = DetermineStrategyFromType<TResult>(record);

                foreach (var p in pages)
                {
                    var succeedLoad = await p.WaitPageLoadAsync();
                    if (!succeedLoad)
                    {
                        index++;
                        continue;
                    }

                    var ret = await strategy.GetNewsAsync(p, selectionValue);
                    if (strategy.IsValid()) list.Add(ret);
                }

                return list;
            }
            finally
            {
                ClosePages(pages);
                if (index > 0) 
                {
                    var newFromPage = pages.Length - index + fromPage;
                    record.FromPage = newFromPage;
                    Guarantor.EnqueueRecord(record);
                }
            }
        }

        private ICrawlerStrategy<TResult> DetermineStrategyFromType<TResult>(Record record)
        {
            ICrawlerStrategy<TResult> strategy;
            if (typeof(TResult) == typeof(object) && record.ResultType != typeof(object))
            {
                strategy = StrategiesFactory.GetDefaultStrategy(record.ResultType) as ICrawlerStrategy<TResult>;
            }
            else if (typeof(TResult) != typeof(object))
            {
                strategy = StrategiesFactory.GetDefaultStrategy(typeof(TResult)) as ICrawlerStrategy<TResult>;
            }
            else
            {
                throw new ArgumentException("object type is not support.");
            }

            if (strategy is null)
            {
                strategy = CustomStrategyFactory?.GetStrategy(typeof(TResult)) as ICrawlerStrategy<TResult>;
            }

            return Checks.ThrowsIsNull(strategy);
        }

        public void ClosePages(IPage[] pages)
        {
            Task.WaitAll(pages.Select(p => p.CloseAsync()).ToArray());
        }

        public void Dispose()
        {
            _browser?.CloseAsync();
            _browser?.Dispose();
        }

        public async ValueTask DisposeAsync()
        {
            if (_browser != null)
            {
                await _browser.CloseAsync();
                await _browser.DisposeAsync();
            }
        }
    }
}
