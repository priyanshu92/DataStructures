using System;
using System.Collections.Generic;

namespace DataStructures.Core.Graph
{
    public partial class Graph<T>
    {
        private void DepthFirstSearch(Action<T> action)
        {
            if (Vertices.Count == 0)
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

        private void DepthFirstSearchVisit(Vertex<T> vertex, Action<T> action, Dictionary<Vertex<T>, Vertex<T>> parents)
        {
            action(vertex.Value);

            foreach (var neighbour in vertex.Neighbours)
            {
                if (!parents.ContainsKey(neighbour.Key))
                {
                    parents.Add(neighbour.Key, vertex);
                    DepthFirstSearchVisit(neighbour.Key, action, parents);
                }
            }
        }

        private void BreadthFirstSearch(Action<T> action)
        {
            HashSet<Vertex<T>> visitedNodes = new HashSet<Vertex<T>>();

            foreach (var vertex in Vertices)
            {
                if (!visitedNodes.Contains(vertex))
                    BreadthFirstSearch(vertex, action, visitedNodes);
            }
        }

        private void BreadthFirstSearch(Vertex<T> source, Action<T> action, HashSet<Vertex<T>> visitedNodes = null)
        {
            if (visitedNodes == null)
                visitedNodes = new HashSet<Vertex<T>>();

            Queue<Vertex<T>> queue = new Queue<Vertex<T>>();
            queue.Enqueue(source);

            if (!visitedNodes.Contains(source))
                visitedNodes.Add(source);

            while (queue.Count > 0)
            {
                var currentVertex = queue.Dequeue();

                action(currentVertex.Value);

                foreach (var neighbour in currentVertex.Neighbours)
                {
                    if (!visitedNodes.Contains(neighbour.Key))
                    {
                        queue.Enqueue(neighbour.Key);
                        visitedNodes.Add(neighbour.Key);
                    }
                }
            }
        }
    }
}
