using Juce.Core.DI.Container;
using System;

namespace Juce.Core.DI.Builder
{
    public interface IDIBindingActionBuilder<T>
    {
        void WhenInit(Action<IDIResolveContainer, T> func);
        void WhenInit(Func<T, Action> func);
        void WhenDispose(Action<T> func);
    }
}