using Juce.Core.Di.Container;
using System;

namespace Juce.Core.Di.Builder
{
    public interface IDiBindingBuilder<T>
    {
        IDiBindingActionBuilder<T> FromNew();
        IDiBindingActionBuilder<T> FromInstance(T instance);
        IDiBindingActionBuilder<T> FromFunction(Func<IDiResolveContainer, T> func);
        IDiBindingActionBuilder<T> FromContainer(IDiContainer container);
    }
}