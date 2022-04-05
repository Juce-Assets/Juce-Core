using Juce.Core.Di.Bindings;
using Juce.Core.Di.Container;
using Juce.Core.Di.Installers;
using System;
using System.Collections.Generic;

namespace Juce.Core.Di.Builder
{
    public class DiContainerBuilderA : IDiContainerBuilderA
    {
        private readonly Dictionary<Type, IDiBindingA> bindings = new Dictionary<Type, IDiBindingA>();

        public void AddBinding(IDiBindingA binding)
        {
            bool alreadyAdded = bindings.ContainsKey(binding.IdentifierType);

            if(alreadyAdded)
            {
                throw new Exception($"Container builder already has a binding " +
                    $"of type {binding.IdentifierType.Name} registered");
            } 

            bindings.Add(binding.IdentifierType, binding);
        }

        public IDiBindingBuilderA<T> Bind<T>()
        {
            return new DiBindingBuilderA<T>(this, typeof(T));
        }

        public IDiBindingBuilderA<TConcrete> Bind<TInterface, TConcrete>()
        {
            Type interfaceType = typeof(TInterface);
            Type concreteType = typeof(TConcrete);

            bool isAssignable = interfaceType.IsAssignableFrom(concreteType);

            if(!isAssignable)
            {
                throw new Exception($"Binding with interface type {interfaceType.Name} is not assignable to " +
                    $"concrete type {concreteType.Name}");
            }

            return new DiBindingBuilderA<TConcrete>(this, typeof(TInterface));
        }

        public void Bind(params IDiContainerA[] containers)
        {
            foreach(IDiContainerA container in containers)
            {
                if (container == null)
                {
                    throw new Exception("There was a null Container while trying to Bind");
                }

                foreach (KeyValuePair<Type, IDiBindingA> binding in container.Bindings)
                {
                    AddBinding(binding.Value);
                }
            }
        }

        public void Bind(IReadOnlyList<IDiContainerA> containers)
        {
            foreach (IDiContainerA container in containers)
            {
                if (container == null)
                {
                    throw new Exception("There was a null Container while trying to Bind");
                }

                foreach (KeyValuePair<Type, IDiBindingA> binding in container.Bindings)
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

        public void Bind(Action<IDiContainerBuilderA> action)
        {
            action?.Invoke(this);
        }

        public IDiContainerA Build()
        {
            return new DiContainerA(bindings);
        }
    }
}