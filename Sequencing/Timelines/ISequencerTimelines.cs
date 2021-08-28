using System;
using System.Collections.Generic;
using System.Linq;

namespace Juce.Core.Sequencing
{
    public interface ISequencerTimelines<T> where T : Enum
    {
        public IReadOnlyList<ISequencer> Timelines { get; }

        ISequencer GetOrCreateTimeline(T identifier);
    }
}