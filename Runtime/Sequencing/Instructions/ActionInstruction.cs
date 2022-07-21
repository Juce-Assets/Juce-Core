using System;

namespace Juce.Core.Sequencing.Instructions
{
    public sealed class ActionInstruction : InstantInstruction
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