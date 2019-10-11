using DataStructures.Core.Graph;
using static System.Console;

namespace DataStructures.Console
{
    public class Program
    {
        private static void Main(string[] args)
        {
            Graph<char> g = new Graph<char>();
            g.AddVertex('A');
            g.AddVertex('B');
            g.AddVertex('C');
            g.AddVertex('D');
            g.AddVertex('E');
            g.AddVertex('F');

            g.AddUndirectedEdge('A', 'B', 0);
            g.AddUndirectedEdge('B', 'D', 0);
            g.AddUndirectedEdge('D', 'F', 0);
            g.AddUndirectedEdge('F', 'E', 0);
            g.AddUndirectedEdge('E', 'C', 0);
            g.AddUndirectedEdge('C', 'A', 0);
            g.AddUndirectedEdge('B', 'E', 0);
            g.AddUndirectedEdge('D', 'E', 0);

            WriteLine("Breadth First Search Traversal: ");
            g.TraverseAllVertices((t) => Write($"{t} "), GraphTraversalType.BreadthFirstSearch);
            WriteLine("\n\n");
            WriteLine("Depth First Search Traversal: ");
            g.TraverseAllVertices((t) => Write($"{t} "), GraphTraversalType.DepthFirstSearch);
            WriteLine();
        }
    }
}
