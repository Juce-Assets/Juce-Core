using Juce.Core.DI.Bindings;
using System;
using System.Collections.Generic;

namespace Juce.Core.DI.Container
{
    public class DIContainer : IDIContainer
    {
        private readonly List<Type> resolvingStack = new List<Type>();

        private readonly Dictionary<Type, IDIBinding> bindings;

        public IReadOnlyDictionary<Type, IDIBinding> Bindings => bindings;

        public DIContainer(Dictionary<Type, IDIBinding> bindings)
        {
            this.bindings = bindings;

            BindNonLazy();
        }

        private void BindNonLazy()
        {
            foreach(KeyValuePair<Type, IDIBinding> binding in bindings)
            {
                if(binding.Value.Lazy)
                {
                    continue;
                }

                Bind(binding.Value);
            }
        }

        private void Bind(IDIBinding binding)
        {
            resolvingStack.Add(binding.IdentifierType);

            binding.Bind(this);

            if (binding.Value == null)
            {
                throw new Exception($"Object of type {binding.IdentifierType.Name} Binding returned a null object");
            }

            resolvingStack.Remove(binding.IdentifierType);

            binding.Init(this);
        }

        public T Resolve<T>()
        {
            Type type = typeof(T);

            bool isCircularDependence = resolvingStack.Contains(type);

            if (isCircularDependence)
            {
                throw new Exception($"Circular dependence found resolving {type.Name}");
            }

            bool found = bindings.TryGetValue(type, out IDIBinding binding);

            if(!found)
            {
                throw new Exception($"Object of type {type.Name} could not be resolved");
            }

            Bind(binding);

            return (T)binding.Value;
        }

        public void Dispose()
        {
            resolvingStack.Clear();

            foreach (KeyValuePair<Type, IDIBinding> binding in bindings)
            {
                binding.Value.Dispose(this);
            }

            bindings.Clear();
        }
    }
}