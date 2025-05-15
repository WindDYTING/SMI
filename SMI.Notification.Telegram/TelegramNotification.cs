using SMI.Core;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace SMI.Notification.Telegram {
    public class TelegramNotification : INewsNotification<ITelegramBotClient, TelegramNotifyOptions>, IDisposable, IAsyncDisposable {
        public ITelegramBotClient Client { get; }
        public TelegramNotifyOptions Options { get; }

        public TelegramNotification(ITelegramBotClient client, TelegramNotifyOptions options)
        {
            Options = options ?? throw new ArgumentNullException(nameof(options));
            Client = client;
        }

        public TelegramNotification(TelegramNotifyOptions options)
        {
            Options = options ?? throw new ArgumentNullException(nameof(options));;
            Client = new TelegramBotClient(new TelegramBotClientOptions(Options.BotToken));
        }

        public virtual Task SendPhotoAsync(ImageResult image)
        {
            return Client.SendPhoto(Options.ChatId, InputFile.FromStream(image.PhotoStream));
        }


        public virtual Task SendText(string text)
        {
            return Client.SendMessage(Options.ChatId, text);
        }

        public void Dispose()
        {
            Client.Close().Wait();
        }

        public async ValueTask DisposeAsync()
        {
            await Client.Close();
        }
    }
}
