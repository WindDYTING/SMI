using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using AutoTrader;
using Microsoft.Extensions.Configuration;
using Serilog;

namespace SMI
{
    public partial class MainForm : Form {
        private readonly IConfiguration _configuration;
        private readonly Crawler _crawler;
        private readonly Guarantor _guarantor;
        private readonly SynchronizationContext _synchronizationContext;
        private readonly TimerService _timerService;

        private readonly List<Dictionary<string, string>> _results = new();

        public MainForm(IConfiguration configuration) {
            InitializeComponent();
            _configuration = configuration;
            queryForm.CustomFormat = "yyyyMMdd";
            queryTo.CustomFormat = "yyyyMMdd";
            checkQueryRecent.Tag = checkSpecifyDate;
            checkSpecifyDate.Tag = checkQueryRecent;
            _crawler = new Crawler();
            _guarantor = new Guarantor(_crawler);
            _guarantor.MessageReceived += OnMessageReceived;
            _crawler.Guarantor = _guarantor;
            _synchronizationContext = SynchronizationContext.Current;
            _timerService = new TimerService(new TimeOnly(21, 30));
            _timerService.TimeIsUp += OnTimeIsUp;
        }

        private async void OnTimeIsUp(object sender, EventArgs e)
        {
            try
            {
                _synchronizationContext.Post(_ => lblStatus.Text = "Getting Data...", null);
                foreach (int checkedIndex in selectionEventList.CheckedIndices)
                {
                    await CrawlerGetReports(checkedIndex);

                    var msg = string.Join($"-------{Environment.NewLine}", _results.Select(d => d.ToJson().Trim('{', '}')));

                    await TelegramNotify.SendAsync(msg, checkTelegramSendToWho.CheckedIndices.Cast<int>().ToArray());
                    _synchronizationContext.Post(_ =>
                    {
                        txtLog.AppendText(msg + Environment.NewLine);
                        txtLog.AppendText("Telegram notified." + Environment.NewLine);
                    }, null);

                    _results.Clear();
                }
                _synchronizationContext.Post(_ => lblStatus.Text = "Finished!", null);
            }
            catch (Exception ex)
            {
                Log.Error(ex, ex.Message);
            }
        }

        private void OnMessageReceived(object sender, MessageReceivedEventArgs e) {
            _results.AddRange(e.Result);
        }

        private async void MainForm_Load(object sender, EventArgs e) {
            await _crawler.InitAsync();
        }

        private void MainForm_Shown(object sender, EventArgs e) 
        {
            try {
                selectionKind.SelectedIndex = 0;
                selectionQueryRange.SelectedIndex = 0;
                foreach (var section in _configuration
                             .GetSection("telegramGroups")
                             .GetChildren())
                {
                    var option = new TelegramNotifyOptions();
                    section.Bind(option);
                    TelegramNotify.Options.Add(option);
                    checkTelegramSendToWho.Items.Add(option.Name);
                }
            } catch(Exception ex) {
                Log.Error(ex, ex.Message);
            }
        }


        private async void btnCrawler_Click(object sender, EventArgs e)
        {
            try
            {
                lblStatus.Text = "Getting Data...";

                await CrawlerGetReports(selectionKind.SelectedIndex);

                var msg = string.Join($"------- {Environment.NewLine}", _results.Select(d => d.ToJson().Trim('{', '}')));
                await TelegramNotify.SendAsync(msg, checkTelegramSendToWho.CheckedIndices.Cast<int>().ToArray());
                txtLog.AppendText(msg + Environment.NewLine);
                txtLog.AppendText("Telegram notified." + Environment.NewLine);
            }
            catch (Exception)
            {
                // ignored
            }
            finally
            {
                _results.Clear();
                lblStatus.Text = "Finished!";
            }
        }

        private async Task CrawlerGetReports(int notionKind)
        {
            IList<Dictionary<string, string>> data;
            if(checkSpecifyDate.Checked) {
                data = await _crawler.GetReportsRange(notionKind,
                    queryForm.Value.ToTaiwanDateString(),
                    queryTo.Value.ToTaiwanDateString());
            } else {
                data = await _crawler.GetReports(notionKind,
                    (selectionQueryRange.SelectedIndex + 1).ToString());
            }
            _results.AddRange(data);
        }


        private async void MainForm_FormClosing(object sender, FormClosingEventArgs e) {
            try {
                await _crawler.DisposeAsync();
            } catch(Exception ex) {
                Log.Error(ex, ex.Message);
            }
        }

        private void checkbox_CheckedChanged(object sender, EventArgs e) {
            var ckb = (CheckBox)sender;
            var ckbTag = Checks.ThrowsIsNull(ckb.Tag as CheckBox);
            ckbTag.Checked = !ckb.Checked;
        }

        private void btnLogClear_Click(object sender, EventArgs e) {
            txtLog.Clear();
        }
    }
}
