using Juce.Core.Di.Container;
using System;

namespace Juce.Core.Di.BindingActions
{
    public class ActionWithoutContainerBindingAction : IDiBindingActionA
    {
        private readonly Action<object> action;

        public ActionWithoutContainerBindingAction(Action<object> action)
        {
            this.action = action;
        }

        public void Execute(IDiResolveContainerA resolver, object obj)
        {
            action?.Invoke(obj);
        }
    }
}
