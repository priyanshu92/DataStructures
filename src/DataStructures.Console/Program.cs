using DataStructures.Core.Graph;
using static System.Console;

namespace DataStructures.Console
{
    public class Program
    {
        private static void Main(string[] args)
        {
            Graph<int> g = new Graph<int>();
            g.AddVertex(1);
            g.AddVertex(2);
            g.AddVertex(3);
            g.AddVertex(4);
            g.AddVertex(5);
            g.AddVertex(6);

            g.AddUndirectedEdge(1, 3, 0);
            g.AddUndirectedEdge(3, 5, 0);
            g.AddUndirectedEdge(5, 6, 0);
            g.AddUndirectedEdge(6, 4, 0);
            g.AddUndirectedEdge(4, 2, 0);
            g.AddUndirectedEdge(2, 1, 0);
            g.AddUndirectedEdge(2, 5, 0);
            g.AddUndirectedEdge(4, 5, 0);

            g.TraverseFromVertex(1, (t) => WriteLine(t), GraphTraversalType.DepthFirstSearch);
        }
    }
}
