using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Juce.Core.Sequencing
{
    public class Sequencer
    {
        private readonly Queue<Instruction> instructionQueue = new Queue<Instruction>();

        private CancellationTokenSource cancellationTokenSource;

        public void Play(Instruction instruction)
        {
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

            cancellationTokenSource = new CancellationTokenSource();

            while (instructionQueue.Count > 0)
            {
                Instruction currentInstruction = instructionQueue.Dequeue();

                await currentInstruction.Execute(cancellationTokenSource.Token);
            }

            cancellationTokenSource = null;
        }
    }
}
