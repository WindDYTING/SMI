using System;
using System.Threading.Tasks;

namespace SMI.Core;

public interface IGuarantor
{
    event EventHandler<MessageReceivedEventArgs> MessageReceived;
    void ClearCache();
    Task WaitGuarantorAsync();
    TaskCompletionSource EnqueueRecord(Record record);
}