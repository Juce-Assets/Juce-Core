using System;

namespace Juce.Core.Events.Pipe
{
    public interface IEventDispatcher
    {
        void Dispatch<T>(T ev) where T : class;
    }
}