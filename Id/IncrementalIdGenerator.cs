namespace Juce.Core.Id
{
    public class IncrementalIdGenerator : IIdGenerator
    {
        private int currValue = int.MinValue;

        public int Generate()
        {
            return ++currValue;
        }
    }
}