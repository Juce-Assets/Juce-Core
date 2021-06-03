using System;

namespace Juce.Core.CleanUp
{
    public interface ICleanUpActionsRepository
    {
        public void Add(Action action);
    }
}
