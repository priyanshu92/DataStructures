using DataStructures.Core;

namespace DataStructures.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            // +-----+------+
            // |  3  | null +
            // +-----+------+
            Node first = new Node { Value = 3 };

            // +-----+------+       +-----+------+ 
            // |  3  | null +       |  5  | null +
            // +-----+------+       +-----+------+
            Node middle = new Node { Value = 5 };

            // +-----+------+       +-----+------+ 
            // |  3  | null +------>|  5  | null +
            // +-----+------+       +-----+------+
            first.Next = middle;

            // +-----+------+       +-----+------+      +-----+------+ 
            // |  3  | null +------>|  5  | null +      |  7  | null +
            // +-----+------+       +-----+------+      +-----+------+
            Node last = new Node { Value = 7 };

            // +-----+------+       +-----+------+      +-----+------+ 
            // |  3  | null +------>|  5  | null +----->|  7  | null +
            // +-----+------+       +-----+------+      +-----+------+
            middle.Next = last;


            // Iterate over the each node and print the value
            PrintList(first);
        }

        static void PrintList(Node node)
        {
            while(node != null)
            {
                System.Console.WriteLine($"Value: {node.Value}");
                node = node.Next;
            }
        }
    }
}
