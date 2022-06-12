using Juce.Core.Di.Container;
using System;

namespace Juce.Core.Di.BindingActions
{
    public sealed class ActionWithoutContainerBindingAction : IDiBindingAction
    {
        private readonly Action<object> action;

        public ActionWithoutContainerBindingAction(Action<object> action)
        {
            this.action = action;
        }

        public void Execute(IDiResolveContainer resolver, object obj)
        {
            action?.Invoke(obj);
        }
    }
}
