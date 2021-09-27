using System;

namespace Juce.Core.Observables
{
    public class ObservableCommand
    {
        public event Action OnExecute;

        public void Execute()
        {
            OnExecute?.Invoke();
        }
    }

    public class ObservableCommand<T>
    {
        public event Action<T> OnExecute;

        public void Execute(T value)
        {
            OnExecute?.Invoke(value);
        }
    }
}
