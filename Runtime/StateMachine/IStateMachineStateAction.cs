using System;

namespace Juce.Core.State
{
    public interface IStateMachineStateAction<T> where T : Enum
    {
        void OnEnter();
        void OnRun(IStateMachine<T> stateMachine);
        void OnExit();
    }
}
