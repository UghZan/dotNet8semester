using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tree
{
    public class TreeNode
    {
        public int data;

        //для упрощения процедуры удаления
        public TreeNode parent;

        public TreeNode left;
        public TreeNode right;

        public TreeNode(int _data)
        {
            data = _data;
        }
        public bool Compare(int c)
        {
            return c > data ? true : false;
        }
        public void Add(int _data)
        {
            if (Compare(_data))
            {
                if (right == null)
                {
                    right = new TreeNode(_data);
                    right.parent = this;
                }
                else
                {
                    right.Add(_data);
                }
            }
            else
            {
                if (left == null)
                {
                    left = new TreeNode(_data);
                    left.parent = this;
                }
                else
                {
                    left.Add(_data);
                }
            }
        }
        public TreeNode Find(int _data)
        {
            if (data == _data)
            {
                return this;
            }
            else
            {
                if (Compare(_data))
                {
                    if (right == null)
                        return null;
                    else
                        return right.Find(_data);
                }
                else
                {
                    if (left == null)
                        return null;
                    else
                        return left.Find(_data);
                }
            }
        }
        public bool Remove(int _data)
        {
            if (data == _data)
            {
                if (left == null && right == null)
                {
                    if (parent.Compare(data))
                        parent.right = null;
                    else
                        parent.left = null;
                }
                else if (left != null && right == null)
                {
                    if (parent.Compare(data))
                        parent.right = left;
                    else
                        parent.left = left;
                }
                else if (right != null && left == null)
                {
                    if (parent.Compare(data))
                        parent.right = right;
                    else
                        parent.left = right;
                }
                else
                {
                    TreeNode newNode = right.FindMinLeftNode();
                    data = newNode.data;
                    newNode.Remove(newNode.data);
                }
                return true;
            }
            else
            {
                if (Compare(_data))
                {
                    if (right == null)
                        return false;
                    else
                        right.Remove(_data);
                }
                else
                {
                    if (left == null)
                        return false;
                    else
                        left.Remove(_data);
                }
            }
            return false;
        }
        public TreeNode FindMinLeftNode()
        {
            TreeNode iterator = this;
            while (iterator.left != null)
            {
                iterator = iterator.left;
            }
            return iterator;
        }
    }
    //хороший вывод деревьев от умных людей
    //источник:
    //https://stackoverflow.com/questions/36311991/c-sharp-display-a-binary-search-tree-in-console
    public static class BTreePrinter
    {
        class NodeInfo
        {
            public TreeNode Node;
            public string Text;
            public int StartPos;
            public int Size { get { return Text.Length; } }
            public int EndPos { get { return StartPos + Size; } set { StartPos = value - Size; } }
            public NodeInfo Parent, Left, Right;
        }

        public static void Print(this TreeNode root, string textFormat = "0", int spacing = 1, int topMargin = 2, int leftMargin = 2)
        {
            if (root == null) return;
            int rootTop = Console.CursorTop + topMargin;
            var last = new List<NodeInfo>();
            var next = root;
            for (int level = 0; next != null; level++)
            {
                var item = new NodeInfo { Node = next, Text = next.data.ToString(textFormat) };
                if (level < last.Count)
                {
                    item.StartPos = last[level].EndPos + spacing;
                    last[level] = item;
                }
                else
                {
                    item.StartPos = leftMargin;
                    last.Add(item);
                }
                if (level > 0)
                {
                    item.Parent = last[level - 1];
                    if (next == item.Parent.Node.left)
                    {
                        item.Parent.Left = item;
                        item.EndPos = Math.Max(item.EndPos, item.Parent.StartPos - 1);
                    }
                    else
                    {
                        item.Parent.Right = item;
                        item.StartPos = Math.Max(item.StartPos, item.Parent.EndPos + 1);
                    }
                }
                next = next.left ?? next.right;
                for (; next == null; item = item.Parent)
                {
                    int top = rootTop + 2 * level;
                    Print(item.Text, top, item.StartPos);
                    if (item.Left != null)
                    {
                        Print("/", top + 1, item.Left.EndPos);
                        Print("_", top, item.Left.EndPos + 1, item.StartPos);
                    }
                    if (item.Right != null)
                    {
                        Print("_", top, item.EndPos, item.Right.StartPos - 1);
                        Print("\\", top + 1, item.Right.StartPos - 1);
                    }
                    if (--level < 0) break;
                    if (item == item.Parent.Left)
                    {
                        item.Parent.StartPos = item.EndPos + 1;
                        next = item.Parent.Node.right;
                    }
                    else
                    {
                        if (item.Parent.Left == null)
                            item.Parent.EndPos = item.StartPos - 1;
                        else
                            item.Parent.StartPos += (item.StartPos - 1 - item.Parent.EndPos) / 2;
                    }
                }
            }
            Console.SetCursorPosition(0, rootTop + 2 * last.Count - 1);
        }

        private static void Print(string s, int top, int left, int right = -1)
        {
            Console.SetCursorPosition(left, top);
            if (right < 0) right = left + s.Length;
            while (Console.CursorLeft < right) Console.Write(s);
        }
    }
    public class BinarySearchTree
    {
        public TreeNode root;
        int count;
        public int Count() { return count; }
        public void Add(int data)
        {
            if(root == null)
            {
                root = new TreeNode(data);
                return;
            }
            else
            {
                root.Add(data);
            }
            count++;
        }

        public TreeNode Find(int data)
        {
            if(root == null)
            {
                return null;
            }
            else
            {
                return root.Find(data);
            }
        }

        public void Remove(int data)
        {
            if(root == null)
            {
                return;
            }
            else
            {
                if (root.Remove(data))
                    count--;
            }
        }

    }
    class Program
    {
        static void Main(string[] args)
        {
            BinarySearchTree tree = new BinarySearchTree();
            Random r = new Random();
            for (int i = 0; i < 32; i++)
            {
                tree.Add(r.Next(100));
            }
            tree.Add(22);
            BTreePrinter.Print(tree.root);

            if (tree.Find(4) != null)
                Console.WriteLine("4 is in the tree");
            else
                Console.WriteLine("4 isn't in the tree");
            if (tree.Find(16) != null)
                Console.WriteLine("16 is in the tree");
            else
                Console.WriteLine("16 isn't in the tree");
            if (tree.Find(1) != null)
                Console.WriteLine("1 is in the tree");
            else
                Console.WriteLine("1 isn't in the tree");

            tree.Remove(4);
            tree.Remove(16);
            tree.Remove(22);
            tree.Remove(1);
            BTreePrinter.Print(tree.root);
        }
    }
}
