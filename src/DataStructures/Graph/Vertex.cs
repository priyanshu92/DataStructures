using DataStructures.Graph;
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
            Neighbours = new List<Neighbour<T>>();
        }

        public Vertex(T value, List<Neighbour<T>> neighbours)
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
        public List<Neighbour<T>> Neighbours { get; }

        public int CompareTo(T other)
        {
            return Value.CompareTo(other);
        }
    }
}
