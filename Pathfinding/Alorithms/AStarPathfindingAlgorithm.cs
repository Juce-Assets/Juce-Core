using Juce.Core.Collections;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Juce.Core.Pathfinding
{
    public class AStarPathfindingAlgorithm<T> : IPathfindingAlgorithm<T> where T : IEquatable<T>
    {
        private readonly Func<IPathfindingNode<T>, IReadOnlyList<IPathfindingNode<T>>> getChildsFunc;
        private readonly Func<IPathfindingNode<T>, float> getPriorityFunc;
        private readonly IPathfindingNode<T> originNode;
        private readonly IPathfindingNode<T> destinationNode;

        private readonly PriorityQueue<IPathfindingNode<T>> toCheck = new PriorityQueue<IPathfindingNode<T>>();
        private readonly Dictionary<T, IPathfindingNode<T>> visited = new Dictionary<T, IPathfindingNode<T>>();

        public bool Finished { get; private set; }
        public PathfindingPath<T> Result { get; private set; }

        public AStarPathfindingAlgorithm(
            Func<IPathfindingNode<T>, IReadOnlyList<IPathfindingNode<T>>> getChildsFunc,
            Func<IPathfindingNode<T>, float> getPriorityFunc,
            IPathfindingNode<T> originNode,
            IPathfindingNode<T> destinationNode
            )
        {
            this.getChildsFunc = getChildsFunc;
            this.getPriorityFunc = getPriorityFunc;
            this.originNode = originNode;
            this.destinationNode = destinationNode;
        }

        public void Start()
        {
            if (getChildsFunc == null)
            {
                throw new ArgumentNullException($"Get childs function was null at {nameof(AStarPathfindingAlgorithm<T>)}");
            }

            if (originNode == null)
            {
                throw new ArgumentNullException($"Origin {nameof(IPathfindingNode<T>)} was null at {nameof(AStarPathfindingAlgorithm<T>)}");
            }

            if (destinationNode == null)
            {
                throw new ArgumentNullException($"Destination {nameof(IPathfindingNode<T>)} was null at {nameof(AStarPathfindingAlgorithm<T>)}");
            }

            bool equal = originNode.Value.Equals(destinationNode.Value);

            if(equal)
            {
                GenerateResult(PathfindingResultType.Complete);
                Finished = true;
                return;
            }

            toCheck.Add(originNode, 0);
        }

        public void Update()
        {
            if (Finished)
            {
                return;
            }

            if (toCheck.Count == 0)
            {
                GenerateResult(PathfindingResultType.Partial);
                Finished = true;
                return;
            }

            IPathfindingNode<T> nodeToCheck = PopToCheck();

            if (nodeToCheck.Value.Equals(destinationNode.Value))
            {
                GenerateResult(PathfindingResultType.Complete);
                Finished = true;
                return;
            }

            AddToVisited(nodeToCheck);

            IReadOnlyList<IPathfindingNode<T>> childNodes = getChildsFunc.Invoke(nodeToCheck);

            AddToCheck(childNodes);
        }

        private IPathfindingNode<T> PopToCheck()
        {
            return toCheck.PopFront();
        }

        private void AddToCheck(IReadOnlyList<IPathfindingNode<T>> nodes)
        {
            foreach (IPathfindingNode<T> node in nodes)
            {
                if (IsAlreadyToCheck(node))
                {
                    continue;
                }

                if (IsAlreadyVisited(node))
                {
                    continue;
                }

                float priority = getPriorityFunc(node);

                toCheck.Add(node, priority);
            }
        }

        private bool IsAlreadyToCheck(IPathfindingNode<T> node)
        {
            if (node == null)
            {
                throw new ArgumentNullException($"{nameof(IPathfindingNode<T>)} was null checking {nameof(IsAlreadyToCheck)}" +
                    $"at {nameof(AStarPathfindingAlgorithm<T>)}");
            }

            for(int i = 0; i < toCheck.Count; ++i)
            {
                bool equal = toCheck.At(i).Value.Equals(node.Value);

                if (equal)
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
                    $"at {nameof(AStarPathfindingAlgorithm<T>)}");
            }

            visited.Add(node.Value, node);
        }

        private bool IsAlreadyVisited(IPathfindingNode<T> node)
        {
            if (node == null)
            {
                throw new ArgumentNullException($"{nameof(IPathfindingNode<T>)} was null checking {nameof(IsAlreadyVisited)}" +
                    $"at {nameof(AStarPathfindingAlgorithm<T>)}");
            }

            return visited.ContainsKey(node.Value);
        }

        private void GenerateResult(PathfindingResultType resultType)
        {
            IReadOnlyList<IPathfindingNode<T>> result = visited.Values.ToList();

            Result = new PathfindingPath<T>(resultType, result);
        }
    }
}
