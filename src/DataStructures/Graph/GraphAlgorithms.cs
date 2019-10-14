using System;
using System.Collections.Generic;

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
        private static void DepthFirstSearchVisit(Vertex<T> vertex, Action<T> action, Dictionary<Vertex<T>, Vertex<T>> parents = null)
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

                foreach (var neighbour in currentVertex.Neighbours)
                {
                    if (!parents.ContainsKey(neighbour.Vertex))
                    {
                        parents.Add(neighbour.Vertex, currentVertex);
                        stack.Push(neighbour.Vertex);
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
        private static void BreadthFirstSearch(Vertex<T> source, Action<T> action, Dictionary<Vertex<T>, Vertex<T>> parents = null)
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
                    if (!parents.ContainsKey(neighbour.Vertex))
                    {
                        queue.Enqueue(neighbour.Vertex);
                        parents.Add(neighbour.Vertex, currentVertex);
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

        private bool IsCyclicDirectedWithStates()
        {
            if (TypeOfGraph != GraphType.Directed)
                throw new InvalidOperationException("The graph is undirected");

            HashSet<Vertex<T>> visited = new HashSet<Vertex<T>>();
            HashSet<Vertex<T>> onStack = new HashSet<Vertex<T>>();
            Stack<Vertex<T>> stack = new Stack<Vertex<T>>();
            var rootVertices = GetRootVertices();

            foreach (var vertex in rootVertices)
            {
                if (visited.Contains(vertex))
                    continue;

                stack.Push(vertex);

                while (stack.Count > 0)
                {
                    var currentVertex = stack.Pop();

                    if (visited.Contains(currentVertex))
                    {
                        onStack.Remove(currentVertex);
                    }
                    else
                    {
                        visited.Add(currentVertex);
                        onStack.Add(currentVertex);
                        stack.Push(currentVertex);
                    }

                    foreach (var neighbour in currentVertex.Neighbours)
                    {
                        if (!visited.Contains(neighbour.Vertex))
                        {
                            stack.Push(neighbour.Vertex);
                        }
                        else if (onStack.Contains(neighbour.Vertex))
                        {
                            return true;
                        }
                    }
                }
            }

            return false;
        }

        private bool IsCyclicDirected()
        {
            if (TypeOfGraph != GraphType.Directed)
                throw new InvalidOperationException("The graph is undirected");

            HashSet<Vertex<T>> visited = new HashSet<Vertex<T>>();
            HashSet<Vertex<T>> onStack = new HashSet<Vertex<T>>();
            Stack<Vertex<T>> stack = new Stack<Vertex<T>>();
            var rootVertices = GetRootVertices();

            if (rootVertices.Count == 0)
                return true;

            foreach (var vertex in rootVertices)
            {
                if (visited.Contains(vertex))
                    continue;

                stack.Push(vertex);

                while (stack.Count > 0)
                {
                    var currentVertex = stack.Pop();

                    if (visited.Contains(currentVertex))
                    {
                        onStack.Remove(currentVertex);
                    }
                    else
                    {
                        visited.Add(currentVertex);
                        onStack.Add(currentVertex);
                        stack.Push(currentVertex);
                    }

                    foreach (var neighbour in currentVertex.Neighbours)
                    {
                        if (!visited.Contains(neighbour.Vertex))
                        {
                            stack.Push(neighbour.Vertex);
                        }
                        else if (onStack.Contains(neighbour.Vertex))
                        {
                            return true;
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
                    if (!visited.Contains(neighbour.Vertex) && IsCyclicHelper(neighbour.Vertex, visited, onStack))
                        return true;
                    else if (onStack.Contains(neighbour.Vertex))
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

                    foreach (var neighbour in currentVertex.Neighbours)
                    {
                        if (!parents.ContainsKey(neighbour.Vertex))
                        {
                            parents.Add(neighbour.Vertex, currentVertex);
                            stack.Push(neighbour.Vertex);
                        }
                        else if (parents[currentVertex] != neighbour.Vertex)
                        {
                            return true;
                        }
                    }
                }
            }

            return false;
        }

        /// <summary>
        /// Returns the list of vertices in Topological sorted order
        /// </summary>
        /// <returns>The list of vertices</returns>
        public List<Vertex<T>> TopologicalSort(TopSortAlgorithmType algorithmType)
        {
            if (Vertices.Count == 0)
                throw new InvalidOperationException(Messages.EmptyGraph);

            if (TypeOfGraph != GraphType.Directed)
                throw new InvalidOperationException(Messages.TopSortDirectedGraph);

            var rootVertices = GetRootVertices();
            if (rootVertices.Count == 0 || IsCyclic())
                throw new InvalidOperationException(Messages.CyclicGraph);

            return algorithmType == TopSortAlgorithmType.DFS
                ? TopSortUsingDFS(rootVertices)
                : TopSortUsingKahnsAlgorithm();
        }

        private List<Vertex<T>> TopSortUsingDFS(List<Vertex<T>> rootVertices)
        {
            Stack<Vertex<T>> sortedVertices = new Stack<Vertex<T>>();
            List<Vertex<T>> topologicalOrder = new List<Vertex<T>>();
            HashSet<Vertex<T>> visitedVertices = new HashSet<Vertex<T>>();
            foreach (var vertex in rootVertices)
            {
                if (visitedVertices.Contains(vertex))
                    continue;

                TopologicalSortHelper(vertex, visitedVertices, sortedVertices);
            }

            topologicalOrder.AddRange(sortedVertices);

            if (topologicalOrder.Count == 0)
                throw new InvalidOperationException(Messages.CyclicGraph);

            return topologicalOrder;
        }

        private static void TopologicalSortHelper(Vertex<T> vertex, HashSet<Vertex<T>> visitedVertices, Stack<Vertex<T>> sortedVertices)
        {
            if (visitedVertices.Contains(vertex))
                return;

            visitedVertices.Add(vertex);

            foreach (var neighbour in vertex.Neighbours)
            {
                if (visitedVertices.Contains(neighbour.Vertex))
                    continue;
                TopologicalSortHelper(neighbour.Vertex, visitedVertices, sortedVertices);
            }

            sortedVertices.Push(vertex);
        }

        private List<Vertex<T>> TopSortUsingKahnsAlgorithm()
        {
            var degrees = GetVerticesWithInDegree();

            Queue<Vertex<T>> vertices = new Queue<Vertex<T>>();

            foreach (var degree in degrees)
            {
                if (degree.Value == 0)
                    vertices.Enqueue(degree.Key);
            }

            List<Vertex<T>> topOrder = new List<Vertex<T>>();
            int visitedNodes = 0;
            while (vertices.Count > 0)
            {
                var currentVertex = vertices.Dequeue();
                topOrder.Add(currentVertex);

                foreach (var neighbour in currentVertex.Neighbours)
                {
                    if (--degrees[neighbour.Vertex] == 0)
                        vertices.Enqueue(neighbour.Vertex);
                }
                visitedNodes++;
            }
            if (visitedNodes != Vertices.Count)
                throw new InvalidOperationException(Messages.CyclicGraph);

            return topOrder;
        }

        public Dictionary<Vertex<T>, int> GetShortestPath(T fromVertexValue) => GetShortestPath(FindVertex(fromVertexValue));

        public Dictionary<Vertex<T>, int> GetShortestPath(Vertex<T> sourceVertex)
        {
            if (IsEmpty())
                throw new InvalidOperationException(Messages.EmptyGraph);

            if (TypeOfGraph == GraphType.Directed && !IsCyclic())
            {
                return GetShortestPathDAG(sourceVertex);
            }

            //TODO: Implement Dijkstra
            return null;
        }

        private Dictionary<Vertex<T>, int> GetShortestPathDAG(Vertex<T> sourceVertex)
        {
            var topOrder = TopologicalSort(TopSortAlgorithmType.DFS);
            int startingIndex = -1;
            Dictionary<Vertex<T>, int> distance = new Dictionary<Vertex<T>, int>();

            for (int i = 0; i < topOrder.Count; i++)
            {
                if (topOrder[i] == sourceVertex)
                {
                    startingIndex = i;
                    break;
                }
            }

            if (startingIndex == -1)
                return distance;

            distance[topOrder[startingIndex]] = 0;

            for (int i = startingIndex; i < topOrder.Count; i++)
            {
                foreach (var neighbour in topOrder[i].Neighbours)
                {
                    int newDistance = distance[topOrder[i]] + neighbour.Cost;
                    if (distance.ContainsKey(neighbour.Vertex))
                    {
                        int currentDistance = distance[neighbour.Vertex];
                        distance[neighbour.Vertex] = Math.Min(currentDistance, newDistance);
                    }
                    else
                    {
                        distance[neighbour.Vertex] = newDistance;
                    }
                }
            }

            return distance;
        }
    }
}
