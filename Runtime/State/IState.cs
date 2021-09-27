using Juce.Core.Events.Generic;

namespace Juce.Core.State
{
    public interface IState<T>
    {
        T Value { get; set; }

        event GenericEvent<IState<T>, StateValueChangedEvent<T>> OnValueChanged;

        void Refresh();
    }
}
