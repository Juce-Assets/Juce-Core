using System;
using System.Collections.Generic;

namespace Juce.Core.InputBlocking
{
    public sealed class IndexBlockedInput : IIndexBlockedInput
    {
        private readonly Dictionary<object, List<int>> blockedInputs = new Dictionary<object, List<int>>();

        readonly int maxIndex;

        public event Action<int, bool> OnBlockedStateChanged;

        public IndexBlockedInput(int maxIndex)
        {
            this.maxIndex = maxIndex;
        }

        public bool IsBlocked(int index)
        {
            foreach (List<int> blockedInput in blockedInputs.Values)
            {
                bool isBlocked = blockedInput.Contains(index);

                if (!isBlocked)
                {
                    continue;
                }

                return true;
            }

            return false;
        }

        public void SetBlocked(object owner, int index, bool blocked)
        {
            bool wasBlocked = IsBlocked(index);

            bool ownerFound = blockedInputs.TryGetValue(owner, out List<int> ownerBlockedInputs);

            if (!ownerFound)
            {
                ownerBlockedInputs = new List<int>();
                blockedInputs.Add(owner, ownerBlockedInputs);
            }

            if (blocked)
            {
                bool alreadyBlocked = ownerBlockedInputs.Contains(index);

                if (!alreadyBlocked)
                {
                    ownerBlockedInputs.Add(index);
                }
            }
            else
            {
                ownerBlockedInputs.Remove(index);
            }

            bool isBlocked = IsBlocked(index);

            if (wasBlocked == isBlocked)
            {
                return;
            }

            OnBlockedStateChanged?.Invoke(index, isBlocked);
        }

        public void BlockAll(object owner)
        {
            for (int i = 0; i <= maxIndex; ++i)
            {
                SetBlocked(owner, i, true);
            }
        }

        public void UnblockAll(object owner)
        {
            for (int i = 0; i <= maxIndex; ++i)
            {
                SetBlocked(owner, i, false);
            }
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
