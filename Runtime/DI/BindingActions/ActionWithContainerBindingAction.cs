using Juce.Core.DI.Container;
using System;

namespace Juce.Core.DI.BindingActions
{
    public class ActionWithContainerBindingAction : IDIBindingAction
    {
        private readonly Action<IDIResolveContainer, object> action;

        public ActionWithContainerBindingAction(Action<IDIResolveContainer, object> action)
        {
            this.action = action;
        }

        public void Execute(IDIResolveContainer resolver, object obj)
        {
            action?.Invoke(resolver, obj);
        }
    }
}
