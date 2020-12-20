using System;
using System.Collections.Generic;
using System.Linq;

namespace Juce.Core.Pathfinding
{
    public class BFSPathfindingAlgorithm<T> : IPathfindingAlgorithm<T> where T : IEquatable<T>
    {
        private readonly Func<IPathfindingNode<T>, IReadOnlyList<IPathfindingNode<T>>> getChildsFunc;
        private readonly IPathfindingNode<T> originNode;

        private readonly List<IPathfindingNode<T>> toCheck = new List<IPathfindingNode<T>>();
        private readonly Dictionary<T, IPathfindingNode<T>> visited = new Dictionary<T, IPathfindingNode<T>>();

        public bool Finished { get; private set; }
        public PathfindingPath<T> Result { get; private set; }

        public BFSPathfindingAlgorithm(
            Func<IPathfindingNode<T>, IReadOnlyList<IPathfindingNode<T>>> getChildsFunc,
            IPathfindingNode<T> originNode
            )
        {
            this.getChildsFunc = getChildsFunc;
            this.originNode = originNode;
        }

        public void Start()
        {
            if (getChildsFunc == null)
            {
                throw new ArgumentNullException($"Get childs function was null at {nameof(BFSPathfindingAlgorithm<T>)}");
            }

            if (originNode == null)
            {
                throw new ArgumentNullException($"Origin {nameof(IPathfindingNode<T>)} was null at {nameof(BFSPathfindingAlgorithm<T>)}");
            }

            toCheck.Add(originNode);
        }

        public void Update()
        {
            if(Finished)
            {
                return;
            }

            if(toCheck.Count == 0)
            {
                GenerateResult();
                Finished = true;
                return;
            }

            IPathfindingNode<T> nodeToCheck = PopToCheck();

            AddToVisited(nodeToCheck);

            IReadOnlyList<IPathfindingNode<T>> childNodes = getChildsFunc.Invoke(nodeToCheck);

            AddToCheck(childNodes);
        }

        private IPathfindingNode<T> PopToCheck()
        {
            IPathfindingNode<T> ret = toCheck[0];

            toCheck.RemoveAt(0);

            return ret;
        }

        private void AddToCheck(IReadOnlyList<IPathfindingNode<T>> nodes)
        {
            foreach(IPathfindingNode<T> node in nodes)
            {
                if(IsAlreadyToCheck(node))
                {
                    continue;
                }

                if (IsAlreadyVisited(node))
                {
                    continue;
                }

                toCheck.Add(node);
            }
        }

        private bool IsAlreadyToCheck(IPathfindingNode<T> node)
        {
            if (node == null)
            {
                throw new ArgumentNullException($"{nameof(IPathfindingNode<T>)} was null checking {nameof(IsAlreadyToCheck)}" +
                    $"at {nameof(BFSPathfindingAlgorithm<T>)}");
            }

            foreach(IPathfindingNode<T> nodeToCheck in toCheck)
            {
                bool equal = nodeToCheck.Value.Equals(node.Value);

                if(equal)
                {
                    return true;
                }
            }

            return false;
        }

        private void AddToVisited(IPathfindingNode<T> node)
        {
            if (node == null)
            {
                throw new ArgumentNullException($"{nameof(IPathfindingNode<T>)} was null checking if {nameof(AddToVisited)}" +
                    $"at {nameof(BFSPathfindingAlgorithm<T>)}");
            }

            visited.Add(node.Value, node);
        }

        private bool IsAlreadyVisited(IPathfindingNode<T> node)
        {
            if(node == null)
            {
                throw new ArgumentNullException($"{nameof(IPathfindingNode<T>)} was null checking {nameof(IsAlreadyVisited)}" +
                    $"at {nameof(BFSPathfindingAlgorithm<T>)}");
            }

            return visited.ContainsKey(node.Value);
        }

        private void GenerateResult()
        {
            IReadOnlyList<IPathfindingNode<T>> result = visited.Values.ToList();

            Result = new PathfindingPath<T>(PathfindingResultType.Complete, result);
        }
    }
}
