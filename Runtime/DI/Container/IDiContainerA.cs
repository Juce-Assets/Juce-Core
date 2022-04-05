using Juce.Core.Di.Bindings;
using System;
using System.Collections.Generic;

namespace Juce.Core.Di.Container
{
    public interface IDiContainerA : IDiResolveContainerA, IDisposable
    {
        public IReadOnlyDictionary<Type, IDiBindingA> Bindings { get; }
    }
}