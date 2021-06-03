using System;

namespace Juce.Core.Time
{
    public interface ITimer
    {
        ITimeContext TimeContext { get; }
        TimeSpan Time { get; }

        void Start();
        void Pause();
        void Resume();
        void Reset();
        void Restart();
        bool HasReached(TimeSpan time);
    }
}