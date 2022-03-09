using System;
using System.Threading;
using System.Threading.Tasks;

namespace Juce.Core.Time
{
    public class CallbackTimer : ITimer
    {
        private readonly Func<TimeSpan> getTimeCallback;

        private bool started;
        private TimeSpan startTime;

        private bool paused;
        private TimeSpan pausedTime;

        public TimeSpan Time
        {
            get
            {
                if (!started)
                {
                    return TimeSpan.Zero;
                }

                if (paused)
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
            if (started)
            {
                return;
            }

            started = true;

            startTime = getTimeCallback.Invoke();
        }

        public void Pause()
        {
            if (!started)
            {
                return;
            }

            if (paused)
            {
                return;
            }

            pausedTime = Time;

            paused = true;
        }

        public void Resume()
        {
            if (!started)
            {
                return;
            }

            if (!paused)
            {
                return;
            }

            paused = false;

            startTime = getTimeCallback.Invoke() - pausedTime;
        }

        public void Reset()
        {
            started = false;
            paused = false;

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
            if (!started)
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