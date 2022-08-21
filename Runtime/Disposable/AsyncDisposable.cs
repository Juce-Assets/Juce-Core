using System;
using System.Threading.Tasks;

namespace Juce.Core.Disposables
{
    public class AsyncDisposable<T> : IAsyncDisposable<T>
    {
        private readonly Func<T, Task> onDispose;

        private bool disposed;

        public T Value { get; }

        public AsyncDisposable(T value, Func<T, Task> onDispose)
        {
            Value = value;
            this.onDispose = onDispose;
        }

        public Task DisposeAsync()
        {
            if(disposed)
            {
                return Task.CompletedTask;
            }

            disposed = true;

            return onDispose?.Invoke(Value);
        }
    }
}
