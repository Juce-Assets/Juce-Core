using Juce.Core.DI.Container;
using System;

namespace Juce.Core.DI.Bindings
{
    public interface IDIBinding
    {
        Type IdentifierType { get; }
        Type ActualType { get; }
        object Value { get; }
        bool Lazy { get; }

        void Bind(IDIResolveContainer container);
        void Init(IDIResolveContainer container);
        void Dispose(IDIResolveContainer container);
    }
}
