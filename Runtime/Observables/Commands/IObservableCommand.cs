using System;

namespace Juce.Core.Observables.Commands
{
    public interface IObservableCommand
    {
        event Action OnExecute;

        void Execute();
    }

    public interface IObservableCommand<T>
    {
        event Action<T> OnExecute;

        void Execute(T value);
    }
}
