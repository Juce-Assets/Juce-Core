using Juce.Core.DI.Container;
using System;

namespace Juce.Core.DI.Builder
{
    public interface IDIBindingBuilder<T>
    {
        IDIBindingActionBuilder<T> FromNew();
        IDIBindingActionBuilder<T> FromInstance(T instance);
        IDIBindingActionBuilder<T> FromFunction(Func<IDIResolveContainer, T> func);
        IDIBindingActionBuilder<T> FromContainer(IDIContainer container);
    }
}