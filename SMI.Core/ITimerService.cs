using System;

namespace SMI.Core;

public interface ITimerService
{
    event EventHandler TimeIsUp;
    void SetTimeIsUp(TimeOnly time);
}