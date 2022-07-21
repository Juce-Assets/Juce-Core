using System;

namespace Juce.Core.Observables.Events
{
    public interface IObservableEvent<TSender, TEventArgs>
    {
        event Action<TSender, TEventArgs> OnExecute;

        void Execute(TSender sender, TEventArgs eventArgs);
        void Clear();
    }

    public interface IObservableEvent<TEventArgs>
    {
        event Action<TEventArgs> OnExecute;

        void Execute(TEventArgs eventArgs);
        void Clear();
    }
}
