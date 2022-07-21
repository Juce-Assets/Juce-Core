using System;

namespace Juce.Core.InputBlocking
{
    public interface IIndexBlockedInput : IBlockedInput
    {
        event Action<int, bool> OnBlockedStateChanged;

        bool IsBlocked(int index);
        void SetBlocked(object owner, int index, bool blocked);
    }
}
