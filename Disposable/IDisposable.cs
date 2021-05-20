using System;

namespace Juce.Core.Disposables
{
    public interface IDisposable<T> : IDisposable
    {
        public T Value { get; }
    }
}
