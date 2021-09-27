using System;

namespace Juce.Core.DI
{
    public class DIBindingBuilder<T> : IDIBindingBuilder<T>
    {
        private readonly DIContainerBuilder containerBuilder;

        public DIBindingBuilder(DIContainerBuilder containerBuilder)
        {
            this.containerBuilder = containerBuilder;
        }

        public IDIBindingActionBuilder<T> FromNew()
        {
            Type type = typeof(T);

            bool canBeCreated = type.GetConstructor(Type.EmptyTypes) != null && !type.IsAbstract;

            if(!canBeCreated)
            {
                throw new Exception($"Object of type {type.Name} cannot be instantiated on runtime");
            }

            DIBinding binding = new NewInstanceBinding(type);

            return AddBinding<T>(binding);
        }

        public IDIBindingActionBuilder<T> FromInstance(T instance)
        {
            Type type = typeof(T);

            DIBinding binding = new ReferenceInstanceBinding(type, instance);

            return AddBinding<T>(binding);
        }

        public IDIBindingActionBuilder<T> FromFunction(Func<IDIResolveContainer, T> func)
        {
            Type type = typeof(T);

            Func<IDIResolveContainer, object> castedFunc = (IDIResolveContainer c) => func.Invoke(c);

            DIBinding binding = new FuncInstanceBinding(type, castedFunc);

            return AddBinding<T>(binding);
        }

        private DIBindingActionBuilder<T> AddBinding<T>(DIBinding binding)
        {
            containerBuilder.AddBinding(binding);

            return new DIBindingActionBuilder<T>(binding);
        }
    }
}