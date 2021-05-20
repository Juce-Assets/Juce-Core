using System;

namespace Juce.Core.Disposables
{
    public class Disposable<T> : IDisposable<T>
    {
        private readonly Action onDispose;

        public T Value { get; }

        public Disposable(T value, Action onDispose)
        {
            Value = value;
            this.onDispose = onDispose;
        }

        public void Dispose()
        {
            onDispose?.Invoke();
        }
    }
}
