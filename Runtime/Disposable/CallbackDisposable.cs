using System;

namespace Juce.Core.Disposables
{
    public sealed class CallbackDisposable<T> : IDisposable<T>
    {
        readonly Action<T> onDispose;

        private bool disposed;

        public T Value { get; }

        public CallbackDisposable(T value, Action<T> onDispose)
        {
            Value = value;
            this.onDispose = onDispose;
        }

        public void Dispose()
        {
            if (disposed)
            {
                return;
            }

            disposed = true;

            onDispose?.Invoke(Value);
        }
    }
}
