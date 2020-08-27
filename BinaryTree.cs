using System;
using System.Collections.Generic;
using System.Drawing.Text;
using System.Windows.Forms;

namespace AutomaticCreationTreeMathematicalExpression
{
    class Node
    {
        public Node LeftNode { get; set; }
        public Node RightNode { get; set; }
        public string Data { get; set; }
    }
    class BinaryTree
    {
        public Node Root { get; set; }

        public int GetNodesAmount()
        {
            return nodesAmount;
        }

        private int nodesAmount = 1;

        public bool Add(string value)
        {
            Node before = null, after = this.Root;

            while (after != null)
            {
                before = after;
                if (String.Compare(value, after.Data) < 0) //Is new node in left tree? 
                    after = after.LeftNode;
                else if (String.Compare(value, after.Data) > 0) //Is new node in right tree?
                    after = after.RightNode;
                else
                {
                    //Exist same value
                    return false;
                }
            }

            Node newNode = new Node();
            newNode.Data = value;

            if (this.Root == null)//Tree ise empty
                this.Root = newNode;
            else
            {
                if (String.Compare(value, before.Data) < 0)
                    before.LeftNode = newNode;
                else
                    before.RightNode = newNode;
            }

            nodesAmount++;
            return true;
        }

        public Node Find(string value)
        {
            return this.Find(value, this.Root);
        }

        public void Remove(string value)
        {
            this.Root = Remove(this.Root, value);
        }

        private Node Remove(Node parent, string key)
        {
            if (parent == null) return parent;

            if (String.Compare(key, parent.Data) < 0) parent.LeftNode = Remove(parent.LeftNode, key);
            else if (String.Compare(key, parent.Data) > 0)
                parent.RightNode = Remove(parent.RightNode, key);

            // if value is same as parent's value, then this is the node to be deleted  
            else
            {
                // node with only one child or no child  
                if (parent.LeftNode == null)
                    return parent.RightNode;
                else if (parent.RightNode == null)
                    return parent.LeftNode;

                // node with two children: Get the inorder successor (smallest in the right subtree)  
                parent.Data = MinValue(parent.RightNode);

                // Delete the inorder successor  
                parent.RightNode = Remove(parent.RightNode, parent.Data);
            }

            nodesAmount--;
            return parent;
        }

        private string MinValue(Node node)
        {
            string minv = node.Data;

            while (node.LeftNode != null)
            {
                minv = node.LeftNode.Data;
                node = node.LeftNode;
            }

            return minv;
        }

        private Node Find(string value, Node parent)
        {
            if (parent != null)
            {
                if (value == parent.Data) return parent;
                if (String.Compare(value, parent.Data) < 0)
                    return Find(value, parent.LeftNode);
                else
                    return Find(value, parent.RightNode);
            }

            return null;
        }

        public int GetTreeDepth()
        {
            return this.GetTreeDepth(this.Root);
        }

        private int GetTreeDepth(Node parent)
        {
            return parent == null ? 0 : Math.Max(GetTreeDepth(parent.LeftNode), GetTreeDepth(parent.RightNode)) + 1;
        }

        public string TraversePreOrder(Node parent)
        {
            if (parent != null)
            {
                string expression = parent.Data + " ";
                expression += TraversePreOrder(parent.LeftNode);
                expression += TraversePreOrder(parent.RightNode);

                return expression;
            }
            else
            {
                return "";
            }
        }

        public string TraverseInOrder(Node parent)
        {
            if (parent != null)
            {
                string expression = TraverseInOrder(parent.LeftNode);
                expression += " ";
                expression += TraverseInOrder(parent.RightNode);

                return expression;
            }
            else
            {
                return "";
            }
        }

        public string TraversePostOrder(Node parent)
        {
            if (parent != null)
            {
                string expression = TraversePostOrder(parent.LeftNode);
                expression += TraversePostOrder(parent.RightNode);
                expression += parent.Data + " ";

                return expression;
            }
            else
            {
                return "";
            }
        }

        public TreeNode[] GetTreeNodes()
        {
            TreeNode[] treeNodes = new TreeNode[nodesAmount];

            return treeNodes;
        }
    }
}
