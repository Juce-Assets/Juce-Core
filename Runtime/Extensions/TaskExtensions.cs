using System;
using System.Threading.Tasks;

namespace Juce.Core.Extensions
{
    public static class TaskExtensions
    {
        public static async void RunAsync(this Task task, Action onFinish = null)
        {
            await task;

            onFinish?.Invoke();
        }
    }
}