using System;
using System.Threading;
using System.Threading.Tasks;

namespace Juce.Core.Time
{
    public class CallbackTimer : ITimer
    {
        private readonly Func<TimeSpan> getTimeCallback;

        private TimeSpan startTime;
        private TimeSpan pausedTime;

        public bool Started { get; private set; }
        public bool Paused { get; private set; }
        public bool StartedAndNotPaused => Started && !Paused;

        public TimeSpan Time
        {
            get
            {
                if (!Started)
                {
                    return TimeSpan.Zero;
                }

                if (Paused)
                {
                    return pausedTime;
                }

                return getTimeCallback.Invoke() - startTime;
            }
        }

        public CallbackTimer(Func<TimeSpan> getTimeCallback)
        {
            this.getTimeCallback = getTimeCallback;;
        }

        public void Start()
        {
            if (Started)
            {
                return;
            }

            Started = true;

            startTime = getTimeCallback.Invoke();
        }

        public void Pause()
        {
            if (!Started)
            {
                return;
            }

            if (Paused)
            {
                return;
            }

            pausedTime = Time;

            Paused = true;
        }

        public void Resume()
        {
            if (!Started)
            {
                return;
            }

            if (!Paused)
            {
                return;
            }

            Paused = false;

            startTime = getTimeCallback.Invoke() - pausedTime;
        }

        public void Reset()
        {
            Started = false;
            Paused = false;

            startTime = TimeSpan.Zero;
            pausedTime = TimeSpan.Zero;
        }

        public void Restart()
        {
            Reset();
            Start();
        }

        public bool HasReached(TimeSpan timeSpan)
        {
            if (!Started)
            {
                return false;
            }

            return TimeSpan.Compare(timeSpan, Time) == -1;
        }

        public async Task AwaitReach(TimeSpan time, CancellationToken cancellationToken)
        {
            while (!HasReached(time) && !cancellationToken.IsCancellationRequested)
            {
                await Task.Yield();
            }
        }

        public Task AwaitTime(TimeSpan time, CancellationToken cancellationToken)
        {
            TimeSpan timeToReach = Time + time;

            return AwaitReach(timeToReach, cancellationToken);
        }
    }
}