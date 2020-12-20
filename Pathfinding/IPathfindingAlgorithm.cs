using System;

namespace Juce.Core.Pathfinding
{
    public interface IPathfindingAlgorithm<T>
    {
        bool Finished { get; }
        PathfindingPath<T> Result { get; }

        void Start();
        void Update();
    }
}
