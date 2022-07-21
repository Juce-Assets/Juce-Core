using System;

namespace Juce.Core.InputBlocking
{
    public interface IEnumBlockedInput<T> : IBlockedInput where  T : Enum
    {
        event Action<T, bool> OnBlockedStateChanged;

        bool IsBlocked(T type);
        void SetBlocked(object owner, T type, bool blocked);
    }
}
