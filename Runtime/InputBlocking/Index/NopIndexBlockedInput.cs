using System;

namespace Juce.Core.InputBlocking
{
    public sealed class NopIndexBlockedInput : IIndexBlockedInput
    {
        public static readonly NopIndexBlockedInput Instance = new NopIndexBlockedInput();

        public event Action<int, bool> OnBlockedStateChanged;

        private NopIndexBlockedInput()
        {

        }

        public bool IsBlocked(int index)
        {
            return false;
        }

        public void SetBlocked(object owner, int index, bool blocked)
        {

        }

        public void SetBlocked(int index, bool blocked)
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
