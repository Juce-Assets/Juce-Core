using System;
using System.Collections.Generic;

namespace Juce.Core.DI
{
    public class DIContainerBuilder : IDIContainerBuilder
    {
        private readonly Dictionary<Type, IDIBinding> bindings = new Dictionary<Type, IDIBinding>();

        public void AddBinding(IDIBinding binding)
        {
            bool alreadyAdded = bindings.ContainsKey(binding.Type);

            if(alreadyAdded)
            {
                throw new Exception($"Container builder already has a binding " +
                    $"of type {binding.Type.Name} registered");
            } 

            bindings.Add(binding.Type, binding);
        }

        public IDIBindingBuilder<T> Bind<T>()
        {
            return new DIBindingBuilder<T>(this);
        }

        public void Bind(params IDIContainer[] containers)
        {
            foreach(IDIContainer container in containers)
            {
                foreach(KeyValuePair<Type, IDIBinding> binding in container.Bindings)
                {
                    AddBinding(binding.Value);
                }
            }
        }

        public void Bind(IReadOnlyList<IDIContainer> containers)
        {
            foreach (IDIContainer container in containers)
            {
                foreach (KeyValuePair<Type, IDIBinding> binding in container.Bindings)
                {
                    AddBinding(binding.Value);
                }
            }
        }

        public IDIContainer Build()
        {
            return new DIContainer(bindings);
        }
    }
}