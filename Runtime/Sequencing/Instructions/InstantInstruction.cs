using System.Threading;
using System.Threading.Tasks;

namespace Juce.Core.Sequencing.Instructions
{
    public abstract class InstantInstruction : Instruction
    {
        protected sealed override Task OnExecute(CancellationToken cancellationToken)
        {
            OnInstantExecute();

            return Task.CompletedTask;
        }

        protected abstract void OnInstantExecute();
    }
}