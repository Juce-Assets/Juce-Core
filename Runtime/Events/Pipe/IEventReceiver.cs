﻿using System;

namespace Juce.Core.Events.Pipe
{
    public interface IEventReceiver
    {
        int EventQueueCount { get; }

        IEventReference Subscribe<T>(Action<T> action) where T : class;
        void Unsubscribe(IEventReference eventReference);
        void DequeNext();
    }
}