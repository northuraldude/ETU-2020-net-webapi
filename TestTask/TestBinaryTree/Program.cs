/**
 * Дисциплина: Основы разработки корпоративных приложений на платформе .NET
 * Тема: Тестовое задание #2 - Бинарное дерево (без стандартных деревьев)
 * Разработал: Белоусов Евгений
 * Группа: 6305
 */

using System;

namespace TestBinaryTree
{
    public class Node
    {
        public int Value;
        public Node Left;
        public Node Right;
    }

    public class BinaryTree
    {
        private Node _root;

        public BinaryTree()
        {
            _root = null;
        }
        
        public void InsertNode(int key)
        {
            if (_root != null)
                InsertNode(key, _root);
            else
                _root = new Node
                {
                    Value = key, 
                    Left = null, 
                    Right = null
                };
        }
        
        private void InsertNode(int key, Node leaf)
        {
            if (leaf == null) 
                throw new ArgumentNullException(nameof(leaf));
            
            while (true)
            {
                if (key < leaf.Value)
                {
                    if (leaf.Left != null)
                    {
                        leaf = leaf.Left;
                        continue;
                    }

                    leaf.Left = new Node
                    {
                        Value = key,
                        Left = null,
                        Right = null
                    };
                }
                else if (key >= leaf.Value)
                {
                    if (leaf.Right != null)
                    {
                        leaf = leaf.Right;
                        continue;
                    }

                    leaf.Right = new Node
                    {
                        Value = key,
                        Right = null,
                        Left = null
                    };
                }

                break;
            }
        }

        public Node SearchNode(int key)
        {
            return SearchNode(key, _root);
        }
        
        private static Node SearchNode(int key, Node leaf)
        {
            while (true)
            {
                if (leaf != null)
                {
                    if (key == leaf.Value)
                        return leaf;
                    
                    leaf = key < leaf.Value ? leaf.Left : leaf.Right;
                }
                else
                {
                    return null;
                }
            }
        }

        public void RemoveNode(int key)
        {
            RemoveNode(_root, SearchNode(key, _root));
        }
        
        private static Node RemoveNode(Node root, Node removableNode)
        {
            if (root == null)
                return null;

            if (removableNode.Value < root.Value)
                root.Left = RemoveNode(root.Left, removableNode);

            if (removableNode.Value > root.Value)
                root.Right = RemoveNode(root.Right, removableNode);

            if (removableNode.Value != root.Value) 
                return root;
            
            switch (root.Left)
            {
                case null when root.Right == null:
                {
                    return null;
                }
                case null:
                {
                    root = root.Right;
                    break;
                }
                default:
                {
                    if (root.Right == null)
                    {
                        root = root.Left;
                    }
                    else
                    {
                        var minimalNode = GetMinimalNode(root.Right);
                        root.Value = minimalNode.Value;
                        root.Right = RemoveNode(root.Right, minimalNode);
                    }

                    break;
                }
            }

            return root;
        }
        
        private static Node GetMinimalNode(Node currentNode)
        {
            while (currentNode?.Left != null)
                currentNode = currentNode.Left;

            return currentNode;
        }

        public void PreOrderTravers()
        {
            PreOrderTravers(_root);
            Console.WriteLine("");
        }

        private static void PreOrderTravers(Node leaf)
        {
            while (true)
            {
                if (leaf == null)
                    return;
                Console.WriteLine("{0}", leaf.Value);
                PreOrderTravers(leaf.Left);
                leaf = leaf.Right;
            }
        }
        
        public void InOrderTravers()
        {
            InOrderTravers(_root);
            Console.WriteLine("");
        }

        private static void InOrderTravers(Node leaf)
        {
            while (true)
            {
                if (leaf != null)
                {
                    InOrderTravers(leaf.Left);
                    Console.WriteLine("{0}", leaf.Value);
                    leaf = leaf.Right;
                    continue;
                }

                break;
            }
        }
        
        public void PostOrderTravers()
        {
            PostOrderTravers(_root);
            Console.WriteLine("");
        }

        private static void PostOrderTravers(Node leaf)
        {
            if (leaf == null) 
                return;
            PostOrderTravers(leaf.Left);
            PostOrderTravers(leaf.Right);
            Console.WriteLine("{0}", leaf.Value);
        }
    }

    public class Program
    {
        public static void Main(string[] args)
        {
            var tree = new BinaryTree();
            
            while (true)
            {
                Console.WriteLine("What you going to do?");
                Console.WriteLine("Enter the number:");
                Console.WriteLine("1 - Insert;");
                Console.WriteLine("2 - Remove;");
                Console.WriteLine("3 - Search;"); 
                Console.WriteLine("4 - Pre-order travers;"); 
                Console.WriteLine("5 - In order travers;"); 
                Console.WriteLine("6 - Post-order travers;");
                Console.WriteLine("0 - Exit.");
                
                var data = "";
                switch (Convert.ToInt32(Console.ReadLine()))
                {
                    case 1:
                        Console.WriteLine("Enter the data:");
                        data = Console.ReadLine();
                        tree.InsertNode(Convert.ToInt32(data));
                        break;
                    case 2:
                        Console.WriteLine("Enter the data:");
                        data = Console.ReadLine();
                        tree.RemoveNode(Convert.ToInt32(data));
                        break;
                    case 3:
                        Console.WriteLine("Enter the data:");
                        data = Console.ReadLine();
                        var temp = tree.SearchNode(Convert.ToInt32(data));
                        Console.WriteLine(temp != null ? "Found!" : "Not found!");
                        break;
                    case 4:
                        tree.PreOrderTravers();
                        break;
                    case 5:
                        tree.InOrderTravers();
                        break;
                    case 6:
                        tree.PostOrderTravers();
                        break;
                    case 0:
                        return;
                    default:
                        Console.WriteLine("Pick the correct number!");
                        break;
                }
            }
        }
    }
}