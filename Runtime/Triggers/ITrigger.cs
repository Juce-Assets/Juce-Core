using System;

namespace Juce.Core.Triggers
{
    public interface ITrigger
    {
        event Action OnTrigger;
    }
}
