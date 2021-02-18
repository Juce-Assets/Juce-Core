using System;

namespace Juce.Core.Events
{
    public interface IEventDispatcher
    {
        void Dispatch<T>(T ev) where T : class;
    }
}