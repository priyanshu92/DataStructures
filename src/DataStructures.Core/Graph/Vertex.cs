using System;
using System.Collections.Generic;

namespace DataStructures.Core.Graph
{
    /// <summary>
    /// Represents a vertex in a graph.
    /// </summary>
    /// <typeparam name="T">The type of data vertex stores</typeparam>
    public class Vertex<T> : IComparable<T> where T : IComparable<T>
    {
        public Vertex(T value)
        {
            Value = value;
            Neighbours = new Dictionary<Vertex<T>, int>();
        }

        public Vertex(T value, Dictionary<Vertex<T>, int> neighbours)
        {
            Value = value;
            Neighbours = neighbours;
        }

        /// <summary>
        /// The value of the vertex
        /// </summary>
        public T Value { get; }

        /// <summary>
        /// The list of neighbours along with the cost associated with each neighbour
        /// </summary>
        public Dictionary<Vertex<T>, int> Neighbours { get; }

        public int CompareTo(T other)
        {
            return Value.CompareTo(other);
        }
    }
}
