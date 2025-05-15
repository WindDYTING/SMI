using System;

namespace SMI.Core {
    public class NotifyKeyValueCollectionEventArgs : EventArgs {
        public INewsNotification Notification { get; }

        public KeyValueCollections Data { get; }

        public NotifyKeyValueCollectionEventArgs(INewsNotification notification, KeyValueCollections data)
        {
            Notification = notification;
            Data = data;
        }
    }
}
