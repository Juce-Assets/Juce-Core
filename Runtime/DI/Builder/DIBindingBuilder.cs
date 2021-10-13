using Juce.Core.DI.Bindings;
using Juce.Core.DI.Container;
using System;

namespace Juce.Core.DI.Builder
{
    public class DIBindingBuilder<T> : IDIBindingBuilder<T>
    {
        private readonly DIContainerBuilder containerBuilder;
        private readonly Type identifierType;

        public DIBindingBuilder(
            DIContainerBuilder containerBuilder,
            Type identifierType
            )
        {
            this.containerBuilder = containerBuilder;
            this.identifierType = identifierType;
        }

        public IDIBindingActionBuilder<T> FromNew()
        {
            Type type = typeof(T);

            bool canBeCreated = type.GetConstructor(Type.EmptyTypes) != null && !type.IsAbstract;

            if(!canBeCreated)
            {
                throw new Exception($"Object of type {type.Name} cannot be instantiated on runtime");
            }

            DIBinding binding = new NewInstanceBinding(identifierType, type);

            return AddBinding(binding);
        }

        public IDIBindingActionBuilder<T> FromInstance(T instance)
        {
            Type type = typeof(T);

            DIBinding binding = new ReferenceInstanceBinding(identifierType, type, instance);

            return AddBinding(binding);
        }

        public IDIBindingActionBuilder<T> FromFunction(Func<IDIResolveContainer, T> func)
        {
            Type type = typeof(T);

            Func<IDIResolveContainer, object> castedFunc = (IDIResolveContainer c) => func.Invoke(c);

            DIBinding binding = new FuncInstanceBinding(identifierType, type, castedFunc);

            return AddBinding(binding);
        }

        public IDIBindingActionBuilder<T> FromContainer(IDIContainer container)
        {
            return FromFunction((c) => container.Resolve<T>());
        }

        private DIBindingActionBuilder<T> AddBinding(DIBinding binding)
        {
            containerBuilder.AddBinding(binding);

            return new DIBindingActionBuilder<T>(binding);
        }
    }
}