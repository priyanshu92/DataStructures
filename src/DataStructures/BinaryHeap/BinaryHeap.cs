using System;
using System.Collections.Generic;

namespace DataStructures.Core.BinaryHeap
{
    public class BinaryHeap<T>
    {
        private readonly IComparer<T> _comparer;
        private readonly BinaryHeapType _heapType;
        private T[] _items;
        private const int INITIAL_HEAP_SIZE = 2;
        private int _count;

        /// <summary>
        /// The largest element in the heap. (Only if heap type is <see cref="BinaryHeapType.MaxHeap"/>. It returns -1 if heap type is <see cref="BinaryHeapType.MinHeap"/>
        /// </summary
        public T Max
        {
            get
            {
                if (_count == 0)
                    return default;
                return _heapType == BinaryHeapType.MaxHeap ? _items[0] : default;
            }
        }

        /// <summary>
        /// The smallest element in the heap. (Only if heap type is <see cref="BinaryHeapType.MinHeap"/>. It returns -1 if heap type is <see cref="BinaryHeapType.MaxHeap"/>
        /// </summary>
        public T Min
        {
            get
            {
                if (_count == 0)
                    return default;
                return _heapType == BinaryHeapType.MinHeap ? _items[0] : default;
            }
        }

        /// <summary>
        /// Returns the number of items in the binary heap
        /// </summary>
        public int Count
        {
            get
            {
                return _count;
            }
        }

        /// <summary>
        /// Creates a new <see cref="BinaryHeap"/> based on the given <see cref="BinaryHeapType"/>
        /// </summary>
        /// <param name="heapType">The type of heap to be created</param>
        public BinaryHeap(BinaryHeapType heapType)
        {
            _heapType = heapType;
            _items = new T[INITIAL_HEAP_SIZE];
            _count = 0;
            _comparer = Comparer<T>.Default;
        }

        /// <summary>
        /// Creates a new <see cref="BinaryHeap"/> based on the given <see cref="BinaryHeapType"/> and <see cref="IComparer{T}"/>
        /// </summary>
        /// <param name="comparer">The comparer to use</param>
        public BinaryHeap(BinaryHeapType heapType, IComparer<T> comparer)
            : this(heapType)
        {
            _comparer = comparer;
        }

        /// <summary>
        /// Returns the index of the parent
        /// </summary>
        /// <param name="index">The child index for which we are getting the parent</param>
        /// <returns>The index of the parent if exists otherwise -1 (for root node)</returns>
        private int GetParentIndex(int index)
        {
            if (index <= 0 || index >= _items.Length)
                return -1;

            return (int)Math.Floor((decimal)(index - 1) / 2);
        }

        /// <summary>
        /// Returns the index of the left child
        /// </summary>
        /// <param name="index">The parent index for which we are getting the child index</param>
        /// <returns>The index of the left child if exists otherwise returns -1</returns>
        private int GetLeftChildIndex(int index)
        {
            int leftChildIndex = 2 * index + 1;
            if (leftChildIndex >= _count)
                return -1;
            return leftChildIndex;
        }

        /// <summary>
        /// Returns the index of the right child
        /// </summary>
        /// <param name="index">The parent index for which we are getting the child index</param>
        /// <returns>The index of the right child if exists otherwise returns -1</returns>
        private int GetRightChildIndex(int index)
        {
            int rightChildIndex = 2 * index + 2;
            if (rightChildIndex >= _count)
                return -1;
            return rightChildIndex;
        }

        /// <summary>
        /// Recursively reorganizes the heap nodes to satisfy heap order property
        /// </summary>
        /// <param name="i">The index of the element to start from</param>
        private void PercolateDown(int i)
        {
            int leftChildIndex = GetLeftChildIndex(i);
            int rightChildIndex = GetRightChildIndex(i);
            int heapElementIndex = i;

            if (_heapType == BinaryHeapType.MaxHeap)
            {
                if (leftChildIndex != -1 && _comparer.Compare(_items[leftChildIndex], _items[i]) > 0)
                    heapElementIndex = leftChildIndex;

                if (rightChildIndex != -1 && _comparer.Compare(_items[rightChildIndex], _items[heapElementIndex]) >= 0)
                    heapElementIndex = rightChildIndex;
            }
            else if (_heapType == BinaryHeapType.MinHeap)
            {
                if (leftChildIndex != -1 && _comparer.Compare(_items[leftChildIndex], _items[i]) < 0)
                    heapElementIndex = leftChildIndex;

                if (rightChildIndex != -1 && _comparer.Compare(_items[rightChildIndex], _items[heapElementIndex]) <= 0)
                    heapElementIndex = rightChildIndex;
            }

            if (heapElementIndex != i)
            {
                T temp = _items[heapElementIndex];
                _items[heapElementIndex] = _items[i];
                _items[i] = temp;
                PercolateDown(heapElementIndex);
            }
        }

        /// <summary>
        /// Deletes the top element from the heap. (Smallest element for MinHeap and largest for a MaxHeap)
        /// </summary>
        /// <returns>The value of the element deleted</returns>
        public T Delete()
        {
            T data = default;
            if (_count == 0)
                return data;

            data = _items[0];
            _items[0] = _items[_count - 1];

            _count--;

            PercolateDown(0);
            return data;
        }

        /// <summary>
        /// Adds a new element in the binary heap
        /// </summary>
        /// <param name="data">The element to be added</param>
        public void Add(T data)
        {
            if (_count == _items.Length)
                ResizeHeap();

            _count++;
            int i = _count - 1;
            while (i >= 0 && GetParentIndex(i) != -1 && ((_heapType == BinaryHeapType.MaxHeap && _comparer.Compare(data, _items[GetParentIndex(i)]) > 0) || (_heapType == BinaryHeapType.MinHeap && _comparer.Compare(data, _items[GetParentIndex(i)]) < 0)))
            {
                _items[i] = _items[GetParentIndex(i)];
                i = GetParentIndex(i);
            }
            _items[i] = data;
        }

        /// <summary>
        /// Creates a new array (twice the size of current array) to store the heap elements and copies the existing items in the new array
        /// </summary>
        private void ResizeHeap()
        {
            T[] newItems = new T[_items.Length * 2];
            _items.CopyTo(newItems, 0);
            _items = newItems;
        }

        /// <summary>
        /// Builds a binary heap from the given input array
        /// </summary>
        /// <param name="inputArray">The array to be converted to heap</param>
        /// <param name="binaryHeapType">The binary heap type. See <see cref="BinaryHeapType"/></param>
        /// <returns>A reference to the binary heap if created successfully otherwise null</returns>
        public static BinaryHeap<T> BuildHeap(T[] inputArray, BinaryHeapType binaryHeapType)
        {
            if (inputArray is null || inputArray.Length == 0)
                return null;

            BinaryHeap<T> heap = new BinaryHeap<T>(binaryHeapType);
            while (inputArray.Length > heap._items.Length)
                heap.ResizeHeap();

            inputArray.CopyTo(heap._items, 0);

            heap._count = inputArray.Length;

            for (int i = inputArray.Length - 1; i >= 0; i--)
                heap.PercolateDown(i);

            return heap;
        }

        public static void Sort(T[] inputArray)
        {
            if (inputArray is null || inputArray.Length == 0)
                return;

            var heap = BuildHeap(inputArray, BinaryHeapType.MaxHeap);

            for (int i = 0; i < inputArray.Length; i++)
            {
                Swap(ref heap._items[0], ref heap._items[heap._count - 1]);
                heap._count--;
                heap.PercolateDown(0);
            }

            for (int i = 0; i < inputArray.Length; i++)
            {
                inputArray[i] = heap._items[i];
            }
        }

        private static void Swap(ref T item1, ref T item2)
        {
            T temp = item1;
            item1 = item2;
            item2 = temp;
        }
    }
}
