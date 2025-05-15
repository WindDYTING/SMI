using System.ComponentModel.DataAnnotations;

namespace SMI.Notification.Telegram {
    public class TelegramNotifyOptions 
    {
        public string Name { get; set; }

        public string ChatId { get; set; }

        public string BotToken { get; set; }
    }
}
