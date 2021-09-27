namespace Juce.Core
{
    public class MathUtils
    {
        public const int CompletePercentage = 100;

        public static int Clamp(int a, int min, int max)
        {
            return a > max ? max : a < min ? min : a;
        }

        public static int Percentage(int baseValue, int percentage)
        {
            return (baseValue * percentage) / CompletePercentage;
        }

        public static int PercentageClamped(int baseValue, int percentage)
        {
            int clampedPercentage = Clamp(percentage, 0, CompletePercentage);
            return Percentage(baseValue, clampedPercentage);
        }

        public static int InvPercentage(int baseValue, int modifiedValue)
        {
            return (modifiedValue * CompletePercentage) / baseValue;
        }
    }
}
