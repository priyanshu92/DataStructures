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
            g.AddVertex(4);
            g.AddVertex(5);
            g.AddVertex(6);

            g.AddDirectedEdge(1, 3, 0);
            g.AddDirectedEdge(3, 4, 0);
            g.AddDirectedEdge(1, 4, 0);
            g.AddDirectedEdge(0, 1, 0);
            g.AddDirectedEdge(0, 2, 0);
            g.AddDirectedEdge(2, 4, 0);

            //g.AddDirectedEdge(0, 1, 0);
            //g.AddDirectedEdge(1, 2, 0);
            //g.AddDirectedEdge(1, 3, 0);
            //g.AddDirectedEdge(3, 4, 0);
            //g.AddDirectedEdge(5, 6, 0);
            //g.AddDirectedEdge(6, 3, 0);
            //g.AddDirectedEdge(4, 6, 0);

            if (g.IsCyclic())
                WriteLine("Cyclic Graph");
            else
                WriteLine("Acyclic Graph");

            WriteLine();
        }
    }
}
