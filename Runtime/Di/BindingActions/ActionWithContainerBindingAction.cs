using Juce.Core.Di.Container;
using System;

namespace Juce.Core.Di.BindingActions
{
    public sealed class ActionWithContainerBindingAction : IDiBindingAction
    {
        private readonly Action<IDiResolveContainer, object> action;

        public ActionWithContainerBindingAction(Action<IDiResolveContainer, object> action)
        {
            this.action = action;
        }

        public void Execute(IDiResolveContainer resolver, object obj)
        {
            action?.Invoke(resolver, obj);
        }
    }
}
