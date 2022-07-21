using System;
using System.Collections.Generic;

namespace Juce.Core.InputBlocking
{
    public sealed class SingleBlockedInput : ISingleBlockedInput
    {
        private readonly List<object> blockedInputs = new List<object>();

        public event Action<bool> OnBlockedStateChanged;

        public bool IsBlocked()
        {
            return blockedInputs.Count > 0;
        }

        public void SetBlocked(object owner,  bool blocked)
        {
            bool wasBlocked = IsBlocked();

            if (blocked)
            {
                bool alreadyBlocked = blockedInputs.Contains(owner);

                if (!alreadyBlocked)
                {
                    blockedInputs.Add(owner);
                }
            }
            else
            {
                blockedInputs.Remove(owner);
            }

            bool isBlocked = IsBlocked();

            if (wasBlocked == isBlocked)
            {
                return;
            }

            OnBlockedStateChanged?.Invoke(isBlocked);
        }

        public void BlockAll(object owner)
        {
            SetBlocked(owner, true);
        }

        public void UnblockAll(object owner)
        {
            SetBlocked(owner, false);
        }

        public void BlockAll()
        {
            BlockAll(this);
        }

        public void UnblockAll()
        {
            UnblockAll(this);
        }
    }
}
