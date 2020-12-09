using System.Threading.Tasks;

namespace Juce.Core.Sequencing
{
    public abstract class AsyncInstruction : Instruction
    {
        protected override void OnStart()
        {
            OnAsyncStart().ContinueWith((Task result) =>
            {
                MarkAsCompleted();
            }, TaskScheduler.FromCurrentSynchronizationContext());
        }

        protected abstract Task OnAsyncStart();
    }
}