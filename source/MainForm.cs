using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using PuppeteerSharp;
using Serilog;
using Serilog.Events;

namespace SMI
{
    public partial class MainForm : Form
    {
        private readonly IBrowserFetcher _fetcher;
        private IBrowser _browser;

        public MainForm()
        {
            InitializeComponent();
            _fetcher = new BrowserFetcher();
            Log.Logger = new LoggerConfiguration()
                .WriteTo.File(".log", LogEventLevel.Error, rollingInterval: RollingInterval.Day)
                .CreateLogger();
        }

        private async void MainForm_Load(object sender, System.EventArgs e)
        {
            try
            {
                await _fetcher.DownloadAsync();
                _browser = await Puppeteer.LaunchAsync(new LaunchOptions { Headless = true });
            }
            catch (Exception ex)
            {
                Log.Error(ex, ex.Message);
            }
        }

        private void MainForm_Shown(object sender, EventArgs e)
        {
            try
            {
                selectionKind.SelectedIndex = 0;
                selectionQueryRange.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                Log.Error(ex, ex.Message);
            }
        }

        private async void btnCrawler_Click(object sender, EventArgs e)
        {
            try
            {
                lblStatus.Text = "Loading...";
                var data = await GetReports(selectionKind.SelectedIndex);
                var json = data.ToJson();
                textBox1.Text = json;
            }
            catch (WaitTaskTimeoutException)
            {
                MessageBox.Show("查無資料");
                textBox1.Text = string.Empty;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "query fault");
                Log.Error(ex, ex.Message);
                textBox1.Text = string.Empty;
            }
            finally
            {
                lblStatus.Text = "Finished";
            }
        }

        public async Task<IList<Dictionary<string, string>>> GetReports(int selectionValue)
        {
            await using var page = await _browser.NewPageAsync();
            await page.GoToAsync(Router.ShareholdingMeetReportsUrl);
            await page.ClickAsync("#div4 > input[type=radio]:nth-child(5)");
            await page.SelectAsync("#date", (selectionQueryRange.SelectedIndex + 1).ToString());
            await page.SelectAsync("#noticeKind", (selectionValue + 1).ToString());
            await page.ClickAsync("#search_bar1 > div > input[type=button]");
            var tbody = await page.WaitForSelectorAsync("#div01 > form > table > tbody", new WaitForSelectorOptions
            {
                Timeout = (int)TimeSpan.FromSeconds((double)timeout.Value).TotalMilliseconds
            });
            var elements = (await tbody.QuerySelectorAllAsync("#div01 > form > table > tbody > tr")).Skip(1);
            foreach (var element in elements)
            {
                var elementOfButton = await element.QuerySelectorAsync(
                    "#div01 > form > table > tbody > tr > td:nth-child(5) > input[type=button]");
                await elementOfButton.ClickAsync();
            }

            var list = new List<Dictionary<string, string>>();
            var pages = await _browser.PagesAsync();
            pages = pages.Where(p => p != page).ToArray();
            foreach (var p in pages)
            {
                if (!p.Url.Equals(Router.ShareholdingMeetReportsUrl) || p == page)
                {
                    continue;
                }

                await p.WaitForSelectorAsync("#div01 > center > table > tbody > tr:nth-child(1) > td");
                await Task.Delay(400);
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

            await Task.WhenAll(pages.Select(p => p.CloseAsync()));

            return list;
        }

        private async void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                if (_browser != null)
                    await _browser.CloseAsync();

                //foreach (var process in Process.GetProcesses().Where(p => p.ProcessName == "chrome"))
                //{
                //    process.Kill();
                //    process.Close();
                //}
            }
            catch (Exception ex)
            {
                Log.Error(ex, ex.Message);
            }
        }
    }
}
