using System;

namespace Juce.Core.Loading.Process
{
    public interface ILoadingProcess
    {
        event Action OnCompleted;

        bool HasParent { get; }

        ILoadingProcess NewChild();
        void Complete();
    }
}
