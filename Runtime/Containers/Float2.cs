using System;

namespace Juce.Core.Containers
{
    public struct Float2 : IEquatable<Float2>
    {
        public float X { get; set; }
        public float Y { get; set; }

        public Float2(Float2 value)
        {
            X = value.X;
            Y = value.Y;
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

        public override string ToString()
        {
            return $"X:{X}, Y:{Y}";
        }

        public override int GetHashCode()
        {
            return (X, Y).GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (!(obj is Float2))
            {
                return false;
            }

            return Equals((Float2)obj);
        }

        public static bool operator ==(Float2 left, Float2 right)
        {
            if (left == null || right == null)
            {
                return false;
            }

            return left.Equals(right);
        }

        public static bool operator !=(Float2 left, Float2 right)
        {
            return !(left == right);
        }
    }
}
