using System;

namespace Juce.Core.State
{
    public interface IStateMachine<T> where T : Enum
    {
        void SetNextState(T state);
    }
}
