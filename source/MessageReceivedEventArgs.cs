using System;
using System.Collections.Generic;

namespace SMI {
    public class MessageReceivedEventArgs : EventArgs {
        public MessageReceivedEventArgs(IList<Dictionary<string, string>> result)
        {
            Result = result;
        }

        public IList<Dictionary<string, string>> Result { get; }
    }
}
