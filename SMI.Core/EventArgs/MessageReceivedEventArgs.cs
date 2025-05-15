namespace SMI.Core {
    public class MessageReceivedEventArgs : System.EventArgs {
        public MessageReceivedEventArgs(object result)
        {
            Result = result;
        }

        public object Result { get; }
    }
}
