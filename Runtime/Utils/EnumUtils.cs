using System;
using System.Linq;

namespace Juce.Core.Utils
{
    public static class EnumUtils
    {
        public static T[] GetValues<T>()
        {
            return Enum.GetValues(typeof(T)).Cast<T>().ToArray();
        }
    }
}
