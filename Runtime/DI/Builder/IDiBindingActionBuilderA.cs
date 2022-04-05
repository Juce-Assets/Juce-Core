using Juce.Core.Di.Container;
using System;

namespace Juce.Core.Di.Builder
{
    public interface IDiBindingActionBuilderA<T>
    {
        Type IdentifierType { get; }
        Type ActualType { get; }

        IDiBindingActionBuilderA<T> NonLazy();
        IDiBindingActionBuilderA<T> WhenInit(Action<IDiResolveContainerA, T> action);
        IDiBindingActionBuilderA<T> WhenInit(Func<T, Action> func);
        IDiBindingActionBuilderA<T> WhenDispose(Action<IDiResolveContainerA, T> action);
        IDiBindingActionBuilderA<T> WhenDispose(Action<T> func);
    }
}