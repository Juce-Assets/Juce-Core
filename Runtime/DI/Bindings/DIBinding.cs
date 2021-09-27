﻿using System;
using System.Collections.Generic;

namespace Juce.Core.DI
{
    public abstract class DIBinding : IDIBinding
    {
        private readonly List<IDIBindingAction> initActions = new List<IDIBindingAction>();
        private readonly List<IDIBindingAction> disposeActions = new List<IDIBindingAction>();

        private bool binded;

        public Type Type { get; }
        public object Value { get; private set; }

        public DIBinding(Type type)
        {
            Type = type;
        }

        public void AddInitAction(IDIBindingAction initAction)
        {
            if (binded)
            {
                return;
            }

            initActions.Add(initAction);
        }

        public void AddDisposeAction(IDIBindingAction disposeAction)
        {
            if (binded)
            {
                return;
            }

            disposeActions.Add(disposeAction);
        }

        public void Bind(IDIResolveContainer container)
        {
            if(binded)
            {
                return;
            }

            binded = true;

            Value = OnBind(container);
        }

        public void Init(IDIResolveContainer container)
        {
            if (!binded)
            {
                return;
            }

            if (Value == null)
            {
                return;
            }

            foreach (IDIBindingAction initAction in initActions)
            {
                initAction.Execute(container, Value);
            }

            initActions.Clear();
        }

        public void Dispose(IDIResolveContainer container)
        {
            if (!binded)
            {
                return;
            }

            if (Value == null)
            {
                return;
            }

            foreach (IDIBindingAction disposeAction in disposeActions)
            {
                disposeAction.Execute(container, Value);
            }

            disposeActions.Clear();
        }

        protected abstract object OnBind(IDIResolveContainer container);
    }
}
