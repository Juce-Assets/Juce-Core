using System;

namespace Juce.Core.Containers
{
    [Serializable]
    public struct Int2 : IEquatable<Int2>
    {
        public int X { get; set; }
        public int Y { get; set; }

        public Int2(Int2 value)
        {
            X = value.X;
            Y = value.Y;
        }

        public Int2(int x, int y)
        {
            X = x;
            Y = y;
        }

        public bool Equals(Int2 other)
        {
            return X == other.X && Y == other.Y;
        }

        public float Distance(Int2 value)
        {
            return (float)System.Math.Sqrt(System.Math.Pow(value.X - X, 2) + System.Math.Pow(value.Y - Y, 2));
        }

        public Int2 ManhattanDistance(Int2 value)
        {
            return new Int2(value.X - X, value.Y - Y);
        }

        public void MakeAbs()
        {
            X = System.Math.Abs(X);
            Y = System.Math.Abs(Y);
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
            if (!(obj is Int2))
            {
                return false;
            }

            return Equals((Int2)obj);
        }

        public static bool operator ==(Int2 left, Int2 right)
        {
            if (left == null || right == null)
            {
                return false;
            }

            return left.Equals(right);
        }

        public static bool operator !=(Int2 left, Int2 right)
        {
            return !(left == right);
        }
    }
}
