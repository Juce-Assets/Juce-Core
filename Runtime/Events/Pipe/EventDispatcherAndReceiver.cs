using System;
using System.Collections.Generic;

namespace Juce.Core.Events.Pipe
{
    public class EventDispatcherAndReceiver : IEventDispatcherAndReceiver
    {
        private readonly Queue<KeyValuePair<Type, object>> eventQueue = new Queue<KeyValuePair<Type, object>>();

        private readonly Dictionary<Type, List<EventReference>> eventHandlers = new Dictionary<Type, List<EventReference>>();

        public int EventQueueCount => eventQueue.Count;

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

            if(eventReference == null)
            {
                return;
            }

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

            eventQueue.Enqueue(new KeyValuePair<Type, object>(type, ev));
        }

        public void DequeNext()
        {
            if(eventQueue.Count == 0)
            {
                return;
            }

            KeyValuePair<Type, object> currEvent = eventQueue.Dequeue();

            bool found = eventHandlers.TryGetValue(currEvent.Key, out List<EventReference> eventReferenceList);

            if (!found)
            {
                return;
            }

            List<EventReference> toInvoke = new List<EventReference>(eventReferenceList);

            foreach (EventReference eventReference in toInvoke)
            {
                eventReference.Action?.Invoke(currEvent.Value);
            }
        }
    }
}