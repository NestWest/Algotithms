using System;
using System.Security.Cryptography;
namespace Algoritms
{
    public class Program
    {
        public static void Main(string[] args)
        {

        }
    }
    /*public class Node<T>
    {
        public T Data { get; set; }
        public List<Node<T>> Neighbors { get; set; }
        public Node(T data)
        {
            Data = data;
            Neighbors = new List<Node<T>>();
        }
    }*/
    public class Graph<T>
    {
        private List<T> nodes;
        private List<List<T>> edges;
        public Graph()
        {

        }
        /// <summary>
        /// Adds the data for a certain node into the graph, appending it to the graph and the node.
        /// </summary>
        /// <param name="node"></param>
        public void AddNode(T node)
        {
            List<T> list = new List<T>();
            nodes.Add(node);
            edges.Add(list);
        }
        public void AddEdge(T node1, T node2)
        {
            if (nodes.Contains(node1) && nodes.Contains(node2))
            {
                int i = nodes.IndexOf(node1);
                int j = nodes.IndexOf(node2);
                if (edges[i].Contains(node2))
                {
                    throw new Exception($"Node already contained within {nodes}");
                }
                else
                {
                    edges[i].Add(node2);
                    edges[j].Add(node1);
                    for (int k = 0; k < edges.Count(); k++)
                    {
                        edges[k].Sort();
                    }
                }
            }
            else
            {
                throw new Exception($"Nodes not found in {nodes}");
            }
        }
        /// <summary>
        /// Converts the grid into a connected graph based on integer values in the grid.
        /// 1 is a wall, and all other numbers are ignored as empty space that are still 
        /// regarded as connected to the graph
        /// </summary>
        /// <param name="grid"></param>
        /// <returns></returns>
        /*public Graph<int> ConvertIntGrid(int[,] grid)
        {
            for (int i = 0; i < grid.GetLength(0); i++)
            {
                for (int j = 0; j < grid.GetLength(1); j++)
                {
                    if (i == 0)
                    {
                        if (j == 0)
                        {

                        }
                    }
                    if (j == 0)
                    {

                    }
                    if (i > 0 && j > 0)
                    {

                    }
                }
            }
        }*/
        public List<T> GetNeighbors(T node)
        {
            if (nodes.Contains(node))
            {
                int index = nodes.IndexOf(node);
                return edges[index];
            }
            else
            {
                throw new Exception($"Node {node} not found in the graph.");
            }
        }
    }
    public class Searches
    {
        public static List<T> DepthFirstSearch<T>(Graph<T> graph, T startNode)
        {
            List<T> visited = new List<T>();
            Stack<T> stack = new Stack<T>();
            stack.Push(startNode);
            while (stack.Count > 0)
            {
                T currentNode = stack.Pop();
                if (!visited.Contains(currentNode))
                {
                    visited.Add(currentNode);
                    // Assuming you have a method to get neighbors of the current node
                    foreach (T neighbor in graph.GetNeighbors(currentNode))
                    {
                        stack.Push(neighbor);
                    }
                }
            }
            return visited;
        }
        public static List<T> BreadthFirstSearch<T>(Graph<T> graph, T startNode)
        {
            List<T> visited = new List<T>();
            Queue<T> queue = new Queue<T>();
            queue.Enqueue(startNode);
            while (queue.Count > 0)
            {
                T currentNode = queue.Dequeue();
                if (!visited.Contains(currentNode))
                {
                    visited.Add(currentNode);
                    // Assuming you have a method to get neighbors of the current node
                    foreach (T neighbor in graph.GetNeighbors(currentNode))
                    {
                        queue.Enqueue(neighbor);
                    }
                }
            }
            return visited;
        }

    }
    public class Sorts
    {
        public static void BubbleSort<T>(List<T> list) where T : IComparable<T>
        {
            int n = list.Count;
            for (int i = 0; i < n - 1; i++)
            {
                for (int j = 0; j < n - i - 1; j++)
                {
                    if (list[j].CompareTo(list[j + 1]) > 0)
                    {
                        // Swap list[j] and list[j + 1]
                        T temp = list[j];
                        list[j] = list[j + 1];
                        list[j + 1] = temp;
                    }
                }
            }
        }
        public static void InsertionSort<T>(List<T> list) where T : IComparable<T>
        {
            int n = list.Count;
            for (int i = 1; i < n; ++i)
            {
                T key = list[i];
                int j = i - 1;
                // Move elements of list[0..i-1], that are greater than key,
                // to one position ahead of their current position
                while (j >= 0 && list[j].CompareTo(key) > 0)
                {
                    list[j + 1] = list[j];
                    j = j - 1;
                }
                list[j + 1] = key;
            }
        }
        public static void MergeSort<T>(List<T> list) where T : IComparable<T>
        {
            if (list.Count <= 1)
                return;
            int mid = list.Count / 2;
            List<T> left = new List<T>(list.GetRange(0, mid));
            List<T> right = new List<T>(list.GetRange(mid, list.Count - mid));
            MergeSort(left);
            MergeSort(right);
            Merge(list, left, right);
        }
        private static void Merge<T>(List<T> list, List<T> left, List<T> right) where T : IComparable<T>
        {
            int i = 0, j = 0, k = 0;
            while (i < left.Count && j < right.Count)
            {
                if (left[i].CompareTo(right[j]) <= 0)
                {
                    list[k] = left[i];
                    i++;
                }
                else
                {
                    list[k] = right[j];
                    j++;
                }
                k++;
            }
            while (i < left.Count)
            {
                list[k] = left[i];
                i++;
                k++;
            }
            while (j < right.Count)
            {
                list[k] = right[j];
                j++;
                k++;
            }
        }
    }
}