using System;
using System.Collections.Generic;

namespace Juce.Core.DI
{
    public interface IDIContainer : IDIResolveContainer, IDisposable
    {
        public IReadOnlyDictionary<Type, IDIBinding> Bindings { get; }
    }
}