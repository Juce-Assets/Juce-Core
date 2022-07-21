using System;

namespace Juce.Core.Extensions
{
    public static class IntExtensions
    {
        public static TimeSpan ToSeconds(this int value)
        {
            return TimeSpan.FromSeconds(value);
        }
    }
}
