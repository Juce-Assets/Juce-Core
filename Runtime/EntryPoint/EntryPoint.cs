using System;
using System.Collections.Generic;

namespace Juce.Core.EntryPoint
{
    public abstract class EntryPoint<T>
    {
        private bool started;

        private readonly List<Action> cleanUpActions = new List<Action>();

        public event Action<T> OnFinish;

        public void Start()
        {
            if (started)
            {
                return;
            }

            started = true;

            cleanUpActions.Clear();

            OnStart();
        }

        public void Finish(T finishRestult)
        {
            if (!started)
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

        protected abstract void OnStart();
    }
}