using System;

namespace Juce.Core.Activables
{
    public class CallbackActivable : IActivable
    {
        private readonly Func<bool> getActive;
        private readonly Action<bool> setActive;

        public bool Active => getActive != null ? getActive.Invoke() : false;

        public CallbackActivable(Func<bool> getActive, Action<bool> setActive)
        {
            this.getActive = getActive;
            this.setActive = setActive;
        }

        public void SetActive(bool active)
        {
            setActive?.Invoke(active);
        }
    }
}