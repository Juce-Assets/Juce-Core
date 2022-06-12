using System;

namespace Juce.Core.Pathfinding
{
    public sealed class PathfindingNode<T> where T : IEquatable<T>
    {
        public PathfindingNode<T> Parent { get; }
        public T Value { get; }
        public float Priority { get; }

        public PathfindingNode(PathfindingNode<T> parent, T value, float priority)
        {
            Parent = parent;
            Value = value;
            Priority = priority;
        }
    }
}
