using System;
using System.Collections;
using System.Collections.Generic;

namespace DataStructures.Core.BinarySearchTree
{
    /// <summary>
    /// Represents a Binary Search Tree
    /// </summary>
    /// <typeparam name="T">The type of data to be stored.</typeparam>
    public class BinarySearchTree<T> : IEnumerable<T>
        where T : IComparable<T>
    {
        private BinaryTreeNode<T> _head;
        private int _count;

        public BinaryTreeNode<T> Root
        {
            get { return _head; }
        }

        #region Add

        /// <summary>
        /// Adds a new value to the binary tree.
        /// </summary>
        /// <param name="value">The value to be added.</param>
        public void Add(T value)
        {
            //CASE 1: The tree is empty - allocate the head to the value.
            if (_head == null)
            {
                _head = new BinaryTreeNode<T>(value);
            }
            //CASE 2: The tree is not empty so find the right location to insert the value.
            else
            {
                IterativeAddTo(value);
            }

            //Update the current number of nodes
            _count++;
        }

        public void Add(BinaryTreeNode<T> node)
        {
            if (node is null)
                return;

            if (_head is null)
                _head = node;
            else
                AddTo(_head, node);
            _count++;
        }

        private void AddTo(BinaryTreeNode<T> node, BinaryTreeNode<T> nodeToAdd)
        {
            if (node is null || nodeToAdd is null)
                return;

            if (nodeToAdd.Value.CompareTo(node.Value) < 0)
            {
                if (node.Left is null)
                    node.Left = nodeToAdd;
                else
                    AddTo(node.Left, nodeToAdd);
            }
            else
            {
                if (node.Right is null)
                    node.Right = nodeToAdd;
                else
                    AddTo(node.Right, nodeToAdd);
            }
        }

        /// <summary>
        /// Recursive Add algorithm
        /// </summary>
        /// <param name="node">The root node of the sub tree.</param>
        /// <param name="value">The value to be inserted.</param>
        private void AddTo(BinaryTreeNode<T> node, T value)
        {
            //CASE 1: The value is less than the current node value
            if (value.CompareTo(node.Value) < 0)
            {
                //There is no left child of current node, so add it.
                if (node.Left == null)
                {
                    node.Left = new BinaryTreeNode<T>(value);
                }
                //There is a left child of current node, so go to that child and repeat the procedure.
                else
                {
                    AddTo(node.Left, value);
                }
            }
            //CASE 2: The value is greater than or equal to the current value.
            else
            {
                if (node.Right == null)
                {
                    node.Right = new BinaryTreeNode<T>(value);
                }
                else
                {
                    AddTo(node.Right, value);
                }
            }
        }

        private void IterativeAddTo(T value)
        {
            BinaryTreeNode<T> nodeToAdd = new BinaryTreeNode<T>(value);

            var current = _head;

            while (true)
            {
                if (value.CompareTo(current.Value) < 0)
                {
                    if (current.Left is null)
                    {
                        current.Left = nodeToAdd;
                        return;
                    }
                    else
                    {
                        current = current.Left;
                    }
                }
                else
                {
                    if (current.Right is null)
                    {
                        current.Right = nodeToAdd;
                        return;
                    }
                    else
                    {
                        current = current.Right;
                    }
                }
            }
        }

        #endregion Add

        /// <summary>
        /// Checks if the specified value exists in the binary tree or not.
        /// </summary>
        /// <param name="value">The value to be checked.</param>
        /// <returns>True if value exists otherwise false.</returns>
        public bool Contains(T value)
        {
            BinaryTreeNode<T> parent;
            return FindWithParent(value, out parent) != null;
        }

        /// <summary>
        /// Finds and returns the first node containing the specified value. If the value is not found
        /// then it returns null. It also returns the parent of the found node (or null) which is used in Remove.
        /// </summary>
        /// <param name="value">The value to be searched.</param>
        /// <param name="parent">The parent of the found node.</param>
        /// <returns>The found node (or null).</returns>
        private BinaryTreeNode<T> FindWithParent(T value, out BinaryTreeNode<T> parent)
        {
            BinaryTreeNode<T> current = _head;
            parent = null;

            while (current != null)
            {
                int result = current.CompareTo(value);
                if (result > 0)
                {
                    //If value is less than current, go Left.
                    parent = current;
                    current = current.Left;
                }
                else if (result < 0)
                {
                    //If value is greater than current, go Right.
                    parent = current;
                    current = current.Right;
                }
                else
                {
                    //We have a match.
                    break;
                }
            }

            return current;
        }

        #region Remove

        /// <summary>
        /// Removes the first node with the given value.
        /// </summary>
        /// <param name="value">The value to be removed.</param>
        /// <returns>True if the node is removed otherwise false.</returns>
        public bool Remove(T value)
        {
            BinaryTreeNode<T> current, parent;

            current = FindWithParent(value, out parent);

            if (current == null)
            {
                return false;
            }

            _count--;

            //CASE 1: The current as no right child
            if (current.Right == null)
            {
                if (parent == null)
                {
                    _head = current.Left;
                }
                else
                {
                    int result = parent.CompareTo(value);

                    if (result > 0)
                    {
                        parent.Left = current.Left;
                    }
                    else if (result < 0)
                    {
                        parent.Right = current.Left;
                    }
                }
            }
            //CASE 2: If current's right child has no left child, then current's right child replaces current.
            else if (current.Right.Left == null)
            {
                //The new parent should point to the left sub tree of current.
                current.Right.Left = current.Left;

                if (parent == null)
                {
                    _head = current.Right;
                }
                else
                {
                    int result = parent.CompareTo(value);

                    if (result > 0)
                    {
                        parent.Left = current.Right;
                    }
                    else if (result < 0)
                    {
                        parent.Right = current.Right;
                    }
                }
            }
            //CASE 3: If current's right child has a left child, replace current with current's right child's
            //left most child
            else
            {
                BinaryTreeNode<T> leftMost = current.Right.Left;
                BinaryTreeNode<T> leftMostParent = current.Right;

                while (leftMost.Left != null)
                {
                    leftMostParent = leftMost;
                    leftMost = leftMost.Left;
                }

                leftMostParent.Left = leftMost.Right;

                leftMost.Left = current.Left;
                leftMost.Right = current.Right;

                if (parent == null)
                {
                    _head = leftMost;
                }
                else
                {
                    int result = parent.CompareTo(value);

                    if (result > 0)
                    {
                        parent.Left = leftMost;
                    }
                    else if (result < 0)
                    {
                        parent.Right = leftMost;
                    }
                }
            }

            return true;
        }

        #endregion Remove

        #region Traversal

        public enum TraversalType
        {
            PreOrder,
            PostOrder,
            InOrder,
            LevelOrder
        }

        public void Traverse(Action<T> action, TraversalType traversalType)
        {
            Traverse(action, traversalType, _head);
        }

        public void Traverse(Action<T> action, TraversalType traversalType, BinaryTreeNode<T> node)
        {
            if (node != null)
            {
                if (traversalType == TraversalType.PreOrder)
                {
                    action(node.Value);
                    Traverse(action, traversalType, node.Left);
                    Traverse(action, traversalType, node.Right);
                }
                else if (traversalType == TraversalType.PostOrder)
                {
                    Traverse(action, traversalType, node.Left);
                    Traverse(action, traversalType, node.Right);
                    action(node.Value);
                }
                else if (traversalType == TraversalType.InOrder)
                {
                    Traverse(action, traversalType, node.Left);
                    action(node.Value);
                    Traverse(action, traversalType, node.Right);
                }
                else
                {
                    LevelOrderTraversal(action);
                }
            }
        }

        /// <summary>
        /// Enumerates the values contained in the binary tree in in-order traversal order.
        /// </summary>
        /// <returns>The enumerator</returns>
        public IEnumerator<T> InOrderTraversal()
        {
            if (_head != null)
            {
                Stack<BinaryTreeNode<T>> stack = new Stack<BinaryTreeNode<T>>();

                BinaryTreeNode<T> current = _head;

                bool goLeftNext = true;

                while (current != null)
                {
                    if (goLeftNext)
                    {
                        while (current.Left != null)
                        {
                            stack.Push(current);
                            current = current.Left;
                        }
                    }

                    yield return current.Value;

                    if (current.Right != null)
                    {
                        current = current.Right;

                        goLeftNext = true;
                    }
                    else
                    {
                        if (stack.Count == 0)
                            current = null;
                        else
                            current = stack.Pop();
                        goLeftNext = false;
                    }
                }
            }
        }

        #endregion Traversal

        /// <summary>
        /// Removes all the nodes from the tree
        /// </summary>
        public void Clear()
        {
            _count = 0;
            _head = null;
        }

        /// <summary>
        /// The number of items currently in the tree/
        /// </summary>
        public int Count
        {
            get { return _count; }
        }

        public IEnumerator<T> GetEnumerator()
        {
            return InOrderTraversal();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public T GetMinimumValue()
        {
            var current = _head;

            while (current.Left != null)
            {
                current = current.Left;
            }
            return current.Value;
        }

        public T GetMaximumValue()
        {
            var current = _head;

            while (current.Right != null)
            {
                current = current.Right;
            }
            return current.Value;
        }

        private void LevelOrderTraversal(Action<T> action)
        {
            Queue<BinaryTreeNode<T>> queue = new Queue<BinaryTreeNode<T>>();

            if (_head is null)
            {
                return;
            }

            queue.Enqueue(_head);

            while (queue.Count != 0)
            {
                var node = queue.Dequeue();

                action(node.Value);

                if (node.Left != null)
                    queue.Enqueue(node.Left);
                if (node.Right != null)
                    queue.Enqueue(node.Right);
            }
        }

        /// <summary>
        /// Finds the value of least common ancestor node between 2 given nodes
        /// </summary>
        /// <remarks>
        /// Assumes that the node exists in the tree.
        /// </remarks>
        /// <param name="node1">The first node</param>
        /// <param name="node2">The second node</param>
        /// <returns>The value of the least common ancestor</returns>
        public T LeastCommonAncestor(BinaryTreeNode<T> node1, BinaryTreeNode<T> node2)
        {
            var node = FindLCA(_head, node1, node2);
            if (node is null)
            {
                return default;
            }
            return node.Value;
        }

        /// <summary>
        /// Recursively finds the least common ancestor for 2 given nodes
        /// </summary>
        /// <param name="currentNode">The current node in the tree</param>
        /// <param name="node1">The first node</param>
        /// <param name="node2">The second node</param>
        /// <returns>The least common ancestor node</returns>
        private BinaryTreeNode<T> FindLCA(BinaryTreeNode<T> currentNode, BinaryTreeNode<T> node1, BinaryTreeNode<T> node2)
        {
            if (currentNode == null)
                return null;

            if (currentNode == node1 || currentNode == node2)
                return currentNode;

            var leftLCA = FindLCA(currentNode.Left, node1, node2);
            var rightLCA = FindLCA(currentNode.Right, node1, node2);

            if (leftLCA != null && rightLCA != null)
                return currentNode;

            return leftLCA
                ?? rightLCA;
        }
    }
}
