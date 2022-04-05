using Juce.Core.Di.Container;
using Juce.Core.Di.Installers;
using System;
using System.Collections.Generic;

namespace Juce.Core.Di.Builder
{
    public interface IDiContainerBuilderA
    {
        IDiBindingBuilderA<T> Bind<T>();
        IDiBindingBuilderA<TConcrete> Bind<TInterface, TConcrete>();
        void Bind(params IDiContainerA[] containers);
        void Bind(IReadOnlyList<IDiContainerA> container);
        void Bind(params IInstaller[] installers);
        void Bind(IReadOnlyList<IInstaller> container);
        void Bind(Action<IDiContainerBuilderA> action);

        IDiContainerA Build();
    }
}
