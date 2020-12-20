using System;

namespace Juce.Core.Pathfinding
{
    public class PathfindingNode<T> where T : IEquatable<T>
    {
        public PathfindingNode<T> Parent { get; }
        public T Value { get; }

        public PathfindingNode(PathfindingNode<T> parent, T value)
        {
            Parent = parent;
            Value = value;
        }
    }
}
