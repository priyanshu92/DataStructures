using System;
using System.Collections;
using System.Collections.Generic;

namespace DataStructures.Core.DoublyLinkedList
{
    /// <summary>
    /// A doubly linked list collection which is capable of basic operations such as
    /// Add, Remove, Find and Enumerate
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class DoublyLinkedList<T> : ICollection<T>
    {
        /// <summary>
        /// A pointer to the Head of the doubly linked list.
        /// </summary>                                  
        public DoublyLinkedListNode<T> Head { get; private set; }

        /// <summary>
        /// A pointer to the tail of the doubly linked list.
        /// </summary>
        public DoublyLinkedListNode<T> Tail { get; private set; }

        #region Add
        /// <summary>
        /// Adds the value to the start of the doubly linked list.
        /// </summary>
        /// <param name="value">The value to be added.</param>
        public void AddFirst(T value)
        {
            AddFirst(new DoublyLinkedListNode<T>(value));
        }

        /// <summary>
        /// Adds the node to the start of the doubly linked list.
        /// </summary>
        /// <param name="node">The node to be added.</param>
        public void AddFirst(DoublyLinkedListNode<T> node)
        {
            DoublyLinkedListNode<T> nodeToBeAdded = new DoublyLinkedListNode<T>(node.Value);

            // Save the head so we don't lose it.
            DoublyLinkedListNode<T> temp = Head;

            // Point head to the new node.
            Head = nodeToBeAdded;

            // Point the new node to the rest of the list.
            Head.Next = temp;

            Count++;

            if (Count == 1)
            {
                // If there is only one node then Head and Tail should both point to it.
                Tail = nodeToBeAdded;
            }
            else
            {
                temp.Previous = Head;
            }

            System.Console.WriteLine($"Node added with value {nodeToBeAdded.Value}");
        }

        /// <summary>
        /// Adds the value to the end of the doubly linked list
        /// </summary>
        /// <param name="value">The value to be added.</param>
        public void AddLast(T value)
        {
            AddLast(new DoublyLinkedListNode<T>(value));
        }

        /// <summary>
        /// Adds the node to the end of the doubly linked list.
        /// </summary>
        /// <param name="node">The node to be added.</param>
        public void AddLast(DoublyLinkedListNode<T> node)
        {
            DoublyLinkedListNode<T> nodeToBeAdded = new DoublyLinkedListNode<T>(node.Value);
            
            if (Count == 0)
            {
                Head = nodeToBeAdded;
            }
            else
            {
                Tail.Next = nodeToBeAdded;
                nodeToBeAdded.Previous = Tail;
            }
            Tail = nodeToBeAdded;
            Count++;

            System.Console.WriteLine($"Node added with value {nodeToBeAdded.Value}");
        }
        #endregion

        #region Remove
        /// <summary>
        /// Removes the first node from the list.
        /// </summary>
        public void RemoveFirst()
        {
            if (Count > 0)
            {
                DoublyLinkedListNode<T> temp = Head;

                Head = Head.Next;                

                Count--;

                if (Count == 0)
                    Tail = null;
                else
                    Head.Previous = null;

                System.Console.WriteLine($"Node deleted from first and had value {temp.Value}");
            }
            else
            {
                System.Console.WriteLine($"List is already empty. No node to delete.");
            }
        }

        /// <summary>
        /// Removes the last node from the list.
        /// </summary>
        public void RemoveLast()
        {
            if (Count > 0)
            {
                //Store the Head in a temp variable            
                DoublyLinkedListNode<T> temp = Head;

                if (Count == 1)
                {
                    Head = null;
                    Tail = null;
                }
                else
                {
                    Tail.Previous.Next = null;
                    Tail = Tail.Previous;      
                }

                Count--;

                System.Console.WriteLine($"Node deleted from last with value {temp.Value}");
            }
            else
            {
                System.Console.WriteLine($"List is already empty. No node to delete.");
            }

        }
        #endregion

        #region ICollection

        /// <summary>
        /// The number of items currently in the list.
        /// </summary>
        public int Count
        {
            get;
            private set;
        }

        /// <summary>
        /// True if the collection is read only otherwise false.
        /// </summary>
        public bool IsReadOnly
        {
            get
            {
                return false;
            }
        }

        /// <summary>
        /// Adds the item to the beginning of the list.
        /// </summary>
        /// <param name="item">The item to be added.</param>
        public void Add(T item)
        {
            AddFirst(item);
        }

        /// <summary>
        /// Removes all the nodes from the list.
        /// </summary>
        public void Clear()
        {
            Head = null;
            Tail = null;
            Count = 0;
        }

        /// <summary>
        /// Returns true if the list contains the specified item,
        /// false otherwise.s
        /// </summary>
        /// <param name="item">The item to be searched.</param>
        /// <returns>True if item is found, otherwise false.</returns>
        public bool Contains(T item)
        {
            DoublyLinkedListNode<T> temp = Head;
            while (temp != null)
            {
                if (temp.Value.Equals(item))
                {
                    return true;
                }

                temp = temp.Next;
            }

            return false;
        }


        /// <summary>
        /// Copies the node values in the specified array.
        /// </summary>
        /// <param name="array">The array in which the values are to be copied.</param>
        /// <param name="arrayIndex">The starting index from which to start copy.</param>
        public void CopyTo(T[] array, int arrayIndex)
        {
            DoublyLinkedListNode<T> temp = Head;
            while (temp != null)
            {
                array[arrayIndex++] = temp.Value;
                temp = temp.Next;
            }
        }

        /// <summary>
        /// Enumerates over the doubly linked list from Head to Tail
        /// </summary>
        /// <returns>A Head to Tail Enumerator</returns>
        public IEnumerator<T> GetEnumerator()
        {
            DoublyLinkedListNode<T> current = Head;
            while (current != null)
            {
                yield return current.Value;
                current = current.Next;
            }
        }

        /// <summary>
        /// Removes the first occurrance of the value from the doubly linked list.
        /// </summary>
        /// <param name="item">The value to be removed.</param>
        /// <returns>True if the value is removed, otherwise false.</returns>
        public bool Remove(T item)
        {
            DoublyLinkedListNode<T> previous = null;
            DoublyLinkedListNode<T> current = Head;

            while (current != null)
            {
                if (current.Value.Equals(item))
                {
                    // It is the first node.
                    if (previous == null)
                    {
                        RemoveFirst();
                    }
                    //The node is in middle or end
                    else
                    {
                        previous.Next = current.Next;

                        //The node is last node so update Tail
                        if (current.Next == null)
                        {
                            Tail = previous;
                        }
                        else
                        {
                            current.Next.Previous = previous;
                        }
                        Count--;
                    }
                    return true;
                }
                else
                {
                    previous = current;
                    current = current.Next;
                }
            }

            return false;
        }

        /// <summary>
        /// Enumerates over the doubly linked list from Head to Tail
        /// </summary>
        /// <returns>A Head to Tail Enumerator</returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable<T>)this).GetEnumerator();
        }
        #endregion
    }
}
