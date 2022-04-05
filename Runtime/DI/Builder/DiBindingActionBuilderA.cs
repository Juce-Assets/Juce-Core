using Juce.Core.Di.BindingActions;
using Juce.Core.Di.Bindings;
using Juce.Core.Di.Container;
using System;

namespace Juce.Core.Di.Builder
{
    public class DiBindingActionBuilderA<T> : IDiBindingActionBuilderA<T>
    {
        private readonly DiBindingA binding;

        public DiBindingActionBuilderA(DiBindingA binding)
        {
            this.binding = binding;
        }

        public Type IdentifierType => binding.IdentifierType;
        public Type ActualType => binding.ActualType;

        public IDiBindingActionBuilderA<T> NonLazy()
        {
            binding.NonLazy();

            return this;
        }

        public IDiBindingActionBuilderA<T> WhenInit(Action<IDiResolveContainerA, T> action)
        {
            Action<IDiResolveContainerA, object> castedAction = (IDiResolveContainerA resolver, object obj) => action?.Invoke(
                resolver,
                (T)obj
                );

            IDiBindingActionA bindingAction = new ActionWithContainerBindingAction(castedAction);

            binding.AddInitAction(bindingAction);

            return this;
        }

        public IDiBindingActionBuilderA<T> WhenInit(Func<T, Action> func)
        {
            Action<object> castedAction = (object obj) =>
            {
                Action returnedAction = func?.Invoke(
                  (T)obj
                  );

                returnedAction?.Invoke();
            };

            IDiBindingActionA bindingAction = new ActionWithoutContainerBindingAction(castedAction);

            binding.AddInitAction(bindingAction);

            return this;
        }

        public IDiBindingActionBuilderA<T> WhenDispose(Action<IDiResolveContainerA, T> action)
        {
            Action<IDiResolveContainerA, object> castedAction = (IDiResolveContainerA resolver, object obj) => action?.Invoke(
              resolver,
              (T)obj
              );

            IDiBindingActionA bindingAction = new ActionWithContainerBindingAction(castedAction);

            binding.AddDisposeAction(bindingAction);

            return this;
        }

        public IDiBindingActionBuilderA<T> WhenDispose(Action<T> action)
        {
            Action<object> castedAction = (object obj) => action?.Invoke(
                (T)obj
                );

            IDiBindingActionA bindingAction = new ActionWithoutContainerBindingAction(castedAction);

            binding.AddDisposeAction(bindingAction);

            return this;
        }
    }
}