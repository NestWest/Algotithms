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
        public Graph<int> ConvertIntGrid(int[,] grid)
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
        }
    }
}