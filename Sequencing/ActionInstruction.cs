using System;

namespace Juce.Core.Sequencing
{
    public class ActionInstruction : InstantInstruction
    {
        private readonly Action action;

        public ActionInstruction(Action action)
        {
            this.action = action;
        }

        protected sealed override void OnInstantExecute()
        {
            action?.Invoke();
        }
    }
}