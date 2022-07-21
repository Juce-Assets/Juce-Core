using Juce.Core.Sequencing.Sequences;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Juce.Core.Sequencing.Timelines
{
    public class SequencerTimelines<T> : ISequencerTimelines<T> where T : Enum
    {
        private readonly Dictionary<T, ISequencer> timelines = new Dictionary<T, ISequencer>();

        public IReadOnlyList<ISequencer> Timelines => timelines.Values.ToArray();

        public ISequencer GetOrCreateTimeline(T identifier)
        {
            bool found = timelines.TryGetValue(identifier, out ISequencer sequencer);

            if (!found)
            {
                sequencer = new Sequencer();
                timelines.Add(identifier, sequencer);
            }

            return sequencer;
        }

        public void KillAll()
        {
            foreach(KeyValuePair<T, ISequencer> timeline in timelines)
            {
                timeline.Value.Kill();
            }
        }
    }
}