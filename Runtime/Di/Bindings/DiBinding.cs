using Juce.Core.Di.BindingActions;
using Juce.Core.Di.Container;
using System;
using System.Collections.Generic;

namespace Juce.Core.Di.Bindings
{
    public abstract class DiBinding : IDiBinding
    {
        private readonly List<IDiBindingAction> initActions = new List<IDiBindingAction>();
        private readonly List<IDiBindingAction> disposeActions = new List<IDiBindingAction>();

        private bool binded;

        public Type IdentifierType { get; }
        public Type ActualType { get; }
        public object Value { get; private set; }
        public bool Lazy { get; private set; } = true;

        public DiBinding(Type identifierType, Type actualType)
        {
            IdentifierType = identifierType;
            ActualType = actualType;
        }

        public void NonLazy()
        {
            Lazy = false;
        }

        public void AddInitAction(IDiBindingAction initAction)
        {
            if (binded)
            {
                return;
            }

            initActions.Add(initAction);
        }

        public void AddDisposeAction(IDiBindingAction disposeAction)
        {
            if (binded)
            {
                return;
            }

            disposeActions.Add(disposeAction);
        }

        public void Bind(IDiResolveContainer container)
        {
            if(binded)
            {
                return;
            }

            binded = true;

            Value = OnBind(container);
        }

        public void Init(IDiResolveContainer container)
        {
            if (!binded)
            {
                return;
            }

            if (Value == null)
            {
                return;
            }

            foreach (IDiBindingAction initAction in initActions)
            {
                initAction.Execute(container, Value);
            }

            initActions.Clear();
        }

        public void Dispose(IDiResolveContainer container)
        {
            if (!binded)
            {
                return;
            }

            if (Value == null)
            {
                return;
            }

            foreach (IDiBindingAction disposeAction in disposeActions)
            {
                disposeAction.Execute(container, Value);
            }

            disposeActions.Clear();
        }

        protected abstract object OnBind(IDiResolveContainer container);
    }
}
