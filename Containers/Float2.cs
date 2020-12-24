using System;

namespace Juce.Core.Containers
{
    public class Float2 : IEquatable<Float2>
    {
        public float X { get; set; }
        public float Y { get; set; }

        public Float2()
        {

        }

        public Float2(float x, float y)
        {
            X = x;
            Y = y;
        }

        public bool Equals(Float2 other)
        {
            return X == other.X && Y == other.Y;
        }
    }
}
