namespace Juce.Core.Refresh
{
    public sealed class NopRefreshable : IRefreshable
    {
        public static readonly NopRefreshable Instance = new NopRefreshable();

        private NopRefreshable()
        {

        }

        public void Refresh()
        {

        }
    }
}
