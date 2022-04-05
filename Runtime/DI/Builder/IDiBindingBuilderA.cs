using Juce.Core.Di.Container;
using System;

namespace Juce.Core.Di.Builder
{
    public interface IDiBindingBuilderA<T>
    {
        IDiBindingActionBuilderA<T> FromNew();
        IDiBindingActionBuilderA<T> FromInstance(T instance);
        IDiBindingActionBuilderA<T> FromFunction(Func<IDiResolveContainerA, T> func);
        IDiBindingActionBuilderA<T> FromContainer(IDiContainerA container);
    }
}