using System;
using System.Collections.Generic;

namespace Juce.Core.CleanUp
{
    public class CleanUpActionsRepository : ICleanUpActionsRepository
    {
        private readonly List<Action> cleanUpActionsRepository = new List<Action>();

        public void Add(Action action)
        {
            cleanUpActionsRepository.Add(action);
        }

        public void CleanUp()
        {
            foreach(Action action in cleanUpActionsRepository)
            {
                action?.Invoke();
            }

            cleanUpActionsRepository.Clear();
        }
    }
}
