using System;
using System.Threading;
using System.Threading.Tasks;

namespace Juce.Core.Sequencing
{
    public class TaskInstruction : Instruction
    {
        private readonly Func<CancellationToken, Task> function;

        public TaskInstruction(Func<CancellationToken, Task> function)
        {
            this.function = function;
        }

        protected override Task OnExecute(CancellationToken cancellationToken)
        {
            if(function == null)
            {
                return Task.CompletedTask;
            }

            return function.Invoke(cancellationToken);
        }
    }
}