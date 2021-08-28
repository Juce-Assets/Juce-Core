using System.Threading;
using System.Threading.Tasks;

namespace Juce.Core.Sequencing
{
    public abstract class Instruction
    {
        public Task Execute(CancellationToken cancellationToken)
        {
            if(cancellationToken.IsCancellationRequested)
            {
                return Task.CompletedTask;
            }

            return OnExecute(cancellationToken);
        }

        protected abstract Task OnExecute(CancellationToken cancellationToken);
    }
}