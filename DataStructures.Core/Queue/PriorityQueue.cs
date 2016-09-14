using System;
using System.Collections;
using System.Collections.Generic;

namespace DataStructures.Core.Queue
{
    public class PriorityQueue<T> : IEnumerable<T>
        where T: IComparable<T>
    {
        /// <summary>
        /// The queued items stored in the form of a linked list in priority order.
        /// </summary>
        private LinkedList<T> _queue = new LinkedList<T>();

        /// <summary>
        /// Inserts the item according to the priority.
        /// </summary>
        /// <param name="item">Item to be inserted.</param>
        public void Enqueue(T item)
        {
            if(_queue.Count == 0)
            {
                _queue.AddLast(item);
            }
            else
            {
                var current = _queue.First;

                while (current != null && current.Value.CompareTo(item)>0)
                {
                    current = current.Next;
                }

                if (current == null)
                {
                    _queue.AddLast(item);
                }
                else
                {
                    _queue.AddBefore(current, item);
                }
            }
        }

        /// <summary>
        /// Removes the item at the start of the queue
        /// </summary>
        /// <returns>It item which is removed.</returns>
        public T Dequeue()
        {
            if (_queue.Count == 0)
            {
                throw new InvalidOperationException("Queue is empty");
            }

            T item = _queue.First.Value;
            _queue.RemoveFirst();
            return item;
        }

        /// <summary>
        /// Returns the first item in the queue
        /// </summary>
        /// <returns></returns>
        public T Peek()
        {
            if (_queue.Count == 0)
            {
                throw new InvalidOperationException("Queue is empty");
            }
            return _queue.First.Value;
        }

        /// <summary>
        /// The number of items in the queue
        /// </summary>
        public int Count
        {
            get
            {
                return _queue.Count;
            }
        }

        /// <summary>
        /// Removes all the items from the queue
        /// </summary>
        public void Clear()
        {
            _queue.Clear();
        }

        public IEnumerator<T> GetEnumerator()
        {
            return _queue.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _queue.GetEnumerator();
        }
    }
}
