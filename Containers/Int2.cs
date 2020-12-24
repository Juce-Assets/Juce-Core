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

        public Int2(int x, int y)
        {
            X = x;
            Y = y;
        }

        public bool Equals(Int2 other)
        {
            return X == other.X && Y == other.Y;
        }
    }
}
