using System;

namespace Juce.Core.Ammount
{
    public class ConstantUIntAmmount : IAmmount<int>
    {
        private readonly int value;

        public bool IsInfinite { get => false; }
        public int Value { get => value; }

        public ConstantUIntAmmount(int value)
        {
            this.value = System.Math.Max(value, 0);
        }

        public void Add(int ammount)
        {
        }

        public void Substract(int ammount)
        {
        }

        public IAmmount<int> DeepCopy()
        {
            return new ConstantUIntAmmount(value);
        }
    }
}