using DataStructures.Core.BinaryHeap;
using static System.Console;

namespace DataStructures.Console
{
    public class Program
    {
        private static void Main(string[] args)
        {
            int[] arr = new int[] { 17, 6, 15, 3, 1, 4, 4 };
            BinaryHeap heap = BinaryHeap.BuildHeap(arr, BinaryHeapType.MaxHeap);

            WriteLine(heap.Max);
            heap.Delete();
            WriteLine(heap.Max);
            heap.Delete();
            WriteLine(heap.Max);
        }
    }
}
