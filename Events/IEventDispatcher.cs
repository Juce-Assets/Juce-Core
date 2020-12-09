using System;

namespace Juce.Core.Events
{
    public interface IEventDispatcher
    {
        IEventReference Subscribe<T>(Action<T> action) where T : class;

        void Unsubscribe(IEventReference eventReference);

        void Dispatch<T>(T ev) where T : class;
    }
}