using Juce.Core.Di.BindingActions;
using Juce.Core.Di.Bindings;
using Juce.Core.Di.Container;
using System;

namespace Juce.Core.Di.Builder
{
    public class DiBindingActionBuilder<T> : IDiBindingActionBuilder<T>
    {
        private readonly DiBinding binding;

        public DiBindingActionBuilder(DiBinding binding)
        {
            this.binding = binding;
        }

        public Type IdentifierType => binding.IdentifierType;
        public Type ActualType => binding.ActualType;

        public IDiBindingActionBuilder<T> NonLazy()
        {
            binding.NonLazy();

            return this;
        }

        public IDiBindingActionBuilder<T> WhenInit(Action<IDiResolveContainer, T> action)
        {
            Action<IDiResolveContainer, object> castedAction = (IDiResolveContainer resolver, object obj) => action?.Invoke(
                resolver,
                (T)obj
                );

            IDiBindingAction bindingAction = new ActionWithContainerBindingAction(castedAction);

            binding.AddInitAction(bindingAction);

            return this;
        }

        public IDiBindingActionBuilder<T> WhenInit(Func<T, Action> func)
        {
            Action<object> castedAction = (object obj) =>
            {
                Action returnedAction = func?.Invoke(
                  (T)obj
                  );

                returnedAction?.Invoke();
            };

            IDiBindingAction bindingAction = new ActionWithoutContainerBindingAction(castedAction);

            binding.AddInitAction(bindingAction);

            return this;
        }

        public IDiBindingActionBuilder<T> WhenDispose(Action<IDiResolveContainer, T> action)
        {
            Action<IDiResolveContainer, object> castedAction = (IDiResolveContainer resolver, object obj) => action?.Invoke(
              resolver,
              (T)obj
              );

            IDiBindingAction bindingAction = new ActionWithContainerBindingAction(castedAction);

            binding.AddDisposeAction(bindingAction);

            return this;
        }

        public IDiBindingActionBuilder<T> WhenDispose(Action<T> action)
        {
            Action<object> castedAction = (object obj) => action?.Invoke(
                (T)obj
                );

            IDiBindingAction bindingAction = new ActionWithoutContainerBindingAction(castedAction);

            binding.AddDisposeAction(bindingAction);

            return this;
        }
    }
}