using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Serilog;
using SMI;
using Telegram.Bot;

namespace AutoTrader
{

    public static class TelegramNotify
    {
        private const double MaximumLength = 4000.0;
        public static IList<TelegramNotifyOptions> Options { get; } = new List<TelegramNotifyOptions>();

        public static async Task SendAsync(string msg, params int[] sendToWho)
        {
            foreach (var index in sendToWho)
            {
                await SendAsync(Options[index], msg);
            }
        }

        public static async Task SendAsync(TelegramNotifyOptions option, string msg)
        {
            if (string.IsNullOrWhiteSpace(msg)) return;

            var (chatId, botToken) = (option.ChatId, option.BotToken);

            // string url = $"https://api.telegram.org/bot{botToken}/sendMessage?chat_id={chatId}&text=```{msg}```&parse_mode=markdown";
            var client = new TelegramBotClient(new TelegramBotClientOptions(botToken));
            try
            {
                var chunkCount = (int)Math.Ceiling(msg.Length / MaximumLength);
                for (int i = 0, startIdx = (int)(i * MaximumLength); i < chunkCount; i++)
                {
                    var length = i == chunkCount - 1 ? (int)(msg.Length % MaximumLength) : (int)MaximumLength;
                    var splitMsg = msg.Substring(startIdx, length);
                    startIdx += length;
                    var sendText = $"# {i+1}/{chunkCount} {Environment.NewLine} {splitMsg}";
                    await client.SendMessage(chatId, sendText);
                }
                //  await httpClient.GetAsync(url);
            }
            catch (Exception ex)
            {
                Log.Error(ex, $"Failed to send message: {ex.Message}");
            }
            finally
            {
                await client.Close();
            }
        }
    }
}