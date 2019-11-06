using DataStructures.Core.Graph;
using System;
using System.Linq;
using static System.Console;

namespace ConsoleApp
{
    public class Program
    {
        private static void Main(string[] args)
        {
            Graph<char> g = new Graph<char>();

            g.AddUndirectedEdge('A', 'B', 10);
            g.AddUndirectedEdge('B', 'D', 2);
            g.AddUndirectedEdge('D', 'E', 7);
            g.AddUndirectedEdge('C', 'E', 2);
            g.AddUndirectedEdge('A', 'C', 3);
            g.AddUndirectedEdge('B', 'C', 1);
            g.AddUndirectedEdge('B', 'C', 1);
            g.AddUndirectedEdge('C', 'D', 8);

            try
            {
                var x = g.GetShortestPath('A', 'E');
                WriteLine(string.Join(" -> ", x.Select(y => y.Value)));
            }
            catch (InvalidOperationException ex)
            {
                WriteLine(ex.Message);
            }

            WriteLine();
        }
    }
}
