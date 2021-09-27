using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Juce.Core.Sequencing
{
    public class Sequencer : ISequencer
    { 
        private readonly Queue<Instruction> instructionQueue = new Queue<Instruction>();

        private TaskCompletionSource<object> taskCompletitionSource;
        private CancellationTokenSource cancellationTokenSource;

        private int playingCount = 0;

        public bool Enabled { get; set; } = true;

        public void Play(Instruction instruction)
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
            cancellationTokenSource = null;

            taskCompletitionSource.SetResult(null);
            taskCompletitionSource = null;
        }

        public Task WaitCompletition()
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

            if (cancellationTokenSource != null)
            {
                return;
            }

            taskCompletitionSource = new TaskCompletionSource<object>();
            cancellationTokenSource = new CancellationTokenSource();

            while (instructionQueue.Count > 0)
            {
                Instruction currentInstruction = instructionQueue.Dequeue();

                ++playingCount;

                await currentInstruction.Execute(cancellationTokenSource.Token);

                --playingCount;
            }

            // Normally, we won't have more than one instruction playing at once, but could happen if
            // an instruction is enqued while inside another instruction execution
            if (playingCount > 0)
            {
                return;
            }

            taskCompletitionSource.SetResult(null);

            taskCompletitionSource = null;
            cancellationTokenSource = null;
        }
    }
}
