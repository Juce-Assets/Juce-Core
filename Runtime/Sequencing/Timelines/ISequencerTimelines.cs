using Juce.Core.Sequencing.Sequences;
using System;
using System.Collections.Generic;

namespace Juce.Core.Sequencing.Timelines
{
    public interface ISequencerTimelines<T> where T : Enum
    {
        public IReadOnlyList<ISequencer> Timelines { get; }

        ISequencer GetOrCreateTimeline(T identifier);
        void KillAll();
    }
}