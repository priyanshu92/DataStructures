using DataStructures.Core;
using DataStructures.Core.BinaryHeap;
using System;
using System.Collections.Generic;
using static System.Console;

namespace DataStructures.Console
{
    public class Program
    {
        private static void Main(string[] args)
        {
            BinaryHeap heap = new BinaryHeap(BinaryHeapType.MaxHeap);
            heap.Add(1);
            heap.Add(4);
            heap.Add(4);
            heap.Add(3);
            heap.Add(15);
            heap.Add(6);
            heap.Add(17);

            WriteLine(heap.Max);
            heap.Delete();
            WriteLine(heap.Max);
            heap.Delete();
            WriteLine(heap.Max);
        }

        private static void PostFixCalculator(string[] args)
        {
            Stack<int> values = new Stack<int>();

            foreach (var token in args)
            {
                int value;

                if (int.TryParse(token, out value))
                {
                    values.Push(value);
                }
                else
                {
                    int rhs = values.Pop();
                    int lhs = values.Pop();

                    switch (token)
                    {
                        case "+":
                            values.Push(lhs + rhs);
                            break;

                        case "-":
                            values.Push(lhs - rhs);
                            break;

                        case "*":
                            values.Push(lhs * rhs);
                            break;

                        case "/":
                            values.Push(lhs / rhs);
                            break;

                        case "%":
                            values.Push(lhs % rhs);
                            break;

                        default:
                            throw new ArgumentException($"Unrecognized token {token}");
                    }
                }
            }

            WriteLine(values.Pop());
        }

        private static void NodesListExample()
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

        private static void PrintList(Node node)
        {
            while (node != null)
            {
                WriteLine($"Value: {node.Value}");
                node = node.Next;
            }
        }
    }
}
