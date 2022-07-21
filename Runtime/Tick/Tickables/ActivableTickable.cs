namespace Juce.Core.Tick.Tickable
{
    public abstract class ActivableTickable : ITickable
    {
        private bool started;

        public bool Active { get; set; }

        public ActivableTickable(bool active)
        {
            Active = active;
        }

        public void Tick()
        {
            if(!Active)
            {
                return;
            }

            if(!started)
            {
                started = true;

                ActivableStart();
            }

            ActivableTick();
        }

        protected virtual void ActivableStart() { }
        protected abstract void ActivableTick();
    }
}