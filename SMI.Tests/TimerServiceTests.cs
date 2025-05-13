using NSubstitute;

namespace SMI.Tests;

[TestFixture]
public class TimerServiceTests
{
    TimerService _timerService;

    [TearDown]
    public void TearDown()
    {
        _timerService.Dispose();
    }

    [Test]
    public void TimerService_SetTimeIsUpMoreDateTimeNow_CanTrigger()
    {
        var time = DateTime.Now.AddSeconds(1);
        var action = Substitute.For<IReceivedAction>();
        _timerService = new TimerService(time.ToTimeOnly());
        _timerService.SetCheckInterval(0.5);
        _timerService.TimeIsUp += (_, e) =>
        {
            action.Trigger();
        };

        Task.Delay(3000).Wait();

        action.Received(1).Trigger();
    }

    [Test]
    public void TimerService_SetTimeIsUpLessDateTimeNow_NotTrigger()
    {
        var time = DateTime.Now.Subtract(TimeSpan.FromSeconds(2));
        var action = Substitute.For<IReceivedAction>();
        _timerService = new TimerService(time.ToTimeOnly());
        _timerService.SetCheckInterval(0.5);
        _timerService.TimeIsUp += (_, e) =>
        {
            action.Trigger();
        };

        Task.Delay(3000).Wait();

        action.DidNotReceive();
    }
}