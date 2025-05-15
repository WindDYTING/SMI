using Telegram.Bot;

namespace SMI.Notification.Telegram {
    public class AutoSplitTextTelegramNotification : TelegramNotification
    {
        private const uint DefaultSplitTextLength = 4000;
        public uint SplitTextLength { get; set; } = DefaultSplitTextLength;

        public AutoSplitTextTelegramNotification(ITelegramBotClient client, TelegramNotifyOptions options) : base(client, options)
        {
        }

        public AutoSplitTextTelegramNotification(TelegramNotifyOptions options) : base(options)
        {
            
        }

        public AutoSplitTextTelegramNotification(TelegramNotifyOptions options, uint splitTextLength) : base(options)
        {
            SplitTextLength = splitTextLength;
        }

        public override async Task SendText(string text)
        {
            double splitTextLength = SplitTextLength;
            var chunkCount = (int)Math.Ceiling(text.Length / splitTextLength);
            for (int i = 0, startIdx = (int)(i * splitTextLength); i < chunkCount; i++)
            {
                var length = i == chunkCount - 1 ? (int)(text.Length % splitTextLength) : (int)splitTextLength;
                var splitMsg = text.Substring(startIdx, length);
                startIdx += length;
                var sendText = $"# {i+1}/{chunkCount} {Environment.NewLine} {splitMsg}";
                await base.SendText(sendText);
            }
        }
    }
}
