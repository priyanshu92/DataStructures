namespace DataStructures.Core.DoublyLinkedList
{
    /// <summary>
    /// A node in the doubly linked list.
    /// </summary>
    /// <typeparam name="T">The type of the list.</typeparam>
    public class DoublyLinkedListNode<T>
    {
        /// <summary>
        /// Constructs a new node with specified value
        /// </summary>
        public DoublyLinkedListNode(T value)
        {
            Value = value;
        }


        /// <summary>
        /// The node value
        /// </summary>
        public T Value { get; set; }

        /// <summary>
        /// Pointer to next node.
        /// </summary>
        public DoublyLinkedListNode<T> Next { get; set; }

        /// <summary>
        /// Pointer to previous node.
        /// </summary>
        public DoublyLinkedListNode<T> Previous { get; set; }
    }
}
