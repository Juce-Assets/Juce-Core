using System;
using System.Threading;
using System.Threading.Tasks;

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
        Task AwaitReach(TimeSpan time, CancellationToken cancellationToken);
        Task AwaitTime(TimeSpan time, CancellationToken cancellationToken);
    }
}