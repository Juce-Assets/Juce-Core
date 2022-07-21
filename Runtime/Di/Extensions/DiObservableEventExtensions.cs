using System;
using Juce.Core.Di.Builder;
using Juce.Core.Di.Container;
using Juce.Core.Observables.Events;

namespace Juce.Core.Di.Extensions
{
    public static class DiObservableEventExtensions
    {
        public static IDiBindingActionBuilder<T> LinkObservableEvent<T, TEventData>(
            this IDiBindingActionBuilder<T> actionBuilder,
            Func<IDiResolveContainer, IObservableEvent<TEventData>> getObservable,
            Func<T, Action<TEventData>> func
        )
        {
            IObservableEvent<TEventData> observableEvent = null;
            Action<TEventData> action = null;

            actionBuilder.WhenInit((c, o) =>
            {
                observableEvent = getObservable.Invoke(c);
                action = func.Invoke(o);

                observableEvent.OnExecute += action;
            });

            actionBuilder.WhenDispose((c, o) =>
            {
                observableEvent.OnExecute -= action;
            });

            actionBuilder.NonLazy();

            return actionBuilder;
        }
    }
}
