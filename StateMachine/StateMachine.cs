using System;
using System.Collections.Generic;
using System.Linq;

namespace Juce.Core.State
{
    public class StateMachine<T> where T : Enum
    {
        private readonly Dictionary<T, StateMachineState<T>> states = new Dictionary<T, StateMachineState<T>>();

        private bool stateChangeLocked;

        public StateMachineState<T> CurrentState { get; private set; }

        public void RegisterState(T stateId, IStateMachineStateAction stateAction)
        {
            states.Add(stateId, new StateMachineState<T>(stateId, stateAction));
        }

        public void RegisterConnection(T from, T to)
        {
            bool foundFromState = states.TryGetValue(from, out StateMachineState<T> fromState);

            if(!foundFromState)
            {
                throw new Exception();
            }

            bool foundToState = states.TryGetValue(to, out StateMachineState<T> toState);

            if (!foundToState)
            {
                throw new Exception();
            }

            fromState.AddConnection(to);
        }

        public void Start(T state)
        {
            bool found = states.TryGetValue(state, out StateMachineState<T> stateData);

            if (!found)
            {
                throw new Exception();
            }

            CurrentState = stateData;

            stateChangeLocked = true;

            stateData.StateAction?.OnEnter();

            stateChangeLocked = false;

            stateData.StateAction?.OnRun();
        }

        public void Next(T state)
        {
            if(CurrentState == null)
            {
                throw new Exception();
            }

            if(stateChangeLocked)
            {
                throw new InvalidOperationException($"State change is locked during OnEnter and OnExit functions of a state");
            }

            bool foundConnection = CurrentState.Connections.Contains(state);

            if(!foundConnection)
            {
                throw new Exception();
            }

            bool found = states.TryGetValue(state, out StateMachineState<T> stateData);

            if (!found)
            {
                throw new Exception();
            }

            StateMachineState<T> lastState = CurrentState;

            CurrentState = stateData;

            stateChangeLocked = true;

            lastState.StateAction.OnExit();

            CurrentState.StateAction?.OnEnter();

            stateChangeLocked = false;

            CurrentState.StateAction.OnRun();
        }
    }
}
