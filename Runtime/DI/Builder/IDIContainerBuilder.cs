using System.Collections.Generic;

namespace Juce.Core.DI
{
    public interface IDIContainerBuilder
    {
        IDIBindingBuilder<T> Bind<T>();
        void Bind(params IDIContainer[] container);
        void Bind(IReadOnlyList<IDIContainer> container);

        IDIContainer Build();
    }
}
