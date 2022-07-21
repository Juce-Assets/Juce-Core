using System;
using System.Collections.Generic;
using Juce.Core.Enums.Utils;

namespace Juce.Core.InputBlocking
{
    public sealed class EnumBlockedInput<T> : IEnumBlockedInput<T> where T : Enum
    {
        private readonly Dictionary<object, List<T>> blockedInputs = new Dictionary<object, List<T>>();

        public event Action<T, bool> OnBlockedStateChanged;

        public bool IsBlocked(T type)
        {
            foreach (List<T> blockedInput in blockedInputs.Values)
            {
                bool isBlocked = blockedInput.Contains(type);

                if (!isBlocked)
                {
                    continue;
                }

                return true;
            }

            return false;
        }

        public void SetBlocked(object owner, T type, bool blocked)
        {
            bool wasBlocked = IsBlocked(type);

            bool ownerFound = blockedInputs.TryGetValue(owner, out List<T> ownerBlockedInputs);

            if (!ownerFound)
            {
                ownerBlockedInputs = new List<T>();
                blockedInputs.Add(owner, ownerBlockedInputs);
            }

            if (blocked)
            {
                bool alreadyBlocked = ownerBlockedInputs.Contains(type);

                if (!alreadyBlocked)
                {
                    ownerBlockedInputs.Add(type);
                }
            }
            else
            {
                ownerBlockedInputs.Remove(type);
            }

            bool isBlocked = IsBlocked(type);

            if (wasBlocked == isBlocked)
            {
                return;
            }

            OnBlockedStateChanged?.Invoke(type, isBlocked);
        }

        public void BlockAll(object owner)
        {
            T[] allValues = EnumsUtils.GetValues<T>();

            foreach (T value in allValues)
            {
                SetBlocked(owner, value, true);
            }
        }

        public void UnblockAll(object owner)
        {
            T[] allValues = EnumsUtils.GetValues<T>();

            foreach (T value in allValues)
            {
                SetBlocked(owner, value, false);
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
