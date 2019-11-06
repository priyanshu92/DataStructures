using DataStructures.Graph;
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

        public Dictionary<Vertex<T>, int> GetShortestDistance(T fromVertexValue) => GetShortestDistance(FindVertex(fromVertexValue));

        public List<Vertex<T>> GetShortestPath(T fromVertexValue, T toVertexValue) => GetShortestPath(FindVertex(fromVertexValue), FindVertex(toVertexValue));

        private List<Vertex<T>> GetShortestPath(Vertex<T> sourceVertex, Vertex<T> destinationVertex)
        {
            if (IsEmpty())
                throw new InvalidOperationException(Messages.EmptyGraph);

            if (destinationVertex is null)
                throw new InvalidOperationException("Destination vertex not found");

            List<Vertex<T>> path = new List<Vertex<T>>();
            Dictionary<Vertex<T>, DistanceInfo<T>> distanceInfo = null;

            if (TypeOfGraph == GraphType.Directed && !IsCyclic())
            {
                distanceInfo = GetShortestDistanceDAG(sourceVertex);
            }
            else
            {
                distanceInfo = GetShortestDistanceDijkstra(sourceVertex);
            }

            var vertex = destinationVertex;
            while (vertex != null)
            {
                path.Add(vertex);
                vertex = distanceInfo[vertex].PreviousVertex;
            }
            path.Reverse();
            return path;
        }

        public Dictionary<Vertex<T>, int> GetShortestDistance(Vertex<T> sourceVertex)
        {
            if (IsEmpty())
                throw new InvalidOperationException(Messages.EmptyGraph);

            Dictionary<Vertex<T>, int> distances = new Dictionary<Vertex<T>, int>();
            Dictionary<Vertex<T>, DistanceInfo<T>> distanceInfo = null;

            if (TypeOfGraph == GraphType.Directed && !IsCyclic())
            {
                distanceInfo = GetShortestDistanceDAG(sourceVertex);
            }
            else
            {
                distanceInfo = GetShortestDistanceDijkstra(sourceVertex);
            }

            foreach (var item in distanceInfo)
            {
                distances.Add(item.Key, item.Value.Distance);
            }

            return distances;
        }

        private Dictionary<Vertex<T>, DistanceInfo<T>> GetShortestDistanceDijkstra(Vertex<T> sourceVertex)
        {
            Dictionary<Vertex<T>, DistanceInfo<T>> distances = new Dictionary<Vertex<T>, DistanceInfo<T>>();
            List<Vertex<T>> verticesInShortestPath = new List<Vertex<T>>();

            foreach (var vertex in Vertices)
            {
                distances.Add(vertex, new DistanceInfo<T>(int.MaxValue, null));
            }

            distances[sourceVertex].Distance = 0;

            foreach (var vertex in Vertices)
            {
                Vertex<T> vertexWithMininumDistance = GetVertexWithMinDistance(distances, verticesInShortestPath);
                verticesInShortestPath.Add(vertexWithMininumDistance);

                foreach (var neighbour in vertexWithMininumDistance.Neighbours)
                {
                    if (verticesInShortestPath.Contains(neighbour.Vertex))
                        continue;

                    int currentDistance = distances[neighbour.Vertex].Distance;
                    int newDistance = distances[vertexWithMininumDistance].Distance + neighbour.Cost;

                    if (newDistance < currentDistance)
                    {
                        distances[neighbour.Vertex].Distance = newDistance;
                        distances[neighbour.Vertex].PreviousVertex = vertexWithMininumDistance;
                    }
                }
            }

            return distances;
        }

        private Vertex<T> GetVertexWithMinDistance(Dictionary<Vertex<T>, DistanceInfo<T>> distances, List<Vertex<T>> verticesInShortestPath)
        {
            int minValue = int.MaxValue;
            Vertex<T> result = new Vertex<T>(default);

            foreach (var item in distances)
            {
                if (verticesInShortestPath.Contains(item.Key))
                    continue;

                if (item.Value.Distance < minValue)
                {
                    minValue = item.Value.Distance;
                    result = item.Key;
                }
            }
            return result;
        }

        private Dictionary<Vertex<T>, DistanceInfo<T>> GetShortestDistanceDAG(Vertex<T> sourceVertex)
        {
            var topOrder = TopologicalSort(TopSortAlgorithmType.DFS);
            int startingIndex = -1;
            Dictionary<Vertex<T>, DistanceInfo<T>> distance = new Dictionary<Vertex<T>, DistanceInfo<T>>();

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

            for (int i = startingIndex; i < topOrder.Count; i++)
                distance.Add(topOrder[i], new DistanceInfo<T>(int.MaxValue, null));

            distance[topOrder[startingIndex]].Distance = 0;

            for (int i = startingIndex; i < topOrder.Count; i++)
            {
                foreach (var neighbour in topOrder[i].Neighbours)
                {
                    int newDistance = distance[topOrder[i]].Distance + neighbour.Cost;
                    int currentDistance = distance[neighbour.Vertex].Distance;
                    if (newDistance < currentDistance)
                    {
                        distance[neighbour.Vertex].Distance = newDistance;
                        distance[neighbour.Vertex].PreviousVertex = topOrder[i];
                    }
                }
            }

            return distance;
        }
    }
}
