using System;

namespace Juce.Core.Extensions
{
    public static class FloatExtensions
    {
        public static TimeSpan ToSeconds(this float value)
        {
            return TimeSpan.FromSeconds(value);
        }
    }
}
