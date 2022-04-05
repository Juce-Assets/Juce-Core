using Juce.Core.Di.Bindings;
using Juce.Core.Di.Container;
using System;

namespace Juce.Core.Di.Builder
{
    public class DiBindingBuilder<T> : IDiBindingBuilder<T>
    {
        private readonly DiContainerBuilder containerBuilder;
        private readonly Type identifierType;

        public DiBindingBuilder(
            DiContainerBuilder containerBuilder,
            Type identifierType
            )
        {
            this.containerBuilder = containerBuilder;
            this.identifierType = identifierType;
        }

        public IDiBindingActionBuilder<T> FromNew()
        {
            Type type = typeof(T);

            bool canBeCreated = type.GetConstructor(Type.EmptyTypes) != null && !type.IsAbstract;

            if(!canBeCreated)
            {
                throw new Exception($"Object of type {type.Name} cannot be instantiated on runtime");
            }

            DiBinding binding = new NewInstanceBinding(identifierType, type);

            return AddBinding(binding);
        }

        public IDiBindingActionBuilder<T> FromInstance(T instance)
        {
            Type type = typeof(T);

            DiBinding binding = new ReferenceInstanceBinding(identifierType, type, instance);

            return AddBinding(binding);
        }

        public IDiBindingActionBuilder<T> FromFunction(Func<IDiResolveContainer, T> func)
        {
            Type type = typeof(T);

            Func<IDiResolveContainer, object> castedFunc = (IDiResolveContainer c) => func.Invoke(c);

            DiBinding binding = new FuncInstanceBinding(identifierType, type, castedFunc);

            return AddBinding(binding);
        }

        public IDiBindingActionBuilder<T> FromContainer(IDiContainer container)
        {
            return FromFunction((c) => container.Resolve<T>());
        }

        private DiBindingActionBuilder<T> AddBinding(DiBinding binding)
        {
            containerBuilder.AddBinding(binding);

            return new DiBindingActionBuilder<T>(binding);
        }
    }
}