using System;
using System.Collections.Generic;

namespace SMI {
    public class MessageReceivedEventArgs<T> : EventArgs {
        public MessageReceivedEventArgs(T result)
        {
            Result = result;
        }

        public T Result { get; }
    }

    public class MessageReceivedEventArgs : EventArgs {
        public MessageReceivedEventArgs(object result)
        {
            Result = result;
        }

        public object Result { get; }
    }
}
