using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Serilog;
using SMI;
using SMI.Models;
using SMI.Options;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace AutoTrader
{

    public static class TelegramNotify
    {
        private const double MaximumLength = 4000.0;
        public static IList<TelegramNotifyOptions> Options { get; } = new List<TelegramNotifyOptions>();

        public static async Task SendAsync(IList<object> results, params int[] sendToWho)
        {
            foreach (object result in results)
            {
                if (result is ImageResult image)
                {
                    foreach (var index in sendToWho)
                    {
                        await SendPhotoAsync(Options[index], image);
                    }
                }
            }

            var msg = string.Join($"-------{Environment.NewLine}", results.Where(x => x is KeyValueCollections)
                .Cast<KeyValueCollections>()
                .Select(d => d.ToJson().Trim('{', '}')));
            await SendAsync(msg, sendToWho);
        }

        public static async Task SendPhotoAsync(TelegramNotifyOptions option, ImageResult image)
        {

            var client = new TelegramBotClient(new TelegramBotClientOptions(option.BotToken));
            try
            {

                await client.SendPhoto(option.ChatId, InputFile.FromStream(image.PhotoStream));
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