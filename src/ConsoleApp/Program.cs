using DataStructures.Core.Graph;
using System;
using static System.Console;

namespace ConsoleApp
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

            g.AddDirectedEdge('A', 'B', 3);
            g.AddDirectedEdge('A', 'C', 6);
            g.AddDirectedEdge('B', 'C', 4);
            g.AddDirectedEdge('B', 'E', 11);
            g.AddDirectedEdge('B', 'D', 4);
            g.AddDirectedEdge('C', 'D', 8);
            g.AddDirectedEdge('C', 'G', 11);
            g.AddDirectedEdge('D', 'E', -4);
            g.AddDirectedEdge('D', 'F', 5);
            g.AddDirectedEdge('D', 'G', 2);
            g.AddDirectedEdge('E', 'H', 9);
            g.AddDirectedEdge('F', 'H', 1);
            g.AddDirectedEdge('G', 'H', 2);

            try
            {
                var x = g.GetShortestPath('A');
                foreach (var item in x)
                {
                    WriteLine($"{item.Key.Value} -> {item.Value}");
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
