using System;
using System.Collections.Generic;
using System.Linq;

namespace DataStructures.Core.Graph
{
    public partial class Graph<T>
    {
        private void DepthFirstSearch(Action<T> action)
        {
            if (IsEmpty())
                return;

            Dictionary<Vertex<T>, Vertex<T>> parents = new Dictionary<Vertex<T>, Vertex<T>>();

            foreach (var vertex in Vertices)
            {
                if (!parents.ContainsKey(vertex))
                {
                    parents.Add(vertex, null);
                    DepthFirstSearchVisit(vertex, action, parents);
                }
            }
        }

        private void DepthFirstSearchVisit(Vertex<T> vertex, Action<T> action, Dictionary<Vertex<T>, Vertex<T>> parents = null)
        {
            if (parents is null)
                parents = new Dictionary<Vertex<T>, Vertex<T>>();

            if (!parents.ContainsKey(vertex))
                parents.Add(vertex, null);

            Stack<Vertex<T>> stack = new Stack<Vertex<T>>();

            stack.Push(vertex);

            while (stack.Count > 0)
            {
                var currentVertex = stack.Pop();

                action(currentVertex.Value);

                foreach (var neighbour in currentVertex.Neighbours.Reverse())
                {
                    if (!parents.ContainsKey(neighbour.Key))
                    {
                        parents.Add(neighbour.Key, currentVertex);
                        stack.Push(neighbour.Key);
                    }
                }
            }
        }

        private void BreadthFirstSearch(Action<T> action)
        {
            if (IsEmpty())
                return;

            Dictionary<Vertex<T>, Vertex<T>> parents = new Dictionary<Vertex<T>, Vertex<T>>();

            foreach (var vertex in Vertices)
            {
                if (!parents.ContainsKey(vertex))
                    BreadthFirstSearch(vertex, action, parents);
            }
        }

        private void BreadthFirstSearch(Vertex<T> source, Action<T> action, Dictionary<Vertex<T>, Vertex<T>> parents = null)
        {
            if (parents == null)
                parents = new Dictionary<Vertex<T>, Vertex<T>>();

            Queue<Vertex<T>> queue = new Queue<Vertex<T>>();
            queue.Enqueue(source);

            if (!parents.ContainsKey(source))
                parents.Add(source, null);

            while (queue.Count > 0)
            {
                var currentVertex = queue.Dequeue();

                action(currentVertex.Value);

                foreach (var neighbour in currentVertex.Neighbours)
                {
                    if (!parents.ContainsKey(neighbour.Key))
                    {
                        queue.Enqueue(neighbour.Key);
                        parents.Add(neighbour.Key, currentVertex);
                    }
                }
            }
        }

        public bool IsCyclic()
        {
            if (IsEmpty())
                return false;

            return GraphType == GraphType.Undirected ? DetectCycleUndirected() : DetectCycleDirected();
        }

        private bool DetectCycleDirected()
        {
            Dictionary<Vertex<T>, Vertex<T>> parents = new Dictionary<Vertex<T>, Vertex<T>>();

            foreach (var vertex in Vertices)
            {
                if (!parents.ContainsKey(vertex))
                {
                    parents.Add(vertex, null);

                    if (!parents.ContainsKey(vertex))
                        parents.Add(vertex, null);

                    Stack<Vertex<T>> stack = new Stack<Vertex<T>>();

                    stack.Push(vertex);

                    while (stack.Count > 0)
                    {
                        var currentVertex = stack.Pop();

                        foreach (var neighbour in currentVertex.Neighbours.Reverse())
                        {
                            if (!parents.ContainsKey(neighbour.Key))
                            {
                                parents.Add(neighbour.Key, currentVertex);
                                stack.Push(neighbour.Key);
                            }
                            else
                            {
                                return true;
                            }
                        }
                    }
                }
            }

            return false;
        }

        private bool DetectCycleUndirected()
        {
            Dictionary<Vertex<T>, Vertex<T>> parents = new Dictionary<Vertex<T>, Vertex<T>>();

            foreach (var vertex in Vertices)
            {
                if (!parents.ContainsKey(vertex))
                {
                    parents.Add(vertex, null);

                    if (!parents.ContainsKey(vertex))
                        parents.Add(vertex, null);

                    Stack<Vertex<T>> stack = new Stack<Vertex<T>>();

                    stack.Push(vertex);

                    while (stack.Count > 0)
                    {
                        var currentVertex = stack.Pop();

                        foreach (var neighbour in currentVertex.Neighbours.Reverse())
                        {
                            if (parents.ContainsKey(neighbour.Key) && parents[currentVertex] != neighbour.Key)
                                return true;

                            if (!parents.ContainsKey(neighbour.Key))
                            {
                                parents.Add(neighbour.Key, currentVertex);
                                stack.Push(neighbour.Key);
                            }
                        }
                    }
                }
            }

            return false;
        }
    }
}
