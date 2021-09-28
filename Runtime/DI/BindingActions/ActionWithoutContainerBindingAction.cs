using Juce.Core.DI.Container;
using System;

namespace Juce.Core.DI.BindingActions
{
    public class ActionWithoutContainerBindingAction : IDIBindingAction
    {
        private readonly Action<object> action;

        public ActionWithoutContainerBindingAction(Action<object> action)
        {
            this.action = action;
        }

        public void Execute(IDIResolveContainer resolver, object obj)
        {
            action?.Invoke(obj);
        }
    }
}
