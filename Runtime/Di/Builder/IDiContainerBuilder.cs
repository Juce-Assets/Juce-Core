using Juce.Core.Di.Container;
using Juce.Core.Di.Installers;
using System;
using System.Collections.Generic;

namespace Juce.Core.Di.Builder
{
    public interface IDiContainerBuilder
    {
        IDiBindingBuilder<T> Bind<T>();
        IDiBindingBuilder<TConcrete> Bind<TInterface, TConcrete>();
        void Bind(params IDiContainer[] containers);
        void Bind(IReadOnlyList<IDiContainer> container);
        void Bind(params IInstaller[] installers);
        void Bind(IReadOnlyList<IInstaller> container);
        void Bind(Action<IDiContainerBuilder> action);

        IDiContainer Build();
    }
}
