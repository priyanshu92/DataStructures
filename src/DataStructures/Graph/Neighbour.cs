using DataStructures.Core.Graph;
using System;

namespace DataStructures.Graph
{
    public class Neighbour<T> where T : IComparable<T>
    {
        public Vertex<T> Vertex { get; }
        public int Cost { get; }

        public Neighbour(Vertex<T> vertex, int cost)
        {
            Vertex = vertex;
            Cost = cost;
        }
    }
}
