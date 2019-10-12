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
            g.AddVertex('G');
            g.AddVertex('H');
            g.AddVertex('I');
            g.AddVertex('J');
            g.AddVertex('K');
            g.AddVertex('L');
            g.AddVertex('M');

            g.AddDirectedEdge('C', 'A', 0);
            g.AddDirectedEdge('C', 'B', 0);
            g.AddDirectedEdge('A', 'D', 0);
            g.AddDirectedEdge('B', 'D', 0);
            g.AddDirectedEdge('E', 'A', 0);
            g.AddDirectedEdge('E', 'D', 0);
            g.AddDirectedEdge('E', 'F', 0);
            g.AddDirectedEdge('D', 'H', 0);
            g.AddDirectedEdge('D', 'G', 0);
            g.AddDirectedEdge('G', 'I', 0);
            g.AddDirectedEdge('H', 'I', 0);
            g.AddDirectedEdge('H', 'J', 0);
            g.AddDirectedEdge('I', 'L', 0);
            g.AddDirectedEdge('J', 'L', 0);
            g.AddDirectedEdge('J', 'M', 0);
            g.AddDirectedEdge('K', 'J', 0);
            g.AddDirectedEdge('K', 'J', 0);
            g.AddDirectedEdge('F', 'K', 0);
            g.AddDirectedEdge('F', 'J', 0);

            var x = g.TopologicalSort();
            foreach (var item in x)
            {
                WriteLine(item.Value);
            }

            WriteLine();
        }
    }
}
