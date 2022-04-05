using Juce.Core.Di.Bindings;
using System;
using System.Collections.Generic;

namespace Juce.Core.Di.Container
{
    public interface IDiContainer : IDiResolveContainer, IDisposable
    {
        public IReadOnlyDictionary<Type, IDiBinding> Bindings { get; }
    }
}