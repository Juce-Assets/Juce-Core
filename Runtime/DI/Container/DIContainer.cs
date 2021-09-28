using Juce.Core.DI.Bindings;
using System;
using System.Collections.Generic;

namespace Juce.Core.DI.Container
{
    public class DIContainer : IDIContainer
    {
        private readonly Dictionary<Type, IDIBinding> bindings;

        public IReadOnlyDictionary<Type, IDIBinding> Bindings => bindings;

        public DIContainer(Dictionary<Type, IDIBinding> bindings)
        {
            this.bindings = bindings;
        }

        public T Resolve<T>()
        {
            Type type = typeof(T);

            bool found = bindings.TryGetValue(type, out IDIBinding binding);

            if(!found)
            {
                throw new Exception($"Object of type {type.Name} could not be resolved");
            }

            binding.Bind(this);

            if(binding.Value == null)
            {
                throw new Exception($"Object of type {type.Name} Binding returned a null object");
            }

            binding.Init(this);

            return (T)binding.Value;
        }

        public void Dispose()
        {
            foreach(KeyValuePair<Type, IDIBinding> binding in bindings)
            {
                binding.Value.Dispose(this);
            }

            bindings.Clear();
        }
    }
}