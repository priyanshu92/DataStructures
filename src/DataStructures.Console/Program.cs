using DataStructures.Core.Graph;
using static System.Console;

namespace DataStructures.Console
{
    public class Program
    {
        private static void Main(string[] args)
        {
            Graph<int> g = new Graph<int>();
            g.AddVertex(0);
            g.AddVertex(1);
            g.AddVertex(2);
            g.AddVertex(3);

            //g.AddDirectedEdge(0, 1, 0);
            //g.AddDirectedEdge(0, 2, 0);
            //g.AddDirectedEdge(1, 2, 0);
            //g.AddDirectedEdge(2, 0, 0);
            //g.AddDirectedEdge(2, 3, 0);

            g.AddUndirectedEdge(1, 0, 0);
            g.AddUndirectedEdge(1, 2, 0);
            g.AddUndirectedEdge(0, 3, 0);
            //g.AddUndirectedEdge(2, 3, 0);

            if (g.IsCyclic())
                WriteLine("Cyclic Graph");
            else
                WriteLine("Acyclic Graph");

            WriteLine();
        }
    }
}
