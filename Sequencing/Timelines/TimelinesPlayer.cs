using System.Collections.Generic;

namespace Juce.Core.Sequencing
{
    public class TimelinesPlayer
    {
        private readonly List<InstructionsPlayer> timelines = new List<InstructionsPlayer>();

        public void Update()
        {
            foreach (InstructionsPlayer timeline in timelines)
            {
                timeline.Update();
            }
        }

        public InstructionsPlayer AddTimeline()
        {
            InstructionsPlayer timeline = new InstructionsPlayer();

            timelines.Add(timeline);

            return timeline;
        }

        public void RemoveTimeline(InstructionsPlayer timeline)
        {
            timelines.Remove(timeline);
        }

        public void Clear()
        {
            timelines.Clear();
        }
    }
}