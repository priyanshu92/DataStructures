using System;
using System.Collections;
using System.Collections.Generic;

namespace DataStructures.Core.Queue
{
    public class QueueLinkedList<T> : IEnumerable<T>
    {
        /// <summary>
        /// The queued items stored in the form of a linked list.
        /// </summary>
        private LinkedList<T> _queue = new LinkedList<T>();

        /// <summary>
        /// Inserts the item at the end of the queue.
        /// </summary>
        /// <param name="item">Item to be inserted.</param>
        public void Enqueue(T item)
        {
            _queue.AddLast(item);
        }

        /// <summary>
        /// Removes the item at the start of the queue
        /// </summary>
        /// <returns>It item which is removed.</returns>
        public T Dequeue()
        {
            if(_queue.Count == 0)
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
