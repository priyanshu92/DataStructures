using System;
using System.Collections.Generic;

namespace DataStructures.Core.Graph
{
    public partial class Graph<T> where T : IComparable<T>
    {
        /// <summary>
        /// Creates a new graph
        /// </summary>
        public Graph()
        {
        }

        /// <summary>
        /// Creates a new graph with the given vertices
        /// </summary>
        /// <param name="vertices">The vertices of the graph</param>
        public Graph(List<Vertex<T>> vertices)
        {
            Vertices = vertices;
        }

        /// <summary>
        /// The type of graph. See <see cref="GraphType"/>
        /// </summary>
        public GraphType TypeOfGraph { get; private set; }

        /// <summary>
        /// The list of vertices in the graph
        /// </summary>
        public List<Vertex<T>> Vertices { get; } = new List<Vertex<T>>();

        /// <summary>
        /// Adds a new vertex to the graph
        /// </summary>
        /// <param name="vertex">The verted to be added</param>
        public void AddVertex(Vertex<T> vertex)
        {
            if (vertex is null)
                return;

            Vertices.Add(vertex);
        }

        /// <summary>
        /// Adds a new vertex to the graph
        /// </summary>
        /// <param name="value">The value of the vertex to be added</param>
        public void AddVertex(T value)
        {
            Vertices.Add(new Vertex<T>(value));
        }

        /// <summary>
        /// Adds the vertices to the graph
        /// </summary>
        /// <param name="vertices">The vertices to be added</param>
        public void AddVertices(List<Vertex<T>> vertices)
        {
            if (vertices is null || vertices.Count == 0)
                return;

            Vertices.AddRange(vertices);
        }

        /// <summary>
        /// Creates a directed edge between 2 given vertices
        /// </summary>
        /// <param name="from">The start vertex</param>
        /// <param name="to">The end vertex</param>
        /// <param name="cost">The cost of the edge</param>
        /// <returns><see langword="true"/>if the edge is added <see langword="false"/> otherwise</returns>
        public bool AddDirectedEdge(Vertex<T> from, Vertex<T> to, int cost)
        {
            if (from is null || to is null)
                return false;

            if (from.Neighbours.ContainsKey(to))
                return false;

            if (IsEmpty())
                TypeOfGraph = GraphType.Directed;

            if (TypeOfGraph != GraphType.Directed)
                throw new InvalidOperationException("Cannot add a directed edge in an undirected graph");

            from.Neighbours.Add(to, cost);
            return true;
        }

        /// <summary>
        /// Creates a directed edge between 2 vertices with given values (assumes the values are unique in the graph)
        /// </summary>
        /// <param name="fromValue">The start vertex value</param>
        /// <param name="toValue">The end vertex value</param>
        /// <param name="cost">The cost of the edge</param>
        /// <returns><see langword="true"/>if the edge is added <see langword="false"/> otherwise</returns>
        public bool AddDirectedEdge(T fromValue, T toValue, int cost) => AddDirectedEdge(FindVertex(fromValue), FindVertex(toValue), cost);

        /// <summary>
        /// Creates an undirected edge between 2 given vertices
        /// </summary>
        /// <param name="firstVertex">The first vertex</param>
        /// <param name="secondVertex">The second vertex</param>
        /// <param name="cost">The cost of the edge</param>
        /// <returns><see langword="true"/>if the edge is added <see langword="false"/> otherwise</returns>
        public bool AddUndirectedEdge(Vertex<T> firstVertex, Vertex<T> secondVertex, int cost)
        {
            if (firstVertex is null || secondVertex is null)
                return false;

            if (IsEmpty())
                TypeOfGraph = GraphType.Undirected;

            if (TypeOfGraph != GraphType.Undirected)
                throw new InvalidOperationException("Cannot add an undirected edge in a directed graph");

            if (!firstVertex.Neighbours.ContainsKey(secondVertex))
            {
                firstVertex.Neighbours.Add(secondVertex, cost);
            }

            if (!secondVertex.Neighbours.ContainsKey(firstVertex))
            {
                secondVertex.Neighbours.Add(firstVertex, cost);
            }

            return true;
        }

        /// <summary>
        /// Creates an undirected edge between 2 vertices with given values (assumes the values are unique in the graph)
        /// </summary>
        /// <param name="firstVertexValue">The first vertex value</param>
        /// <param name="secondVertexValue">The second vertex value</param>
        /// <param name="cost">The cost of the edge</param>
        /// <returns><see langword="true"/>if the edge is added <see langword="false"/> otherwise</returns>
        public bool AddUndirectedEdge(T firstVertexValue, T secondVertexValue, int cost) => AddUndirectedEdge(FindVertex(firstVertexValue), FindVertex(secondVertexValue), cost);

        /// <summary>
        /// Traverses all the vertices of the graph
        /// </summary>
        /// <param name="action">The action to perform on each vertex</param>
        /// <param name="graphTraversalType">The traversal type</param>
        public void TraverseAllVertices(Action<T> action, GraphTraversalType graphTraversalType)
        {
            if (graphTraversalType == GraphTraversalType.BreadthFirstSearch)
                BreadthFirstSearch(action);
            else
                DepthFirstSearch(action);
        }

        /// <summary>
        /// Traverses the graph from the given source vertex
        /// </summary>
        /// <param name="vertex">The vertex to start traversing</param>
        /// <param name="action">The action to perform on each vertex</param>
        /// <param name="graphTraversalType">The traversal type</param>
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

        /// <summary>
        /// Traverses the graph from the vertex with the given value
        /// </summary>
        /// <param name="value">The value of the vertex to start traversing</param>
        /// <param name="action">The action to perform</param>
        /// <param name="graphTraversalType">The traversal type</param>
        public void TraverseFromVertex(T value, Action<T> action, GraphTraversalType graphTraversalType) => TraverseFromVertex(FindVertex(value), action, graphTraversalType);

        /// <summary>
        /// Finds a vertex with the given value in the graph
        /// </summary>
        /// <param name="value">The value of the vertex to find</param>
        /// <returns>The vertex if found <see langword="null"/>otherwise</returns>
        public Vertex<T> FindVertex(T value)
        {
            if (Vertices.Count == 0)
                return null;
            return Vertices.Find(x => x.Value.CompareTo(value) == 0);
        }

        /// <summary>
        /// Checks if the graph is empty or not
        /// </summary>
        /// <returns><see langword="true"/>if graph contains no vertices or no edges <see langword="false"/>otherwise</returns>
        public bool IsEmpty()
        {
            if (Vertices.Count == 0)
                return true;

            return Vertices.TrueForAll(x => x.Neighbours.Count == 0);
        }

        /// <summary>
        /// Returns a list of all the vertices with in-order degree 0
        /// </summary>
        /// <returns>A reference to the list containig the vertices</returns>
        public List<Vertex<T>> GetRootVertices()
        {
            List<Vertex<T>> verticesWithDegreeZero = new List<Vertex<T>>();
            Dictionary<Vertex<T>, int> degree = new Dictionary<Vertex<T>, int>();

            if (Vertices.Count > 0)
            {
                Vertices.ForEach((vertex) => degree.Add(vertex, 0));

                Vertices.ForEach((vertex) =>
                {
                    foreach (var neighbour in vertex.Neighbours)
                    {
                        degree[neighbour.Key]++;
                    }
                });
            }

            foreach (var vertex in degree)
            {
                if (vertex.Value == 0)
                    verticesWithDegreeZero.Add(vertex.Key);
            }

            return verticesWithDegreeZero;
        }
    }
}
