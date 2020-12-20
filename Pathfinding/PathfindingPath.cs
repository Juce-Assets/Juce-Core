using System;
using System.Collections.Generic;

namespace Juce.Core.Pathfinding
{
    public class PathfindingPath<T> where T : IEquatable<T>
    {
        public PathfindingResultType ResultType { get; }
        public IReadOnlyList<IPathfindingNode<T>> Result { get; }

        public PathfindingPath(PathfindingResultType resultType, IReadOnlyList<IPathfindingNode<T>> result)
        {
            ResultType = resultType;
            Result = result;
        }
    }
}
