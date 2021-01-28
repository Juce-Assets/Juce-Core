using System;

namespace Juce.Core.Pathfinding
{
    public interface IPathfindingAlgorithm<T> where T : IEquatable<T>
    {
        bool Finished { get; }

        void Start();
        void Update();
    }
}
