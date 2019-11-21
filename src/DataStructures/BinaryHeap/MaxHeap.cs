using DataStructures.BinaryHeap;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataStructures.BinaryHeap
{
    public class MaxHeap<T> : BinaryHeap<T>
    {
        public MaxHeap()
            : base()
        {

        }

        public MaxHeap(IComparer<T> comparer)
            : base(comparer)
        {

        }

        protected override void PercolateDown(int i)
        {
            int leftChildIndex = GetLeftChildIndex(i);
            int rightChildIndex = GetRightChildIndex(i);
            int heapElementIndex = i;

            if (leftChildIndex != -1 && _comparer.Compare(_items[leftChildIndex], _items[i]) > 0)
                heapElementIndex = leftChildIndex;

            if (rightChildIndex != -1 && _comparer.Compare(_items[rightChildIndex], _items[heapElementIndex]) >= 0)
                heapElementIndex = rightChildIndex;

            if (heapElementIndex != i)
            {
                T temp = _items[heapElementIndex];
_items[heapElementIndex] = _items[i];
                _items[i] = temp;
                PercolateDown(heapElementIndex);
            }
        }

        protected override void PercolateUp(int currentIdx)
        {
            int i = currentIdx;
            while (true)
            {
                int parentIndex = GetParentIndex(i);
                if (parentIndex == -1)
                    return;
                if (_comparer.Compare(_items[parentIndex], _items[i]) > 0)
                    Swap(ref _items[parentIndex], ref _items[i]);
                i = parentIndex;
            }
        }
    }
}
