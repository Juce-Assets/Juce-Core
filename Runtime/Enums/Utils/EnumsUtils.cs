using System;
using System.Linq;

namespace Juce.Core.Enums.Utils
{
    public static class EnumsUtils
    {
        public static T[] GetValues<T>()
        {
            return Enum.GetValues(typeof(T)).Cast<T>().ToArray();
        }
    }
}
