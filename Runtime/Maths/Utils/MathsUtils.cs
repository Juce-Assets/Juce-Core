namespace Juce.Core.Maths.Utils
{
    public class MathsUtils
    {
        public const int CompletePercentage = 100;

        public static int Clamp(int a, int min, int max)
        {
            return a > max ? max : a < min ? min : a;
        }

        public static float Clamp(float a, float min, float max)
        {
            return a > max ? max : a < min ? min : a;
        }

        public static int Percentage(int baseValue, int percentage)
        {
            return (baseValue * percentage) / CompletePercentage;
        }

        public static float Percentage(float baseValue, float percentage)
        {
            return (baseValue * percentage) / CompletePercentage;
        }

        public static int PercentageClamped(int baseValue, int percentage)
        {
            int clampedPercentage = Clamp(percentage, 0, CompletePercentage);
            return Percentage(baseValue, clampedPercentage);
        }

        public static float PercentageClamped(float baseValue, float percentage)
        {
            float clampedPercentage = Clamp(percentage, 0, CompletePercentage);
            return Percentage(baseValue, clampedPercentage);
        }

        public static int InvPercentage(int baseValue, int modifiedValue)
        {
            return (modifiedValue * CompletePercentage) / baseValue;
        }

        public static float InvPercentage(float baseValue, float modifiedValue)
        {
            return (modifiedValue * CompletePercentage) / baseValue;
        }
    }
}
