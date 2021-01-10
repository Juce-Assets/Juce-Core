using Juce.Core.Collections;
using System;
using System.Collections.Generic;

namespace Juce.Core.Pathfinding.Algorithms
{
    public class AStarPathfindingAlgorithm<T> : IPathfindingAlgorithm<T> where T : IEquatable<T>
    {
        private readonly Func<T, IReadOnlyList<T>> getChildsFunc;
        private readonly Func<T, float> getPriorityFunc;
        private readonly Func<AStarPathfindingResult, PathfindingNode<T>, PathfindingPath<T>> generateResultFunc;

        private readonly T originValue;
        private readonly T destinationValue;

        private readonly PriorityQueue<PathfindingNode<T>> toCheck = new PriorityQueue<PathfindingNode<T>>();
        private readonly Dictionary<T, PathfindingNode<T>> visited = new Dictionary<T, PathfindingNode<T>>();

        public bool Finished { get; private set; }
        public PathfindingPath<T> Result { get; private set; }

        public AStarPathfindingAlgorithm(
            Func<T, IReadOnlyList<T>> getChildsFunc,
            Func<T, float> getPriorityFunc,
            Func<AStarPathfindingResult, PathfindingNode<T>, PathfindingPath<T>> generateResultFunc,
            T originValue,
            T destinationValue
            )
        {
            this.getChildsFunc = getChildsFunc;
            this.getPriorityFunc = getPriorityFunc;
            this.generateResultFunc = generateResultFunc;
            this.originValue = originValue;
            this.destinationValue = destinationValue;
        }

        public void Start()
        {
            if (getChildsFunc == null)
            {
                throw new ArgumentNullException($"Get childs function was null at {nameof(AStarPathfindingAlgorithm<T>)}");
            }

            if (originValue == null)
            {
                throw new ArgumentNullException($"Origin {nameof(T)} was null at {nameof(AStarPathfindingAlgorithm<T>)}");
            }

            if(originValue.Equals(destinationValue))
            {
                Finish(AStarPathfindingResult.OriginIsDestination, new PathfindingNode<T>(null, originValue, float.MinValue));
            }

            AddToCheck(null, new List<T> { originValue });
        }

        public void Update()
        {
            if (Finished)
            {
                return;
            }

            if (toCheck.Count == 0)
            {
                Finish(AStarPathfindingResult.DestinationCouldNotBeReached, GetVisitedWithMorePriority());
                Finished = true;
                return;
            }

            PathfindingNode<T> nodeToCheck = PopToCheck();

            bool isDestination = nodeToCheck.Value.Equals(destinationValue);

            if(isDestination)
            {
                Finish(AStarPathfindingResult.DestinationReached, nodeToCheck);
                Finished = true;
                return;
            }

            AddToVisited(nodeToCheck);

            IReadOnlyList<T> childValues = getChildsFunc.Invoke(nodeToCheck.Value);

            AddToCheck(nodeToCheck, childValues);
        }

        private PathfindingNode<T> PopToCheck()
        { 
            return toCheck.PopFront();
        }

        private void AddToCheck(PathfindingNode<T> parent, IReadOnlyList<T> values)
        {
            foreach (T value in values)
            {
                if (IsAlreadyToCheck(value))
                {
                    continue;
                }

                if (IsAlreadyVisited(value))
                {
                    continue;
                }

                float priority = getPriorityFunc.Invoke(value);

                PathfindingNode<T> newPathfindingNode = new PathfindingNode<T>(parent, value, priority);

                toCheck.Add(newPathfindingNode, priority);
            }
        }

        private bool IsAlreadyToCheck(T value)
        {
            if (value == null)
            {
                throw new ArgumentNullException($"{nameof(T)} was null checking {nameof(IsAlreadyToCheck)}" +
                    $"at {nameof(BFSPathfindingAlgorithm<T>)}");
            }

            for(int i = 0; i < toCheck.Count; ++i)
            {
                bool equals = toCheck.At(i).Value.Equals(value);

                if (equals)
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
                    $"at {nameof(AStarPathfindingAlgorithm<T>)}");
            }

            visited.Add(node.Value, node);
        }

        private bool IsAlreadyVisited(T value)
        {
            if (value == null)
            {
                throw new ArgumentNullException($"{nameof(T)} was null checking {nameof(IsAlreadyVisited)}" +
                    $"at {nameof(AStarPathfindingAlgorithm<T>)}");
            }

            return visited.ContainsKey(value);
        }

        private PathfindingNode<T> GetVisitedWithMorePriority()
        {
            PathfindingNode<T> maxPriorityNode = null;
            float maxPriority = float.MinValue;

            foreach(PathfindingNode<T> node in visited.Values)
            {
                if(node.Priority > maxPriority)
                {
                    maxPriorityNode = node;
                    maxPriority = node.Priority;
                }
            }

            return maxPriorityNode;
        }

        private void Finish(AStarPathfindingResult result, PathfindingNode<T> destinationNode)
        {
            Finished = true;

            Result = generateResultFunc.Invoke(result, destinationNode);
        }
    }
}
