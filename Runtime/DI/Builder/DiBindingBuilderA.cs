using Juce.Core.Di.Bindings;
using Juce.Core.Di.Container;
using System;

namespace Juce.Core.Di.Builder
{
    public class DiBindingBuilderA<T> : IDiBindingBuilderA<T>
    {
        private readonly DiContainerBuilderA containerBuilder;
        private readonly Type identifierType;

        public DiBindingBuilderA(
            DiContainerBuilderA containerBuilder,
            Type identifierType
            )
        {
            this.containerBuilder = containerBuilder;
            this.identifierType = identifierType;
        }

        public IDiBindingActionBuilderA<T> FromNew()
        {
            Type type = typeof(T);

            bool canBeCreated = type.GetConstructor(Type.EmptyTypes) != null && !type.IsAbstract;

            if(!canBeCreated)
            {
                throw new Exception($"Object of type {type.Name} cannot be instantiated on runtime");
            }

            DiBindingA binding = new NewInstanceBinding(identifierType, type);

            return AddBinding(binding);
        }

        public IDiBindingActionBuilderA<T> FromInstance(T instance)
        {
            Type type = typeof(T);

            DiBindingA binding = new ReferenceInstanceBinding(identifierType, type, instance);

            return AddBinding(binding);
        }

        public IDiBindingActionBuilderA<T> FromFunction(Func<IDiResolveContainerA, T> func)
        {
            Type type = typeof(T);

            Func<IDiResolveContainerA, object> castedFunc = (IDiResolveContainerA c) => func.Invoke(c);

            DiBindingA binding = new FuncInstanceBinding(identifierType, type, castedFunc);

            return AddBinding(binding);
        }

        public IDiBindingActionBuilderA<T> FromContainer(IDiContainerA container)
        {
            return FromFunction((c) => container.Resolve<T>());
        }

        private DiBindingActionBuilderA<T> AddBinding(DiBindingA binding)
        {
            containerBuilder.AddBinding(binding);

            return new DiBindingActionBuilderA<T>(binding);
        }
    }
}