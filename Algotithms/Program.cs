using System;
using System.Security.Cryptography;
namespace Algoritms
{
    public class Program
    {
        public static void Main(string[] args)
        {
            GridOptimisation gridOptimisation = new GridOptimisation();
            gridOptimisation.GenerateGrid();
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
    // The following algoritms have been filled in with Intellecode's assistance, and are not guaranteed to be correct or optimal.
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
    public class Pathfinding
    {
        public static List<T> Dijkstra<T>(Graph<T> graph, T startNode, T endNode)
        {
            Dictionary<T, int> distances = new Dictionary<T, int>();
            Dictionary<T, T> previousNodes = new Dictionary<T, T>();
            List<T> unvisitedNodes = new List<T>();
            foreach (T node in graph.GetNeighbors(startNode))
            {
                distances[node] = int.MaxValue;
                previousNodes[node] = default(T);
                unvisitedNodes.Add(node);
            }
            distances[startNode] = 0;
            while (unvisitedNodes.Count > 0)
            {
                T currentNode = GetClosestNode(distances, unvisitedNodes);
                unvisitedNodes.Remove(currentNode);
                if (currentNode.Equals(endNode))
                    break;
                foreach (T neighbor in graph.GetNeighbors(currentNode))
                {
                    int altDistance = distances[currentNode] + 1; // Assuming all edges have weight 1
                    if (altDistance < distances[neighbor])
                    {
                        distances[neighbor] = altDistance;
                        previousNodes[neighbor] = currentNode;
                    }
                }
            }
            return ConstructPath(previousNodes, startNode, endNode);
        }
        private static T GetClosestNode<T>(Dictionary<T, int> distances, List<T> unvisitedNodes)
        {
            T closestNode = default(T);
            int closestDistance = int.MaxValue;
            foreach (T node in unvisitedNodes)
            {
                if (distances[node] < closestDistance)
                {
                    closestDistance = distances[node];
                    closestNode = node;
                }
            }
            return closestNode;
        }
        private static List<T> ConstructPath<T>(Dictionary<T, T> previousNodes, T startNode, T endNode)
        {
            List<T> path = new List<T>();
            T currentNode = endNode;
            while (!currentNode.Equals(startNode))
            {
                path.Add(currentNode);
                currentNode = previousNodes[currentNode];
            }
            path.Add(startNode);
            path.Reverse();
            return path;
        }
        public static List<T> AStar<T>(Graph<T> graph, T startNode, T endNode, Func<T, T, int> heuristic)
        {
            Dictionary<T, int> gScores = new Dictionary<T, int>();
            Dictionary<T, int> fScores = new Dictionary<T, int>();
            Dictionary<T, T> cameFrom = new Dictionary<T, T>();
            List<T> openSet = new List<T> { startNode };
            gScores[startNode] = 0;
            fScores[startNode] = heuristic(startNode, endNode);
            while (openSet.Count > 0)
            {
                T currentNode = GetLowestFScoreNode(fScores, openSet);
                if (currentNode.Equals(endNode))
                    return ReconstructPath(cameFrom, currentNode);
                openSet.Remove(currentNode);
                foreach (T neighbor in graph.GetNeighbors(currentNode))
                {
                    int tentativeGScore = gScores[currentNode] + 1; // Assuming all edges have weight 1
                    if (!gScores.ContainsKey(neighbor) || tentativeGScore < gScores[neighbor])
                    {
                        cameFrom[neighbor] = currentNode;
                        gScores[neighbor] = tentativeGScore;
                        fScores[neighbor] = gScores[neighbor] + heuristic(neighbor, endNode);
                        if (!openSet.Contains(neighbor))
                            openSet.Add(neighbor);
                    }
                }
            }
            return new List<T>(); // Return an empty path if no path is found
        }
        private static T GetLowestFScoreNode<T>(Dictionary<T, int> fScores, List<T> openSet)
        {
            T lowestNode = default(T);
            int lowestScore = int.MaxValue;
            foreach (T node in openSet)
            {
                if (fScores[node] < lowestScore)
                {
                    lowestScore = fScores[node];
                    lowestNode = node;
                }
            }
            return lowestNode;
        }
        private static List<T> ReconstructPath<T>(Dictionary<T, T> cameFrom, T currentNode)
        {
            List<T> totalPath = new List<T> { currentNode };
            while (cameFrom.ContainsKey(currentNode))
            {
                currentNode = cameFrom[currentNode];
                totalPath.Add(currentNode);
            }
            totalPath.Reverse();
            return totalPath;
        }

    }
    //Written for platformer optimisation using random variables instead of procedural generation with noise
    public class GridOptimisation
    {
        public int[,] grid = new int[100, 100];
        private double targetFillPercentage = 0.4; // Target fill percentage 40% +- 0.05%
        Random rnd = new Random();
        private int fillCount;
        private double fillPercentage;
        private int emptyCount;
        private double emptyPercentage;
        public void GenerateGrid()
        {
            for (int y = 0; y < 100; y++)
            {
                for (int x = 0; x < 100; x++)
                {
                    grid[x, y] = rnd.Next(0, 2);
                    if (grid[x, y] == 1)
                    {
                        fillCount++;
                    }
                    else
                    {
                        emptyCount++;
                    }
                }
            }
            fillPercentage = (double)fillCount / (fillCount + emptyCount) * 100;
            emptyPercentage = (double)emptyCount / (fillCount + emptyCount) * 100;
            Console.WriteLine($"Fill %: {fillPercentage:F2}");
            Console.WriteLine($"Empty %: {emptyPercentage:F2}");
            if (fillPercentage < targetFillPercentage * 100)
            {

            }
            else if (fillPercentage > targetFillPercentage * 100)
            {

            }
        }
    }
}