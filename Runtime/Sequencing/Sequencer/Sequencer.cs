using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Juce.Core.Sequencing
{
    public sealed class Sequencer : ISequencer
    {
        private readonly Queue<Instruction> instructionQueue = new Queue<Instruction>();

        private TaskCompletionSource<object> taskCompletitionSource;
        private CancellationTokenSource cancellationTokenSource;

        public event Action OnComplete;

        public int Count => instructionQueue.Count;
        public bool Enabled { get; set; } = true;

        private void Play(Instruction instruction)
        {
            if (!Enabled)
            {
                return;
            }

            instructionQueue.Enqueue(instruction);

            TryRunInstructions();
        }

        public void Play(Action action)
        {
            Play(new ActionInstruction(action));
        }

        public void Play(Func<CancellationToken, Task> function)
        {
            Play(new TaskInstruction(function));
        }

        public void Kill()
        {
            if (cancellationTokenSource == null)
            {
                return;
            }

            instructionQueue.Clear();

            cancellationTokenSource.Cancel();
        }

        public Task AwaitCompletition()
        {
            if (taskCompletitionSource == null)
            {
                return Task.CompletedTask;
            }

            return taskCompletitionSource.Task;
        }

        private async void TryRunInstructions()
        {
            if (instructionQueue.Count == 0)
            {
                return;
            }

            if (taskCompletitionSource != null)
            {
                return;
            }

            taskCompletitionSource = new TaskCompletionSource<object>();
            cancellationTokenSource = new CancellationTokenSource();

            while (instructionQueue.Count > 0)
            {
                Instruction currentInstruction = instructionQueue.Dequeue();

                await currentInstruction.Execute(cancellationTokenSource.Token);

                if (cancellationTokenSource.IsCancellationRequested)
                {
                    break;
                }
            }

            cancellationTokenSource.Dispose();
            cancellationTokenSource = null;

            taskCompletitionSource.SetResult(null);
            taskCompletitionSource = null;

            // We check if we can play again to avoid issues with
            // TaskCompletionSource instant instructions
            TryRunInstructions();

            OnComplete?.Invoke();
        }
    }
}
