using System;

namespace Juce.Core.Refresh
{
    public class CallbackRefreshable : IRefreshable
    {
        private readonly Action onRefresh;

        public CallbackRefreshable(Action onRefresh)
        {
            this.onRefresh = onRefresh;
        }

        public void Refresh()
        {
            onRefresh?.Invoke();
        }
    }
}
