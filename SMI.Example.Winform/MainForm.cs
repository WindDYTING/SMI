using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Extensions.Configuration;
using Serilog;
using Serilog.Events;
using SMI.Core;
using SMI.Logging.Serilog;
using SMI.Notification.Telegram;
using Log = Serilog.Log;

namespace SMI.Example.Winform
{
    public partial class MainForm : Form {
        private readonly IConfiguration _configuration;
        private readonly SynchronizationContext _synchronizationContext;
        private readonly Dictionary<string, TelegramNotifyOptions> _telegramNotifyOptions = new Dictionary<string, TelegramNotifyOptions>();
        private readonly SmiBot _bot;

        public MainForm(IConfiguration configuration) {
            InitializeComponent();
            _synchronizationContext = SynchronizationContext.Current;
            _configuration = configuration;
            queryForm.CustomFormat = "yyyyMMdd";
            queryTo.CustomFormat = "yyyyMMdd";
            checkQueryRecent.Tag = checkSpecifyDate;
            checkSpecifyDate.Tag = checkQueryRecent;
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.File(".log", LogEventLevel.Information, rollingInterval: RollingInterval.Day)
                .CreateLogger();

            _bot = new SmiBot();
            _bot.BeforeNotifyKeyValueCollections += OnBeforeNotifyKeyValueCollections;
            _bot.BeforeTimeIsUp += OnBeforeTimeIsUp;
            _bot.AfterTimeIsUp += OnAfterTimeIsUp;
            _bot.Logger = new SmiLoggerSerilogAdapter(Log.Logger);
            _bot.AddNotification("textbox", new NotificationToTextBox(txtLog, _synchronizationContext));
        }

        private void OnAfterTimeIsUp(object sender, EventArgs e) {
            _synchronizationContext.Post(_ => lblStatus.Text = "Finished!", null);
        }

        private void OnBeforeTimeIsUp(object sender, EventArgs e) {
            _synchronizationContext.Post(_ => lblStatus.Text = "Getting Data...", null);
        }

        private async void OnBeforeNotifyKeyValueCollections(object sender, BeforeNotifyKeyValueCollectionsEventArgs e) {
            var text = e.Data.Process();
            await e.Notification.SendText(text);
        }

        private async void MainForm_Load(object sender, EventArgs e) {
            await _bot.InitAsync();
        }

        private void MainForm_Shown(object sender, EventArgs e) {
            try {
                selectionKind.SelectedIndex = 0;
                selectionQueryRange.SelectedIndex = 0;
                foreach(var section in _configuration
                             .GetSection("telegramGroups")
                             .GetChildren()) {
                    var option = new TelegramNotifyOptions();
                    section.Bind(option);
                    _telegramNotifyOptions.Add(option.Name, option);
                    checkTelegramSendToWho.Items.Add(option.Name);
                }
            } catch(Exception ex) {
                Log.Error(ex, ex.Message);
            }
        }


        private async void btnCrawler_Click(object sender, EventArgs e) {
            try {
                lblStatus.Text = "Getting Data...";

                var dicts = await CrawlerGetReports(selectionKind.SelectedIndex);
                _bot.RaiseNotify(dicts);
            } catch(Exception ex) {
                Log.Error(ex, ex.Message);
            } finally {
                lblStatus.Text = "Finished!";
            }
        }

        private async Task<IList<KeyValueCollections>> CrawlerGetReports(int notionKind) {
            if(checkSpecifyDate.Checked) {
                return await _bot.GetReportsRange((NotionKind)(notionKind + 1), queryForm.Value, queryTo.Value);
            }
            return await _bot.GetReports((NotionKind)(notionKind + 1),
                (QueryRange)(selectionQueryRange.SelectedIndex + 1));

        }


        private async void MainForm_FormClosing(object sender, FormClosingEventArgs e) {
            try {
                _bot.BeforeNotifyKeyValueCollections -= OnBeforeNotifyKeyValueCollections;
                _bot.BeforeTimeIsUp -= OnBeforeTimeIsUp;
                _bot.AfterTimeIsUp -= OnAfterTimeIsUp;
                await _bot.DisposeAsync();
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

        private void selectionEventList_ItemCheck(object sender, ItemCheckEventArgs e) {
            if(e.NewValue == CheckState.Checked) {
                _bot.Subscribe((NotionKind)(e.Index + 1));
            } else {
                _bot.Unsubscribe((NotionKind)(e.Index + 1));
            }
        }

        private void checkTelegramSendToWho_ItemCheck(object sender, ItemCheckEventArgs e) {
            var name = checkTelegramSendToWho.Items[e.Index] as string;
            if (e.NewValue == CheckState.Checked)
            {
                _bot.AddNotification(name, new AutoSplitTextTelegramNotification(_telegramNotifyOptions[name]));
            }
            else
            {
                _bot.RemoveNotification(name);
            }
        }
    }
}
