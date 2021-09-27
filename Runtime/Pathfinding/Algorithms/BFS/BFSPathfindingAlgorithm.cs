using System;
using System.Collections.Generic;

namespace Juce.Core.Pathfinding.Algorithms
{
    public class BFSPathfindingAlgorithm<T> : IPathfindingAlgorithm<T> where T : IEquatable<T>
    {
        private readonly Func<T, IReadOnlyList<T>> getChildsFunc;
        private readonly Func<IReadOnlyDictionary<T, PathfindingNode<T>>, List<T>> generateResultFunc;

        private readonly T originValue;

        private readonly List<PathfindingNode<T>> toCheck = new List<PathfindingNode<T>>();
        private readonly Dictionary<T, PathfindingNode<T>> visited = new Dictionary<T, PathfindingNode<T>>();

        public bool Finished { get; private set; }
        public List<T> Result { get; private set; }

        public BFSPathfindingAlgorithm(
            Func<T, IReadOnlyList<T>> getChildsFunc,
            Func<IReadOnlyDictionary<T, PathfindingNode<T>>, List<T>> generateResultFunc,
            T originValue
            )
        {
            this.getChildsFunc = getChildsFunc;
            this.generateResultFunc = generateResultFunc;
            this.originValue = originValue;
        }

        public void Start()
        {
            if (getChildsFunc == null)
            {
                throw new ArgumentNullException($"Get childs function was null at {nameof(BFSPathfindingAlgorithm<T>)}");
            }

            if (originValue == null)
            {
                throw new ArgumentNullException($"Origin {nameof(T)} was null at {nameof(BFSPathfindingAlgorithm<T>)}");
            }

            AddToCheck(null, new List<T> { originValue });
        }

        public void Update()
        {
            if(Finished)
            {
                return;
            }

            if(toCheck.Count == 0)
            {
                Finish();
                Finished = true;
                return;
            }

            PathfindingNode<T> nodeToCheck = PopToCheck();

            AddToVisited(nodeToCheck);

            IReadOnlyList<T> childValues = getChildsFunc.Invoke(nodeToCheck.Value);

            AddToCheck(nodeToCheck, childValues);
        }

        private PathfindingNode<T> PopToCheck()
        {
            PathfindingNode<T> ret = toCheck[0];

            toCheck.RemoveAt(0);

            return ret;
        }

        private void AddToCheck(PathfindingNode<T> parent, IReadOnlyList<T> values)
        {
            foreach(T value in values)
            {
                if(IsAlreadyToCheck(value))
                {
                    continue;
                }

                if (IsAlreadyVisited(value))
                {
                    continue;
                }

                PathfindingNode<T> newPathfindingNode = new PathfindingNode<T>(parent, value, 0);

                toCheck.Add(newPathfindingNode);
            }
        }

        private bool IsAlreadyToCheck(T value)
        {
            if (value == null)
            {
                throw new ArgumentNullException($"{nameof(T)} was null checking {nameof(IsAlreadyToCheck)}" +
                    $"at {nameof(BFSPathfindingAlgorithm<T>)}");
            }

            foreach(PathfindingNode<T> nodeToCheck in toCheck)
            {
                bool equals = nodeToCheck.Value.Equals(value);

                if(equals)
                {
                    return true;
                }
            }

            return false;
        }

        private void AddToVisited(PathfindingNode<T> node)
        {
            if (node == null)
            {
                throw new ArgumentNullException($"{nameof(PathfindingNode<T>)} was null checking if {nameof(AddToVisited)}" +
                    $"at {nameof(BFSPathfindingAlgorithm<T>)}");
            }

            visited.Add(node.Value, node);
        }

        private bool IsAlreadyVisited(T value)
        {
            if(value == null)
            {
                throw new ArgumentNullException($"{nameof(T)} was null checking {nameof(IsAlreadyVisited)}" +
                    $"at {nameof(BFSPathfindingAlgorithm<T>)}");
            }

            return visited.ContainsKey(value);
        }

        private void Finish()
        {
            Finished = true;

            Result = generateResultFunc.Invoke(visited);
        }
    }
}
