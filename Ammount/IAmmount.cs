namespace Juce.Core.Ammount
{
    public interface IAmmount<T>
    {
        bool IsInfinite { get; }
        T Value { get; }

        void Add(T ammount);

        void Substract(T ammount);

        IAmmount<T> DeepCopy();
    }
}