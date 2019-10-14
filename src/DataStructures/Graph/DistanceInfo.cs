using DataStructures.Core.Graph;
using System;

namespace DataStructures.Graph
{
    public class DistanceInfo<T> where T : IComparable<T>
    {
        public int Distance { get; set; }
        public Vertex<T> PreviousVertex { get; set; }

        public DistanceInfo(int distance, Vertex<T> previous)
        {
            Distance = distance;
            PreviousVertex = previous;
        }
    }
}
