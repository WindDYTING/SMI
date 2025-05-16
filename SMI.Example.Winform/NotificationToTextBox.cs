using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using SMI.Core;

namespace SMI.Example.Winform {
    public class NotificationToTextBox : INewsNotification {
        private readonly TextBox _control;
        private readonly SynchronizationContext _context;

        public NotificationToTextBox(TextBox control, SynchronizationContext context)
        {
            _control = control;
            _context = context;
        }

        public Task SendPhotoAsync(ImageResult image)
        {
            // Not Support Image.
            return Task.CompletedTask;
        }

        public Task SendText(string text)
        {
            _context.Post(_ => {
                _control.AppendText(text + Environment.NewLine);
                _control.AppendText("Telegram notified." + Environment.NewLine);
            }, null);
            return Task.CompletedTask;
        }
    }
}
