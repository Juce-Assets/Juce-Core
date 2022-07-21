using Juce.Core.Sequencing.Instructions;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Juce.Core.Sequencing.Sequences
{
    public interface ISequencer
    {
        event Action OnComplete;


        bool IsRunning { get; }
        bool Enabled { get; set; }

        void Play(Action action);
        void Play(Func<CancellationToken, Task> function);
        void Play(IInstruction instruction);
        void Kill();
        Task AwaitCompletition();
    }
}
