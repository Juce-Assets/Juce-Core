using Juce.Core.Di.Container;
using System;

namespace Juce.Core.Di.Bindings
{
    public interface IDiBindingA
    {
        Type IdentifierType { get; }
        Type ActualType { get; }
        object Value { get; }
        bool Lazy { get; }

        void Bind(IDiResolveContainerA container);
        void Init(IDiResolveContainerA container);
        void Dispose(IDiResolveContainerA container);
    }
}
