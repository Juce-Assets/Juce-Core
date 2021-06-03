using System;

namespace Juce.Core.Time
{
    public class Timer : ITimer
    {
        private bool started;
        private TimeSpan startTime;

        private bool paused;
        private TimeSpan pausedTime;

        public ITimeContext TimeContext { get; private set; }

        public TimeSpan Time
        {
            get
            {
                if (!started)
                {
                    return TimeSpan.Zero;
                }

                if(paused)
                {
                    return pausedTime;
                }

                return TimeContext.Time - startTime;
            }
        }

        public Timer(TickableTimeContext tickableTimeContext)
        {
            if (tickableTimeContext == null)
            {
                throw new ArgumentNullException($"Null {nameof(TickableTimeContext)} at {nameof(Timer)}");
            }

            TimeContext = tickableTimeContext;
        }

        public void Start()
        {
            if (started)
            {
                return;
            }

            started = true;

            startTime = TimeContext.Time;
        }

        public void Pause()
        {
            if (!started)
            {
                return;
            }

            if(paused)
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

            startTime = TimeContext.Time - pausedTime;
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
    }
}