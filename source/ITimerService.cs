using System;

namespace SMI;

public interface ITimerService
{
    event EventHandler TimeIsUp;
    void SetTimeIsUp(TimeOnly time);
}