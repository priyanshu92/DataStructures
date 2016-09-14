using System;
using System.Collections;
using System.Collections.Generic;

namespace DataStructures.Core.Stack
{   
    /// <summary>
    /// Stack implementation using Linked List
    /// </summary>
    /// <typeparam name="T">The type of the stack</typeparam>
    public class Stack<T> : IEnumerable<T>
    {
        
        private LinkedList<T> _list = new LinkedList<T>();


        /// <summary>
        /// Pushes the item at the top of the stack.
        /// </summary>
        /// <param name="item">Item to be pushed.</param>
        public void Push(T item)
        {
            _list.AddFirst(item);
        }

        /// <summary>
        /// Pops the first item from the stack or throws exception if the stack is empty.
        /// </summary>
        /// <returns>Item which is popped.</returns>
        public T Pop()
        {
            if(_list.Count == 0)
            {
                throw new InvalidOperationException("The stack is empty");
            }
            T value = _list.First.Value;
            _list.RemoveFirst();

            return value;
        }

        /// <summary>
        /// Returns the top most item in the stack.
        /// </summary>
        /// <returns>Top most item in the stack.</returns>
        public T Peek()
        {
            if (_list.Count == 0)
            {
                throw new InvalidOperationException("The stack is empty");
            }
            return _list.First.Value;
        }

        /// <summary>
        /// Gives the current number of items in the stack.
        /// </summary>
        public int Count
        {
            get
            {
                return _list.Count;
            }
        }

        /// <summary>
        /// Removes all the items from the list.
        /// </summary>
        public void Clear()
        {
            _list.Clear();
        }

        public IEnumerator<T> GetEnumerator()
        {
            return _list.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _list.GetEnumerator();
        }
    }
}
