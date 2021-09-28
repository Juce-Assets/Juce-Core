using Juce.Core.DI.BindingActions;
using Juce.Core.DI.Bindings;
using Juce.Core.DI.Container;
using System;

namespace Juce.Core.DI.Builder
{
    public class DIBindingActionBuilder<T> : IDIBindingActionBuilder<T>
    {
        private readonly DIBinding binding;

        public DIBindingActionBuilder(DIBinding binding)
        {
            this.binding = binding;
        }

        public void WhenInit(Action<IDIResolveContainer, T> action)
        {
            Action<IDIResolveContainer, object> castedAction = (IDIResolveContainer resolver, object obj) => action?.Invoke(
                resolver,
                (T)obj
                );

            IDIBindingAction bindingAction = new ActionWithContainerBindingAction(castedAction);

            binding.AddInitAction(bindingAction);
        }

        public void WhenInit(Func<T, Action> func)
        {
            Action<object> castedAction = (object obj) =>
            {
                Action returnedAction = func?.Invoke(
                  (T)obj
                  );

                returnedAction?.Invoke();
            };

            IDIBindingAction bindingAction = new ActionWithoutContainerBindingAction(castedAction);

            binding.AddInitAction(bindingAction);
        }

        public void WhenDispose(Action<T> action)
        {
            Action<object> castedAction = (object obj) => action?.Invoke(
                (T)obj
                );

            IDIBindingAction bindingAction = new ActionWithoutContainerBindingAction(castedAction);

            binding.AddDisposeAction(bindingAction);
        }
    }
}