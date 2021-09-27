using System;

namespace Juce.Core.CleanUp
{
    public interface ICleanUpActionsRepository
    {
        void Add(Action action);
        void CleanUp();
    }
}
