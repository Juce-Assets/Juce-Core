using System;

namespace Juce.Core.Containers
{
    [System.Serializable]
    public class Int2 : IEquatable<Int2>
    {
        public int X { get; set; }
        public int Y { get; set; }

        public Int2()
        {

        }

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

        public override int GetHashCode()
        {
            return (X, Y).GetHashCode();
        }

        public float Distance(Int2 value)
        {
            return (float)Math.Sqrt(Math.Pow(value.X - X, 2) + Math.Pow(value.Y - Y, 2));
        }

        public Int2 ManhattanDistance(Int2 value)
        {
            return new Int2(value.X - X, value.Y - Y);
        }

        public void MakeAbs()
        {
            X = Math.Abs(X);
            Y = Math.Abs(Y);
        }
    }
}
