using System;

namespace Juce.Core.InputBlocking
{
    public interface ISingleBlockedInput : IBlockedInput
    {
        event Action<bool> OnBlockedStateChanged;

        bool IsBlocked();
        void SetBlocked(object owner, bool blocked);
    }
}
