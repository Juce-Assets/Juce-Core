using System;
using System.Threading.Tasks;

namespace Juce.Core.Disposables
{
    public sealed class CallbackAsyncDisposable<T> : IAsyncDisposable<T>
    {
        readonly Func<T, Task> onDispose;

        private bool disposed;

        public T Value { get; }


        public CallbackAsyncDisposable(T value, Func<T, Task> onDispose)
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

            return onDispose.Invoke(Value);
        }
    }
}
