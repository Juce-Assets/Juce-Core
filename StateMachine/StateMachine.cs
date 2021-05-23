using System;
using System.Collections.Generic;
using System.Linq;

namespace Juce.Core.State
{
    public class StateMachine<T> : IStateMachine<T> where T : Enum
    {
        private readonly Dictionary<T, StateMachineState<T>> states = new Dictionary<T, StateMachineState<T>>();

        private bool idle;

        private StateMachineState<T> nextState;

        public StateMachineState<T> CurrentState { get; private set; }

        public void RegisterState(T stateId, IStateMachineStateAction<T> stateAction)
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
            idle = true;

            SetNextState(state);
        }

        public void SetNextState(T state)
        {
            bool found = states.TryGetValue(state, out StateMachineState<T> stateData);

            if (!found)
            {
                throw new Exception();
            }

            nextState = stateData;

            if(!idle)
            {
                return;
            }

            Next();
        }

        private void Next()
        {
            if (!idle)
            {
                throw new Exception("Not idle");
            }

            idle = false;

            bool tryNext = true;

            while(tryNext)
            {
                tryNext = TryNext();
            }

            idle = true;
        }

        private bool TryNext()
        {
            if(nextState == null)
            {
                return false;
            }

            if (CurrentState != null)
            {
                bool foundConnection = CurrentState.Connections.Contains(nextState.StateId);

                if (!foundConnection)
                {
                    throw new Exception();
                }

                bool found = states.TryGetValue(nextState.StateId, out StateMachineState<T> stateData);

                if (!found)
                {
                    throw new Exception();
                }

                CurrentState.StateAction.OnExit();
            }

            CurrentState = nextState;

            nextState = null;

            CurrentState.StateAction?.OnEnter();

            CurrentState.StateAction.OnRun(this);

            return true;
        }
    }
}
