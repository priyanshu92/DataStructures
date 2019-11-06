using DataStructures.Core.Graph;
using System;
using System.Collections.Generic;

namespace DataStructures.Graph
{
    public class DistanceComparer<T> : IComparer<KeyValuePair<Vertex<T>, DistanceInfo<T>>> where T : IComparable<T>
    {
        public int Compare(KeyValuePair<Vertex<T>, DistanceInfo<T>> x, KeyValuePair<Vertex<T>, DistanceInfo<T>> y)
        {
            return x.Value.Distance.CompareTo(y.Value.Distance);
        }
    }
}
