using System;

namespace Juce.Core.Pathfinding
{
    public interface IPathfindingNode<T> where T : IEquatable<T>
    {
        IPathfindingNode<T> Parent { get; }
        T Value { get; }
    }
}
