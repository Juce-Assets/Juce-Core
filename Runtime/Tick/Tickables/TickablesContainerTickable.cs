using Juce.Core.Tick.Tickable;
using System.Collections.Generic;

namespace Juce.Core.Tick.Tickables
{
    public sealed class TickablesContainerTickable : ITickable
    {
        readonly List<ITickable> tickablesToAdd = new List<ITickable>();
        readonly List<ITickable> tickablesToRemove = new List<ITickable>();

        readonly List<ITickable> tickables = new List<ITickable>();

        public void Tick()
        {
            ActuallyRemoveTickables();

            TickTickables();

            ActuallyAddTickables();
        }

        public void Add(ITickable tickable)
        {
            if (tickable == null)
            {
                throw new System.ArgumentNullException($"Tried to add {nameof(ITickable)} but it was null at {nameof(TickablesContainerTickable)}");
            }

            bool contains = tickables.Contains(tickable);

            if (contains)
            {
                throw new System.Exception($"Tried to add {nameof(ITickable)} but it was already at {nameof(TickablesContainerTickable)}");
            }

            bool alreadyToAdd = tickablesToAdd.Contains(tickable);

            if(alreadyToAdd)
            {
                return;
            }

            tickablesToAdd.Add(tickable);
        }

        public void Remove(ITickable tickable)
        {
            if (tickable == null)
            {
                throw new System.ArgumentNullException($"Tried to remove {nameof(ITickable)} but it was null at {nameof(TickablesContainerTickable)}");
            }

            bool contained = tickables.Contains(tickable);

            if (!contained)
            {
                return;
            }

            bool alreadyToRemove = tickablesToRemove.Contains(tickable);

            if (alreadyToRemove)
            {
                return;
            }

            tickablesToRemove.Add(tickable);
        }

        public void Clear()
        {
            tickablesToRemove.AddRange(tickables);

            ActuallyRemoveTickables();
        }

        void ActuallyAddTickables()
        {
            foreach(ITickable tickable in tickablesToAdd)
            {
                tickables.Add(tickable);
            }

            tickablesToAdd.Clear();
        }

        public void ActuallyRemoveTickables()
        {
            foreach (ITickable tickable in tickablesToRemove)
            {
                tickables.Remove(tickable);
            }

            tickablesToRemove.Clear();
        }

        void TickTickables()
        {
            foreach (ITickable tickable in tickables)
            {
                tickable.Tick();
            }
        }
    }
}
