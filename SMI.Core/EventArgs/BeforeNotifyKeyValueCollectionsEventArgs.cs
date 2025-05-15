using System;
using System.Collections.Generic;

namespace SMI.Core {
    public class BeforeNotifyKeyValueCollectionsEventArgs : EventArgs {
        public INewsNotification Notification { get; }

        public bool Handle { get; set; }

        public IList<KeyValueCollections> Data { get; }

        public BeforeNotifyKeyValueCollectionsEventArgs(INewsNotification notification, bool handle, IList<KeyValueCollections> data)
        {
            Notification = notification;
            Handle = handle;
            Data = data;
        }
    }
}
