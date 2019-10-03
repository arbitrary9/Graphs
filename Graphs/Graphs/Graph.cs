using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Graphs
{
    public class Graph
    {
        public List<GraphVertex> GraphVertexes { get; set; }
        public List<GraphVerge> GraphVerges { get; set; }

        public Graph()
        {
            GraphVertexes = new List<GraphVertex>();
            GraphVerges = new List<GraphVerge>();
        }

        public Graph(List<GraphVertex> graphVertices, List<GraphVerge> graphVerges)
        {
            GraphVertexes = graphVertices;
            GraphVerges = graphVerges;
        }

        public Graph(string _path)
        {
            GetGraph(ReadFromFile(_path));
        }

        public int[,] ReadFromFile(string filePath)
        {
            string[] allLinesFromFile =  File.ReadAllLines(filePath);
            return ToArray(allLinesFromFile);
        }

        private int[,] ToArray(string[] allLinesFromFile)
        {
            var length = allLinesFromFile.Length;
            var adjacencyMatrix = new int[length,length];
            foreach(var line in allLinesFromFile)
            {
                var k = 0;
                for (int i = 0; i < length; i++)
                {
                    for (int j = 0; j < length; j++)
                    {
                        adjacencyMatrix[i, j] = line[k++];
                    }
                }
            }
            return adjacencyMatrix;
        }        
        private Graph GetGraph(int[,] adjacencyMatrix)
        {
            var graph = new Graph();
            for (int i = 0; i < adjacencyMatrix.Length; i++)
            {
                var vX = new GraphVertex(i + 1);
                graph.GraphVertexes.Add(vX);
                for (int j = 0; j < adjacencyMatrix.Length; j++)
                {
                    if (i >= j)
                        continue;
                    var vY = new GraphVertex(j + 1);
                    graph.GraphVertexes.Add(vY);
                    if (adjacencyMatrix[i, j] == 1)
                        vX.AdjacentVertexes.Add(vY);
                }
            }
            return graph;
        }
    }
}
