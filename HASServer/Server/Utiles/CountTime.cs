using System;
using System.Diagnostics;
using System.Threading;

namespace Server.Utiles
{
    internal class CountTime
    {
        private Timer _timer;
        private Stopwatch _stopWatch;
        private int _endTime;
        private int _timeInterval;
        private Action<double> _elapsed;
        private Action _callback;
        public bool IsRunning { get; private set; }
        public CountTime(Action<double> elapsed, Action callback, int endTime, int interval)
        {
            _callback = callback;
            _elapsed = elapsed;
            _stopWatch = new Stopwatch();
            _timeInterval = interval;
            _endTime = endTime;
            IsRunning = false;
        }
        public CountTime(Action<double> elapsed, Action callback, int interval)
        {
            _callback = callback;
            _elapsed = elapsed;
            _stopWatch = new Stopwatch();
            _timeInterval = interval;
            IsRunning = false;
        }
        public void SetEndTime(int endTime)
        {
            _endTime = endTime;
        }
        public void StartCount()
        {
            IsRunning = true;
            _stopWatch.Restart();
            _timer = new Timer(HandleTimerElapsed, null, 0, _timeInterval);
        }
        private void HandleTimerElapsed(object state)
        {
            double elapsed = _stopWatch.Elapsed.TotalSeconds;
            _elapsed?.Invoke(elapsed);
            if (elapsed > _endTime)
            {
                IsRunning = false;
                _callback?.Invoke();
                _timer?.Dispose();
            }
        }
        public void Abort(bool invokeCallback = true)
        {
            Console.WriteLine("Count Dispose");
            _timer.Dispose();
            IsRunning = false;
            if (invokeCallback)
                _callback?.Invoke();
        }
    }
}
