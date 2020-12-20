using System;
using System.Collections.Generic;

namespace Juce.Core.EntryPoint
{
    public abstract class EntryPoint<T>
    {
        private bool executed;

        private readonly List<Action> cleanUpActions = new List<Action>();

        public event Action<T> OnFinish;

        public void Execute()
        {
            if (executed)
            {
                return;
            }

            executed = true;

            cleanUpActions.Clear();

            OnExecute();
        }

        public void Finish(T finishRestult)
        {
            if (!executed)
            {
                return;
            }

            OnFinish?.Invoke(finishRestult);
        }

        public void CleanUp()
        {
            for (int i = 0; i < cleanUpActions.Count; ++i)
            {
                cleanUpActions[i].Invoke();
            }
        }

        public void AddCleanUpAction(Action action)
        {
            if (action == null)
            {
                throw new ArgumentNullException($"Null CleanUp action at {nameof(EntryPoint)}");
            }

            cleanUpActions.Add(action);
        }

        protected abstract void OnExecute();
    }
}