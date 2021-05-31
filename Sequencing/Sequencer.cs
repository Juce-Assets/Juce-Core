using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Juce.Core.Sequencing
{
    public class Sequencer
    {
        private readonly Queue<Instruction> instructionQueue = new Queue<Instruction>();

        private TaskCompletionSource<object> taskCompletitionSource;
        private CancellationTokenSource cancellationTokenSource;

        public bool Enabled { get; set; } = true;

        public void Play(Instruction instruction)
        {
            if(!Enabled)
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
            if(cancellationTokenSource == null)
            {
                return;
            }

            cancellationTokenSource.Cancel();
        }

        public Task WaitCompletition()
        {
            if(taskCompletitionSource == null)
            {
                return Task.CompletedTask;
            }

            return taskCompletitionSource.Task;
        }

        private async void TryRunInstructions()
        {
            if(instructionQueue.Count == 0)
            {
                return;
            }

            if(cancellationTokenSource != null)
            {
                return;
            }

            taskCompletitionSource = new TaskCompletionSource<object>();
            cancellationTokenSource = new CancellationTokenSource();

            while (instructionQueue.Count > 0)
            {
                Instruction currentInstruction = instructionQueue.Dequeue();

                await currentInstruction.Execute(cancellationTokenSource.Token);
            }

            taskCompletitionSource.SetResult(null);
            cancellationTokenSource = null;
        }
    }
}
