using Juce.Core.DI.Bindings;
using Juce.Core.DI.Container;
using Juce.Core.DI.Installers;
using System;
using System.Collections.Generic;

namespace Juce.Core.DI.Builder
{
    public class DIContainerBuilder : IDIContainerBuilder
    {
        private readonly Dictionary<Type, IDIBinding> bindings = new Dictionary<Type, IDIBinding>();

        public void AddBinding(IDIBinding binding)
        {
            bool alreadyAdded = bindings.ContainsKey(binding.IdentifierType);

            if(alreadyAdded)
            {
                throw new Exception($"Container builder already has a binding " +
                    $"of type {binding.IdentifierType.Name} registered");
            } 

            bindings.Add(binding.IdentifierType, binding);
        }

        public IDIBindingBuilder<T> Bind<T>()
        {
            return new DIBindingBuilder<T>(this, typeof(T));
        }

        public IDIBindingBuilder<TConcrete> Bind<TInterface, TConcrete>()
        {
            Type interfaceType = typeof(TInterface);
            Type concreteType = typeof(TConcrete);

            bool isAssignable = interfaceType.IsAssignableFrom(concreteType);

            if(!isAssignable)
            {
                throw new Exception($"Binding with interface type {interfaceType.Name} is not assignable to " +
                    $"concrete type {concreteType.Name}");
            }

            return new DIBindingBuilder<TConcrete>(this, typeof(TInterface));
        }

        public void Bind(params IDIContainer[] containers)
        {
            foreach(IDIContainer container in containers)
            {
                if (container == null)
                {
                    throw new Exception("There was a null Container while trying to Bind");
                }

                foreach (KeyValuePair<Type, IDIBinding> binding in container.Bindings)
                {
                    AddBinding(binding.Value);
                }
            }
        }

        public void Bind(IReadOnlyList<IDIContainer> containers)
        {
            foreach (IDIContainer container in containers)
            {
                if (container == null)
                {
                    throw new Exception("There was a null Container while trying to Bind");
                }

                foreach (KeyValuePair<Type, IDIBinding> binding in container.Bindings)
                {
                    AddBinding(binding.Value);
                }
            }
        }

        public void Bind(params IInstaller[] installers)
        {
            foreach (IInstaller installer in installers)
            {
                if(installer == null)
                {
                    throw new Exception("There was a null Installer while trying to Bind");
                }

                installer.Install(this);
            }
        }

        public void Bind(IReadOnlyList<IInstaller> installers)
        {
            foreach (IInstaller installer in installers)
            {
                if (installer == null)
                {
                    throw new Exception("There was a null Installer while trying to Bind");
                }

                installer.Install(this);
            }
        }

        public void Bind(Action<IDIContainerBuilder> action)
        {
            action?.Invoke(this);
        }

        public IDIContainer Build()
        {
            return new DIContainer(bindings);
        }
    }
}