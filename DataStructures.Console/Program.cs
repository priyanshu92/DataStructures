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
    }
}
