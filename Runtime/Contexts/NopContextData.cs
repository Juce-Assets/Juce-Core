namespace Juce.Core.Contexts
{
    public sealed class NopContextData : IContextData
    {
        public static readonly NopContextData Instance = new NopContextData();

        private NopContextData()
        {

        }
    }
}
