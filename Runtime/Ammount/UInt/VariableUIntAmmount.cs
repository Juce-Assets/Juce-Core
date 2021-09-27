using System;

namespace Juce.Core.Ammount
{
    public class VariableUIntAmmount : IAmmount<int>
    {
        private int value;

        public bool IsInfinite { get => false; }
        public int Value { get => value; }

        public VariableUIntAmmount(int value)
        {
            this.value = System.Math.Max(value, 0);
        }

        public void Add(int ammount)
        {
            this.value = System.Math.Max(value + ammount, 0);
        }

        public void Substract(int ammount)
        {
            this.value = System.Math.Max(value - ammount, 0);
        }

        public IAmmount<int> DeepCopy()
        {
            return new VariableUIntAmmount(value);
        }
    }
}