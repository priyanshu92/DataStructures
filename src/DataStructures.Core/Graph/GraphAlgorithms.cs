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

        private void DepthFirstSearchVisit(Vertex<T> vertex, Action<T> action, Dictionary<Vertex<T>, Vertex<T>> parents = null)
        {
            if (parents is null)
                parents = new Dictionary<Vertex<T>, Vertex<T>>();

            if (!parents.ContainsKey(vertex))
                parents.Add(vertex, null);

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
            if (Vertices.Count == 0)
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
                        parents.Add(neighbour.Key, source);
                    }
                }
            }
        }
    }
}
