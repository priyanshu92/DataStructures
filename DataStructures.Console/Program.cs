using System;
using DataStructures.Core;
using System.Collections.Generic;

namespace DataStructures.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            //NodesListExample();

            PostFixCalculator(args);
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

            System.Console.WriteLine(values.Pop());
        }

        static void NodesListExample()
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
            while (node != null)
            {
                System.Console.WriteLine($"Value: {node.Value}");
                node = node.Next;
            }
        }
    }
}
