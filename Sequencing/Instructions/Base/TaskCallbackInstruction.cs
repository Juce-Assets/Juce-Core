using System;
using System.Threading;
using System.Threading.Tasks;

namespace Juce.Core.Sequencing
{
    public class TaskCallbackInstruction : AsyncInstruction
    {
        private readonly Func<CancellationToken, Task> action;

        public TaskCallbackInstruction(Func<CancellationToken, Task> action)
        {
            this.action = action;
        }

        protected override Task OnAsyncStart()
        {
            return action?.Invoke(default);
        }
    }
}