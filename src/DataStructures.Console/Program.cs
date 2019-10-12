using DataStructures.Core.Graph;
using System;
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

            g.AddDirectedEdge('A', 'B', 0);
            g.AddDirectedEdge('A', 'C', 0);
            g.AddDirectedEdge('B', 'C', 0);
            g.AddDirectedEdge('B', 'E', 0);
            g.AddDirectedEdge('B', 'D', 0);
            g.AddDirectedEdge('C', 'D', 0);
            g.AddDirectedEdge('C', 'G', 0);
            g.AddDirectedEdge('D', 'E', 0);
            g.AddDirectedEdge('D', 'F', 0);
            g.AddDirectedEdge('D', 'G', 0);
            g.AddDirectedEdge('E', 'H', 0);
            g.AddDirectedEdge('F', 'H', 0);
            g.AddDirectedEdge('G', 'H', 0);

            try
            {
                var x = g.TopologicalSort(TopSortAlgorithmType.KahnsAlgorithm);
                foreach (var item in x)
                {
                    WriteLine(item.Value);
                }
            }
            catch (InvalidOperationException ex)
            {
                WriteLine(ex.Message);
            }

            WriteLine();
        }
    }
}
