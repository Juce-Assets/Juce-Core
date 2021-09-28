using Juce.Core.DI.Bindings;
using System;
using System.Collections.Generic;

namespace Juce.Core.DI.Container
{
    public interface IDIContainer : IDIResolveContainer, IDisposable
    {
        public IReadOnlyDictionary<Type, IDIBinding> Bindings { get; }
    }
}