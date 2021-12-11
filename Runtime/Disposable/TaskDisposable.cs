using System;
using System.Threading.Tasks;

namespace Juce.Core.Disposables
{
    public class TaskDisposable<T> : ITaskDisposable<T>
    {
        private readonly Func<T, Task> onDispose;

        public T Value { get; }

        public TaskDisposable(T value, Func<T, Task> onDispose)
        {
            Value = value;
            this.onDispose = onDispose;
        }

        public Task Dispose()
        {
            return onDispose?.Invoke(Value);
        }
    }
}
