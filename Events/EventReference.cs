using System;

namespace Juce.Core.Events
{
    internal class EventReference : IEventReference
    {
        public Type Type { get; }
        public Action<object> Action { get; }

        public EventReference(Type type, Action<object> action)
        {
            Type = type;
            Action = action;
        }
    }
}