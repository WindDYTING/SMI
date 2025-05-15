using System;

namespace SMI.Core {
    public class NotifyImageEventArgs : EventArgs {
        public ImageResult Data { get; set; }

        public INewsNotification Notification { get; }

        public NotifyImageEventArgs(ImageResult data, INewsNotification notification)
        {
            Data = data;
            Notification = notification;
        }
    }
}
