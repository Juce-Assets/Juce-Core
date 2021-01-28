using System;
using System.Collections.Generic;

namespace Juce.Core.State
{
    public class StateMachineState<T> where T : Enum
    {
        private readonly List<T> connections = new List<T>();

        public T StateId { get; }
        public Action StateAction { get; }
        public IReadOnlyList<T> Connections => connections;

        public StateMachineState(T stateId, Action stateAction)
        {
            StateId = stateId;
            StateAction = stateAction;
        }

        public void AddConnection(T connection)
        {
            bool alreadyAdded = connections.Contains(connection);

            if(alreadyAdded)
            {
                throw new Exception();
            }

            connections.Add(connection);
        }
    }
}
