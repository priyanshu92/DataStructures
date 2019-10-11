using System;
using System.Collections.Generic;
using System.Linq;

namespace DataStructures.Core.Graph
{
    public partial class Graph<T>
    {
        /// <summary>
        /// Traverses the entire graph using Depth First Search method
        /// </summary>
        /// <param name="action">The action to perform on each vertex</param>
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

        /// <summary>
        /// Traverses the graph using Depth First Search method from the given start vertex
        /// </summary>
        /// <param name="vertex">The vertex to start traversing</param>
        /// <param name="action">The action to perform on each vertex</param>
        /// <param name="parents">The parents list</param>
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

        /// <summary>
        /// Traverses the entire graph using Breadth First Search method
        /// </summary>
        /// <param name="action">The action to perform on each vertex</param>
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

        /// <summary>
        /// Traverses the graph using Breadth First Search method from the given start vertex
        /// </summary>
        /// <param name="vertex">The vertex to start traversing</param>
        /// <param name="action">The action to perform on each vertex</param>
        /// <param name="parents">The parents list</param>
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

        /// <summary>
        /// Detects cycle in a graph
        /// </summary>
        /// <returns><see langword="true"/> if graph has cycle <see langword="false"/>otherwise</returns>
        public bool IsCyclic()
        {
            if (IsEmpty())
                return false;

            return TypeOfGraph == GraphType.Directed ? IsCyclicDirected() : IsCyclicUndirected();
        }

        private enum ColorType
        {
            White,
            Gray,
            Black
        }

        private enum ProcessingStateType : byte
        {
            Enter,
            Exit
        }

        private bool IsCyclicDirected()
        {
            if (TypeOfGraph != GraphType.Directed)
                throw new InvalidOperationException("The graph is undirected");

            var rootVertices = GetRootVertices();
            if (rootVertices.Count == 0)
                return true;

            foreach (var vertex in rootVertices)
            {
                Dictionary<Vertex<T>, ColorType> states = new Dictionary<Vertex<T>, ColorType>();
                Vertices.ForEach((v) => states.Add(v, ColorType.White));

                Stack<KeyValuePair<Vertex<T>, ProcessingStateType>> stack = new Stack<KeyValuePair<Vertex<T>, ProcessingStateType>>();
                stack.Push(new KeyValuePair<Vertex<T>, ProcessingStateType>(vertex, ProcessingStateType.Enter));

                while (stack.Count > 0)
                {
                    var currentVertex = stack.Pop();

                    if (currentVertex.Value == ProcessingStateType.Exit)
                    {
                        states[currentVertex.Key] = ColorType.Black;
                    }
                    else
                    {
                        states[currentVertex.Key] = ColorType.Gray;
                        stack.Push(new KeyValuePair<Vertex<T>, ProcessingStateType>(currentVertex.Key, ProcessingStateType.Exit));
                    }

                    foreach (var neighbour in currentVertex.Key.Neighbours)
                    {
                        if (states[neighbour.Key] == ColorType.Gray)
                        {
                            return true;
                        }
                        else if (states[neighbour.Key] == ColorType.White)
                        {
                            stack.Push(new KeyValuePair<Vertex<T>, ProcessingStateType>(neighbour.Key, ProcessingStateType.Enter));
                        }
                    }
                }
            }

            return false;
        }

        /// <summary>
        /// Detects cycle in a directed graph
        /// </summary>
        /// <returns><see langword="true"/> if graph has cycle <see langword="false"/>otherwise</returns>
        private bool IsCyclicDirectedRecursive()
        {
            if (TypeOfGraph != GraphType.Directed)
                throw new InvalidOperationException("The graph is undirected");

            HashSet<Vertex<T>> visited = new HashSet<Vertex<T>>();
            HashSet<Vertex<T>> onStack = new HashSet<Vertex<T>>();
            var rootVertices = GetRootVertices();

            foreach (var vertex in rootVertices)
            {
                if (IsCyclicHelper(vertex, visited, onStack))
                    return true;
            }
            return false;
        }

        /// <summary>
        /// Recursively checks for cycle in the graph
        /// </summary>
        /// <returns><see langword="true"/> if graph has cycle <see langword="false"/>otherwise</returns>
        private bool IsCyclicHelper(Vertex<T> vertex, HashSet<Vertex<T>> visited, HashSet<Vertex<T>> onStack)
        {
            if (!visited.Contains(vertex))
            {
                visited.Add(vertex);
                onStack.Add(vertex);

                foreach (var neighbour in vertex.Neighbours)
                {
                    if (!visited.Contains(neighbour.Key) && IsCyclicHelper(neighbour.Key, visited, onStack))
                        return true;
                    else if (onStack.Contains(neighbour.Key))
                        return true;
                }
            }
            onStack.Remove(vertex);
            return false;
        }

        /// <summary>
        /// Detects cycle in an undirected graph
        /// </summary>
        /// <returns><see langword="true"/> if graph has cycle <see langword="false"/>otherwise</returns>
        private bool IsCyclicUndirected()
        {
            if (TypeOfGraph != GraphType.Undirected)
                throw new InvalidOperationException("The graph is directed");

            Dictionary<Vertex<T>, Vertex<T>> parents = new Dictionary<Vertex<T>, Vertex<T>>();
            foreach (var vertex in Vertices)
            {
                if (parents.ContainsKey(vertex))
                    continue;

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
                        else if (parents[currentVertex] != neighbour.Key)
                        {
                            return true;
                        }
                    }
                }
            }

            return false;
        }
    }
}
