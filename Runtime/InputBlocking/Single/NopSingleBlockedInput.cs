using System;

namespace Juce.Core.InputBlocking
{
    public sealed class NopSingleBlockedInput : ISingleBlockedInput
    {
        public static readonly NopSingleBlockedInput Instance = new NopSingleBlockedInput();

        public event Action<bool> OnBlockedStateChanged;

        private NopSingleBlockedInput()
        {

        }

        public bool IsBlocked()
        {
            return false;
        }

        public void SetBlocked(object owner,  bool blocked)
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
