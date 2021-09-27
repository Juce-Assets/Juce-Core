using System;
using System.Collections.Generic;
using System.Linq;

namespace Juce.Core.Sequencing
{
    public class SequencerTimelines<T> : ISequencerTimelines<T> where T : Enum
    {
        private readonly Dictionary<T, Sequencer> timelines = new Dictionary<T, Sequencer>();

        public IReadOnlyList<ISequencer> Timelines => timelines.Values.ToArray();

        public ISequencer GetOrCreateTimeline(T identifier)
        {
            bool found = timelines.TryGetValue(identifier, out Sequencer sequencer);

            if (!found)
            {
                sequencer = new Sequencer();
                timelines.Add(identifier, sequencer);
            }

            return sequencer;
        }

        public void KillAll()
        {
            foreach(KeyValuePair<T, Sequencer> timeline in timelines)
            {
                timeline.Value.Kill();
            }
        }
    }
}