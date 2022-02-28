using System;
using System.Threading.Tasks;

namespace Juce.Core.Loading
{
    public class TaskCallbackLoadingToken : ITaskLoadingToken
    {
        private Func<Task> onCompleted;

        public TaskCallbackLoadingToken(Func<Task> onCompleted)
        {
            this.onCompleted = onCompleted;
        }

        public async Task Complete()
        {
            await onCompleted?.Invoke();

            onCompleted = null;
        }
    }
}
