using System;

namespace Juce.Core.DI
{
    public interface IDIBinding
    {
        Type Type { get; }
        object Value { get; }

        void Bind(IDIResolveContainer container);
        void Init(IDIResolveContainer container);
        void Dispose(IDIResolveContainer container);
    }
}
