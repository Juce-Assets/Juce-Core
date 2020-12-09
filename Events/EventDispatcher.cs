using System;
using System.Collections.Generic;

namespace Juce.Core.Events
{
    public class EventDispatcher : IEventDispatcher
    {
        private readonly Dictionary<Type, List<EventReference>> eventHandlers = new Dictionary<Type, List<EventReference>>();

        private readonly List<object> eventStack = new List<object>();

        public IEventReference Subscribe<T>(Action<T> action) where T : class
        {
            Type type = typeof(T);

            bool found = eventHandlers.TryGetValue(type, out List<EventReference> eventReferenceList);

            if (!found)
            {
                eventReferenceList = new List<EventReference>();
                eventHandlers.Add(type, eventReferenceList);
            }

            EventReference eventReference = new EventReference(type, (object obj) => action.Invoke(obj as T));

            eventReferenceList.Add(eventReference);

            return eventReference;
        }

        public void Unsubscribe(IEventReference eventReference)
        {
            EventReference typedEventReference = eventReference as EventReference;

            bool found = eventHandlers.TryGetValue(typedEventReference.Type, out List<EventReference> eventReferenceList);

            if (!found)
            {
                return;
            }

            eventReferenceList.Remove(typedEventReference);
        }

        public void Dispatch<T>(T ev) where T : class
        {
            Type type = typeof(T);

            bool found = eventHandlers.TryGetValue(type, out List<EventReference> eventReferenceList);

            if (!found)
            {
                return;
            }

            List<EventReference> toInvoke = new List<EventReference>(eventReferenceList);

            foreach (EventReference eventReference in toInvoke)
            {
                eventReference.Action?.Invoke(ev);
            }

            eventStack.Add(ev);
        }

        public bool TryPopEventStack(out List<object> outEventStack)
        {
            if (eventStack.Count == 0)
            {
                outEventStack = null;
                return false;
            }

            outEventStack = new List<object>(eventStack);

            eventStack.Clear();

            return true;
        }
    }
}