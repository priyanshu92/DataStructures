using System;
using System.Collections;
using System.Collections.Generic;

namespace DataStructures.Core.Graph
{
    public partial class Graph<T> : IEnumerable<T> where T : IComparable<T>
    {
        public Graph()
        {
        }

        public Graph(List<Vertex<T>> vertices)
        {
            Vertices = vertices;
        }

        public List<Vertex<T>> Vertices { get; } = new List<Vertex<T>>();

        public void AddVertex(Vertex<T> vertex)
        {
            if (vertex is null)
                return;

            Vertices.Add(vertex);
        }

        public void AddVertex(T value)
        {
            Vertices.Add(new Vertex<T>(value));
        }

        public void AddVertices(List<Vertex<T>> vertices)
        {
            if (vertices is null || vertices.Count == 0)
                return;

            Vertices.AddRange(vertices);
        }

        public bool AddDirectedEdge(Vertex<T> from, Vertex<T> to, int cost)
        {
            if (from is null || to is null)
                return false;

            if (from.Neighbours.ContainsKey(to))
                return false;

            from.Neighbours.Add(to, cost);
            return true;
        }

        public bool AddDirectedEdge(T fromValue, T toValue, int cost) => AddDirectedEdge(FindVertex(fromValue), FindVertex(toValue), cost);

        public bool AddUndirectedEdge(Vertex<T> from, Vertex<T> to, int cost)
        {
            if (from is null || to is null)
                return false;

            if (!from.Neighbours.ContainsKey(to))
            {
                from.Neighbours.Add(to, cost);
            }

            if (!to.Neighbours.ContainsKey(from))
            {
                to.Neighbours.Add(from, cost);
            }

            return true;
        }

        public bool AddUndirectedEdge(T fromValue, T toValue, int cost) => AddUndirectedEdge(FindVertex(fromValue), FindVertex(toValue), cost);

        public void TraverseAllVertices(Action<T> action, GraphTraversalType graphTraversalType)
        {
            if (graphTraversalType == GraphTraversalType.BreadthFirstSearch)
                BreadthFirstSearch(action);
            else
                DepthFirstSearch(action);
        }

        public void TraverseFromVertex(Vertex<T> vertex, Action<T> action, GraphTraversalType graphTraversalType)
        {
            if (graphTraversalType == GraphTraversalType.DepthFirstSearch)
            {
                DepthFirstSearchVisit(vertex, action);
            }
            else
            {
                BreadthFirstSearch(vertex, action);
            }
        }

        public void TraverseFromVertex(T value, Action<T> action, GraphTraversalType graphTraversalType) => TraverseFromVertex(FindVertex(value), action, graphTraversalType);

        public Vertex<T> FindVertex(T value)
        {
            if (Vertices.Count == 0)
                return null;
            return Vertices.Find(x => x.Value.CompareTo(value) == 0);
        }

        public IEnumerator<T> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}
