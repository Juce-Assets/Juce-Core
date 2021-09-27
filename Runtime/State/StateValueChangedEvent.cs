namespace Juce.Core.State
{
    public class StateValueChangedEvent<T> 
    {
        public T OldValue { get; }
        public T NewValue { get; }

        public StateValueChangedEvent(
            T oldValue,
            T newValue
            )
        {
            OldValue = oldValue;
            NewValue = newValue;
        }
    }
}
