using System;

namespace Juce.Core.Tick.Tickable
{
    public class CallbackTickable : ITickable
    {
        private readonly Action callback;

        public CallbackTickable(
            Action callback
            )
        {
            this.callback = callback;
        }

        public void Tick()
        {
            callback.Invoke();
        }
    }
}