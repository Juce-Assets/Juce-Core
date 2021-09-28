using Juce.Core.DI.Container;
using System;

namespace Juce.Core.DI.Builder
{
    public interface IDIBindingActionBuilder<T>
    {
        IDIBindingActionBuilder<T> WhenInit(Action<IDIResolveContainer, T> action);
        IDIBindingActionBuilder<T> WhenInit(Func<T, Action> func);
        IDIBindingActionBuilder<T> WhenDispose(Action<T> func);
    }
}