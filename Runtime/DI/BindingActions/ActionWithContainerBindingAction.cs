using Juce.Core.Di.Container;
using System;

namespace Juce.Core.Di.BindingActions
{
    public class ActionWithContainerBindingAction : IDiBindingActionA
    {
        private readonly Action<IDiResolveContainerA, object> action;

        public ActionWithContainerBindingAction(Action<IDiResolveContainerA, object> action)
        {
            this.action = action;
        }

        public void Execute(IDiResolveContainerA resolver, object obj)
        {
            action?.Invoke(resolver, obj);
        }
    }
}
