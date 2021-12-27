using System;
using System.Threading;
using System.Threading.Tasks;

namespace Juce.Core.Sequencing
{
    public interface ISequencer
    {
        bool Enabled { get; set; }

        void Play(Action action);
        void Play(Func<CancellationToken, Task> function);
        void Kill();
        Task WaitCompletition();
    }
}
