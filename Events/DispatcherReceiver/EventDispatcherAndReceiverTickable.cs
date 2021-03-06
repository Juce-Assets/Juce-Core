﻿using Juce.Core.Tickable;

namespace Juce.Core.Events
{
    public class EventDispatcherAndReceiverTickable : ITickable
    {
        private readonly EventDispatcherAndReceiver eventDispatcherAndReceiver;

        public EventDispatcherAndReceiverTickable(EventDispatcherAndReceiver eventDispatcherAndReceiver)
        {
            this.eventDispatcherAndReceiver = eventDispatcherAndReceiver;
        }

        public void Tick()
        {
            eventDispatcherAndReceiver.DequeNext();
        }
    }
}