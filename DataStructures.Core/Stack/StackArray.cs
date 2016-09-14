using System;
using System.Collections;
using System.Collections.Generic;

namespace DataStructures.Core.Stack
{
    public class StackArray<T> : IEnumerable<T>
    {
        /// <summary>
        /// The array of items in the Stack. Initialized to 0 length and it grows as the
        /// items are pushed in the array.
        /// </summary>
        T[] _items = new T[0];

        /// <summary>
        /// The current number of items in the Stack.
        /// </summary>
        int _size;

        /// <summary>
        /// Adds the specified item in the stack.
        /// </summary>
        /// <param name="item">Item to be pushed.</param>
        public void Push(T item)
        {
            if (_size == _items.Length)
            {
                // Get the new size.
                int newLength = _size == 0 ? 4 : _size * 2;

                // Create a new array with new size and copy the items in it.
                T[] newArray = new T[newLength];
                _items.CopyTo(newArray, 0);
                _items = newArray;

            }

            //Add the item on the Stack
            _items[_size++] = item;
        }


        /// <summary>
        /// Removes the top item from the Stack.
        /// </summary>
        /// <returns>Item which is popped.</returns>
        public T Pop()
        {
            if (_size == 0)
            {
                throw new InvalidOperationException("The stack is empty");
            }

            _size--;
            return _items[_size];
        }

        /// <summary>
        /// Returns the top item in the Stack.
        /// </summary>
        /// <returns>The top most item in the Stack.</returns>
        public T Peek()
        {
            if (_size == 0)
            {
                throw new InvalidOperationException("The stack is empty");
            }

            return _items[_size - 1];
        }

        /// <summary>
        /// The current number of items in the Stack
        /// </summary>
        public int Count
        {
            get
            {
                return _size;
            }
        }


        /// <summary>
        /// Removes all items from the Stack.
        /// </summary>
        public void Clear()
        {
            _size = 0;
            _items = null;
        }

        public IEnumerator<T> GetEnumerator()
        {
            for (int i = _size - 1; i >= 0; i--)
            {
                yield return _items[i];
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
