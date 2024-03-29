﻿namespace Juce.Core.Refresh
{
    public sealed class CompositeRefreshable : IRefreshable
    {
        private readonly IRefreshable[] items;

        public CompositeRefreshable(IRefreshable[] items)
        {
            this.items = items;
        }

        public void Refresh()
        {
            foreach(IRefreshable item in items)
            {
                item.Refresh();
            }
        }
    }
}
