using System;

namespace Juce.Core.Observables.Commands
{
    public sealed class ObservableCommand : IObservableCommand
    {
        public event Action OnExecute;

        public void Execute()
        {
            OnExecute?.Invoke();
        }
    }

    public class ObservableCommand<T> : IObservableCommand<T>
    {
        public event Action<T> OnExecute;

        public void Execute(T value)
        {
            OnExecute?.Invoke(value);
        }
    }
}
