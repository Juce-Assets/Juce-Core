using Juce.Core.Di.BindingActions;
using Juce.Core.Di.Container;
using System;
using System.Collections.Generic;

namespace Juce.Core.Di.Bindings
{
    public abstract class DiBindingA : IDiBindingA
    {
        private readonly List<IDiBindingActionA> initActions = new List<IDiBindingActionA>();
        private readonly List<IDiBindingActionA> disposeActions = new List<IDiBindingActionA>();

        private bool binded;

        public Type IdentifierType { get; }
        public Type ActualType { get; }
        public object Value { get; private set; }
        public bool Lazy { get; private set; } = true;

        public DiBindingA(Type identifierType, Type actualType)
        {
            IdentifierType = identifierType;
            ActualType = actualType;
        }

        public void NonLazy()
        {
            Lazy = false;
        }

        public void AddInitAction(IDiBindingActionA initAction)
        {
            if (binded)
            {
                return;
            }

            initActions.Add(initAction);
        }

        public void AddDisposeAction(IDiBindingActionA disposeAction)
        {
            if (binded)
            {
                return;
            }

            disposeActions.Add(disposeAction);
        }

        public void Bind(IDiResolveContainerA container)
        {
            if(binded)
            {
                return;
            }

            binded = true;

            Value = OnBind(container);
        }

        public void Init(IDiResolveContainerA container)
        {
            if (!binded)
            {
                return;
            }

            if (Value == null)
            {
                return;
            }

            foreach (IDiBindingActionA initAction in initActions)
            {
                initAction.Execute(container, Value);
            }

            initActions.Clear();
        }

        public void Dispose(IDiResolveContainerA container)
        {
            if (!binded)
            {
                return;
            }

            if (Value == null)
            {
                return;
            }

            foreach (IDiBindingActionA disposeAction in disposeActions)
            {
                disposeAction.Execute(container, Value);
            }

            disposeActions.Clear();
        }

        protected abstract object OnBind(IDiResolveContainerA container);
    }
}
