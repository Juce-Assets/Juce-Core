using Juce.Core.DI.Container;
using Juce.Core.DI.Installers;
using System;
using System.Collections.Generic;

namespace Juce.Core.DI.Builder
{
    public interface IDIContainerBuilder
    {
        IDIBindingBuilder<T> Bind<T>();
        IDIBindingBuilder<TConcrete> Bind<TInterface, TConcrete>();
        void Bind(params IDIContainer[] containers);
        void Bind(IReadOnlyList<IDIContainer> container);
        void Bind(params IInstaller[] installers);
        void Bind(IReadOnlyList<IInstaller> container);
        void Bind(Action<IDIContainerBuilder> action);

        IDIContainer Build();
    }
}
