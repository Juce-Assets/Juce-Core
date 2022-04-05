using Juce.Core.Di.Bindings;
using Juce.Core.Di.Container;
using Juce.Core.Di.Installers;
using System;
using System.Collections.Generic;

namespace Juce.Core.Di.Builder
{
    public class DiContainerBuilder : IDiContainerBuilder
    {
        private readonly Dictionary<Type, IDiBinding> bindings = new Dictionary<Type, IDiBinding>();

        public void AddBinding(IDiBinding binding)
        {
            bool alreadyAdded = bindings.ContainsKey(binding.IdentifierType);

            if(alreadyAdded)
            {
                throw new Exception($"Container builder already has a binding " +
                    $"of type {binding.IdentifierType.Name} registered");
            } 

            bindings.Add(binding.IdentifierType, binding);
        }

        public IDiBindingBuilder<T> Bind<T>()
        {
            return new DiBindingBuilder<T>(this, typeof(T));
        }

        public IDiBindingBuilder<TConcrete> Bind<TInterface, TConcrete>()
        {
            Type interfaceType = typeof(TInterface);
            Type concreteType = typeof(TConcrete);

            bool isAssignable = interfaceType.IsAssignableFrom(concreteType);

            if(!isAssignable)
            {
                throw new Exception($"Binding with interface type {interfaceType.Name} is not assignable to " +
                    $"concrete type {concreteType.Name}");
            }

            return new DiBindingBuilder<TConcrete>(this, typeof(TInterface));
        }

        public void Bind(params IDiContainer[] containers)
        {
            foreach(IDiContainer container in containers)
            {
                if (container == null)
                {
                    throw new Exception("There was a null Container while trying to Bind");
                }

                foreach (KeyValuePair<Type, IDiBinding> binding in container.Bindings)
                {
                    AddBinding(binding.Value);
                }
            }
        }

        public void Bind(IReadOnlyList<IDiContainer> containers)
        {
            foreach (IDiContainer container in containers)
            {
                if (container == null)
                {
                    throw new Exception("There was a null Container while trying to Bind");
                }

                foreach (KeyValuePair<Type, IDiBinding> binding in container.Bindings)
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

        public void Bind(Action<IDiContainerBuilder> action)
        {
            action?.Invoke(this);
        }

        public IDiContainer Build()
        {
            return new DiContainer(bindings);
        }
    }
}