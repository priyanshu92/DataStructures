using System;

namespace DataStructures.Core.BinaryHeap
{
    public class BinaryHeap
    {
        private readonly BinaryHeapType _heapType;
        private int[] _items;
        private const int INITIAL_HEAP_SIZE = 20;
        private int _count;

        public int Max
        {
            get
            {
                if (_count == 0)
                    return -1;
                return _heapType == BinaryHeapType.MaxHeap ? _items[0] : -1;
            }
        }

        public int Min
        {
            get
            {
                if (_count == 0)
                    return -1;
                return _heapType == BinaryHeapType.MinHeap ? _items[0] : -1;
            }
        }

        public BinaryHeap(BinaryHeapType heapType)
        {
            _heapType = heapType;
            _items = new int[INITIAL_HEAP_SIZE];
            _count = 0;
        }

        private int GetParentIndex(int index)
        {
            if (index <= 0 || index >= _items.Length)
                return -1;

            return (int)Math.Floor((decimal)(index - 1) / 2);
        }

        private int GetLeftChildIndex(int index)
        {
            int leftChildIndex = 2 * index + 1;
            if (leftChildIndex >= _count)
                return -1;
            return leftChildIndex;
        }

        private int GetRightChildIndex(int index)
        {
            int rightChildIndex = 2 * index + 2;
            if (rightChildIndex >= _count)
                return -1;
            return rightChildIndex;
        }

        private void PercolateDown(int i)
        {
            int leftChildIndex = GetLeftChildIndex(i);
            int rightChildIndex = GetRightChildIndex(i);
            int heapElementIndex = i;

            if (_heapType == BinaryHeapType.MaxHeap)
            {
                if (leftChildIndex != -1 && _items[leftChildIndex] > _items[i])
                    heapElementIndex = leftChildIndex;

                if (rightChildIndex != -1 && _items[rightChildIndex] > _items[heapElementIndex])
                    heapElementIndex = rightChildIndex;
            }
            else if (_heapType == BinaryHeapType.MinHeap)
            {
                if (leftChildIndex != -1 && _items[leftChildIndex] < _items[i])
                    heapElementIndex = leftChildIndex;

                if (rightChildIndex != -1 && _items[rightChildIndex] < _items[heapElementIndex])
                    heapElementIndex = rightChildIndex;
            }

            if (heapElementIndex != i)
            {
                int temp = _items[heapElementIndex];
                _items[heapElementIndex] = _items[i];
                _items[i] = temp;
                PercolateDown(heapElementIndex);
            }
        }

        public int Delete()
        {
            int data = -1;
            if (_count == 0)
                return data;

            data = _items[0];
            _items[0] = _items[_count - 1];

            _count--;

            PercolateDown(0);
            return data;
        }

        public void Add(int data)
        {
            if (_count == _items.Length)
                ResizeHeap();

            _count++;
            int i = _count - 1;
            while (i >= 0 && GetParentIndex(i) != -1 && ((_heapType == BinaryHeapType.MaxHeap && data > _items[GetParentIndex(i)]) || (_heapType == BinaryHeapType.MinHeap && data < _items[GetParentIndex(i)])))
            {
                _items[i] = _items[GetParentIndex(i)];
                i = GetParentIndex(i);
            }
            _items[i] = data;
        }

        private void ResizeHeap()
        {
            int[] newItems = new int[_items.Length * 2];
            _items.CopyTo(newItems, 0);
            _items = newItems;
        }
    }
}
