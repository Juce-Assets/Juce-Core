using System;

namespace Juce.Core.InputBlocking
{
    public sealed class NopEnumBlockedInput<T> : IEnumBlockedInput<T> where T : Enum
    {
        public static readonly NopEnumBlockedInput<T> Instance = new NopEnumBlockedInput<T>();

        public event Action<T, bool> OnBlockedStateChanged;

        private NopEnumBlockedInput()
        {

        }

        public bool IsBlocked(T type)
        {
            return false;
        }

        public void SetBlocked(object owner, T type, bool blocked)
        {
            throw new NotImplementedException();
        }

        public void SetBlocked(T type, bool blocked)
        {

        }

        public void BlockAll(object owner)
        {

        }

        public void UnblockAll(object owner)
        {

        }

        public void BlockAll()
        {

        }

        public void UnblockAll()
        {

        }
    }
}
