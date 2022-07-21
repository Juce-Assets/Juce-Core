using System;
using System.Threading.Tasks;

namespace Juce.Core.Extensions
{
    public static class TaskExtensions
    {
        public static async Task AwaitUntil(Func<bool> func)
        {
            while (!func.Invoke())
            {
                await Task.Yield();
            }
        }

        public static async void RunAsync(this Task task)
        {
            await task;
        }

        public static async void RunAsync(this Task task, Action<Exception> onException)
        {
            try
            {
                await task;
            }
            catch (Exception exception)
            {
                onException?.Invoke(exception);
            }
        }

        public static async void RunAsync(this Task task, Action onComplete)
        {
            await task;

            onComplete.Invoke();
        }
    }
}