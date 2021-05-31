using System;

namespace Juce.Core.Disposables
{
    public class Disposable<T> : IDisposable<T>
    {
        private readonly Action<T> onDispose;

        public T Value { get; }

        public Disposable(T value, Action<T> onDispose)
        {
            Value = value;
            this.onDispose = onDispose;
        }

        public void Dispose()
        {
            onDispose?.Invoke(Value);
        }
    }
}
