namespace Juce.Core.Time
{
    public class TimeContextTimer : CallbackTimer
    {
        public ITimeContext TimeContext { get; }

        public TimeContextTimer(TickableTimeContext tickableTimeContext) 
            : base(() => tickableTimeContext.Time)
        {
            TimeContext = tickableTimeContext;
        }
    }
}