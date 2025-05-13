using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PuppeteerSharp;
using Serilog;
using Serilog.Events;
using SMI.Options;

namespace SMI {
    public class Crawler : ICrawler, IDisposable, IAsyncDisposable
    {
        private readonly IBrowserFetcher _fetcher;
        private IBrowser _browser;
        public IGuarantor Guarantor { get; set; }
        public CrawlerOptions Options { get; } = new();

        public Crawler()
        {
            _fetcher = new BrowserFetcher();
            Log.Logger = new LoggerConfiguration()
                .WriteTo.File(".log", LogEventLevel.Error, rollingInterval: RollingInterval.Day)
                .CreateLogger();
        }

        // For Unit Test
        internal Crawler(IBrowser browser, IBrowserFetcher fetcher, IGuarantor guarantor)
        {
            _browser = browser;
            _fetcher = fetcher;
            Guarantor = guarantor;
        }

        public async Task InitAsync()
        {
            try {
                await _fetcher.DownloadAsync();
                _browser = await Puppeteer.LaunchAsync(
                    new LaunchOptions { Headless = true }
                );
            } catch(Exception ex) {
                Log.Error(ex, ex.Message);
            }
        }


        public async Task<IList<Dictionary<string, string>>> GetReportsRange(int selectionValue, string from, string to, int fromPage=0) {
            var results = new List<Dictionary<string, string>>();
            foreach(var marketType in Constants.MarketTypes) 
            {
                var result = await GetSpecifiedMarketTypeReportsRange(selectionValue, from, to, fromPage, marketType);
                results.AddRange(result);
                await Guarantor.WaitGuarantorAsync();
                Guarantor.ClearCache();

            }

            return results;
        }

        public async Task<IList<Dictionary<string, string>>> GetSpecifiedMarketTypeReportsRange(int selectionValue, string from, string to, int fromPage,
            string marketType)
        {
            await using var page = await _browser.NewPageAsync();
            var record = new Record(selectionValue, marketType, from, to);
            try {
                await ActionToGetReportsRange(from, to, page);
                var elements1 = await GetAllReportHandles(selectionValue, marketType, page);
                IElementHandle[] elements = elements1;
                await ClickDetailButtons(elements, fromPage);
                var result = await GetInnerReports(selectionValue, page, record, fromPage);
                return result;
            } catch(OperationCanceledException)
            {
                throw;
            } catch(WaitTaskTimeoutException)
            {
                Log.Warning(string.Empty, Constants.NoData);
            } catch(Exception ex)
            {
                Log.Error(ex, ex.Message);
            } finally {
                await Task.Delay(500);
            }

            return Array.Empty<Dictionary<string, string>>();
        }

        public async Task<IList<Dictionary<string, string>>> GetReports(int selectionValue, string queryRange, int fromPage=0) {
            var results = new List<Dictionary<string, string>>();

            foreach(var marketType in Constants.MarketTypes) {
                var result = await GetSpecifiedMarketTypeReports(selectionValue, queryRange, fromPage, marketType);
                results.AddRange(result);
                await Guarantor.WaitGuarantorAsync();
                Guarantor.ClearCache();
            }

            return results;
        }

        public async Task<IList<Dictionary<string, string>>> GetSpecifiedMarketTypeReports(int selectionValue, string queryRange, int fromPage, string marketType)
        {
            var record = new Record(selectionValue, marketType, queryRange);
            try {
                await using var page = await _browser.NewPageAsync();
                await ActionToGetReports(queryRange, page);
                var elements1 = await GetAllReportHandles(selectionValue, marketType, page);
                IElementHandle[] elements = elements1;
                await ClickDetailButtons(elements, fromPage);
                var ret = await GetInnerReports(selectionValue, page, record, fromPage);
                return ret;
            } catch(OperationCanceledException) {
                throw;
            } catch(WaitTaskTimeoutException) {
                Log.Warning(string.Empty, Constants.NoData);
            } catch(Exception ex) {
                Log.Error(ex, ex.Message);
            } finally {
                await Task.Delay(500);
            }
            return Array.Empty<Dictionary<string, string>>();
        }

        public async Task ActionToGetReports(string queryRange, IPage page)
        {
            await page.GoToAsync(Options.GoToUrl);
            await page.ClickAsync("#div4 > input[type=radio]:nth-child(5)");
            await page.SelectAsync("#date", queryRange);
        }

        public async Task ActionToGetReportsRange(string from, string to, IPage page)
        {
            await page.GoToAsync(Options.GoToUrl);
            await page.ClickAsync("#div4 > input[type=radio]:nth-child(5)");
            await page.ClickAsync("#noticeDate");
            await page.TypeAsync("#yymmdd1", from);
            await page.TypeAsync("#yymmdd2", to);
        }

        private static async Task ClickDetailButtons(IElementHandle[] elements, int fromPage)
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


        private async Task<IList<Dictionary<string, string>>> GetInnerReports(int selectionValue, IPage mainPage,
            Record record, int fromPage) {
            var list = new List<Dictionary<string, string>>();
            var pages = await _browser.PagesAsync();
            pages = pages.Where(p => p != mainPage)
                .Where(p => p.Url.Equals(Router.ShareholdingMeetReportsUrl))
                .ToArray();
            var index = 0;
            try
            {
                foreach (var p in pages)
                {
                    var succeedLoad = await p.WaitPageLoadAsync();
                    if (!succeedLoad)
                    {
                        index++;
                        continue;
                    }
                    
                    var dict = new Dictionary<string, string>();
                    var values = await p.QuerySelectorAllAsync(SelectorFactory.GetValueSelector(selectionValue));
                    var fields = await p.QuerySelectorAllAsync(SelectorFactory.GetFieldSelector(selectionValue));
                    foreach (var item in fields.Zip(values, (x, y) => (fieldElement: x, valueElement: y)))
                    {
                        var field = await p.EvaluateFunctionAsync<string>("e => e.textContent", item.fieldElement);
                        var value = await p.EvaluateFunctionAsync<string>("e => e.textContent", item.valueElement);
                        field = field.FilterString("\n");
                        value = value.FilterString("\n");
                        dict.TryAdd(field, value);
                    }

                    if (dict.Any()) list.Add(dict);
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
