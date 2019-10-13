using System;
using System.Collections;
using System.Collections.Generic;

namespace DataStructures.Core.Queue
{
    public class QueueArray<T> : IEnumerable<T>
    {
        /// <summary>
        /// Array to store the items of the queue
        /// </summary>
        T[] _items = new T[0];

        /// <summary>
        /// The number of items in the queue
        /// </summary>
        int _size;

        /// <summary>
        /// The index of the first item in the queue.
        /// </summary>
        int _head = 0;

        /// <summary>
        /// The index of the last item in the queue.
        /// </summary>
        int _tail = -1;

        /// <summary>
        /// Adds the item to the end of the queue.
        /// </summary>
        /// <param name="item">Item to be added.</param>
        public void Enqueue(T item)
        {
            if (_size == _items.Length)
            {
                int newLength = _size == 0 ? 4 : _size * 2;

                T[] newArray = new T[newLength];

                if (_size > 0)
                {
                    int targetIndex = 0;

                    if (_tail < _head)
                    {
                        for (int index = _head; index < _items.Length; index++)
                        {
                            newArray[targetIndex++] = _items[index];
                        }
                        for (int index = 0; index <= _tail; index++)
                        {
                            newArray[targetIndex++] = _items[index];
                        }
                    }
                    else
                    {
                        for (int index = _head; index <= _tail; index++)
                        {
                            newArray[targetIndex++] = _items[index];
                        }
                    }

                    _head = 0;
                    _tail = targetIndex - 1;
                }
                else
                {
                    _head = 0;
                    _tail = -1;
                }

                _items = newArray;

            }

            if (_tail == _items.Length - 1)
            {
                _tail = 0;
            }
            else
            {
                _tail++;
            }
            _items[_tail] = item;
            _size++;
        }

        /// <summary>
        /// Removes the first element from the Queue
        /// </summary>
        /// <returns>The item removed</returns>
        public T Dequeue()
        {
            if (_size == 0)
            {
                throw new InvalidOperationException("The queue is empty");
            }

            T value = _items[_head];

            if (_head == _items.Length - 1)
            {
                _head = 0;
            }
            else
            {
                _head++;
            }
            _size--;
            return value;

        }


        /// <summary>
        /// Returns the first item in the queue without removing it.
        /// </summary>
        /// <returns>First item in the queue.</returns>
        public T Peek()
        {
            if (_size == 0)
            {
                throw new InvalidOperationException("The queue is empty");
            }

            return _items[_head];
        }

        /// <summary>
        /// The current number of items in the queue.
        /// </summary>
        public int Count
        {
            get
            {
                return _size;
            }
        }

        /// <summary>
        /// Removes all the items from the queue.
        /// </summary>
        public void Clear()
        {
            _size = 0;
            _head = 0;
            _tail = -1;
            _items = null;
        }

        /// <summary>
        /// Returns an enumerator that enumerates a queue.
        /// </summary>
        /// <returns>The enumerator</returns>
        public IEnumerator<T> GetEnumerator()
        {
            if (_tail < _head)
            {
                // Head -> end
                for(int i = _head; i < _items.Length - 1; i++)
                {
                    yield return _items[i];
                }

                // 0 -> Tail
                for (int i = 0; i <= _tail; i++)
                {
                    yield return _items[i];
                }
            }
            else
            {
                for (int i = _head; i <= _tail; i++)
                {
                    yield return _items[i];
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
