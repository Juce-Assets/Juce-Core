namespace Juce.Core.Ammount
{
    public class InfiniteUIntAmmount : IAmmount<int>
    {
        public bool IsInfinite { get => true; }
        public int Value { get => 1; }

        public void Add(int ammount)
        {
        }

        public void Substract(int ammount)
        {
        }

        public IAmmount<int> DeepCopy()
        {
            return new InfiniteUIntAmmount();
        }
    }
}