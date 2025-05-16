using System;
using System.Timers;

namespace SMI.Core {
    public class TimerService : IDisposable, ITimerService
    {
        private TimeOnly _specifyTimeIsUp;
        private readonly Timer _timer = new(TimeSpan.FromMinutes(5).TotalMilliseconds);
        private int _currentDay;

        public event EventHandler TimeIsUp;

        public TimerService(TimeOnly specifyTimeIsUp)
        {
            _specifyTimeIsUp = specifyTimeIsUp;
            _timer.Elapsed += OnElapsed;
            _currentDay = DateTime.Now.Day;
            _timer.Start();
        }

        /// <summary>
        /// For Unit Test
        /// </summary>
        /// <param name="interval"></param>
        internal void SetCheckInterval(double interval) => _timer.Interval = interval; 
        
        public void SetTimeIsUp(TimeOnly time)
        {
            _timer.Stop();
            _specifyTimeIsUp = time;
            _timer.Start();
        }

        private void OnElapsed(object sender, ElapsedEventArgs e)
        {
            var now = DateTime.Now;
            if (now.ToTimeOnly() >= _specifyTimeIsUp && now.Day == _currentDay)
            {
                _currentDay = now.AddDays(1).Day;
                TimeIsUp?.Invoke(this, EventArgs.Empty);
            }
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }
    }
}
