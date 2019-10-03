using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Graphs
{

    public class Graph
    {
        #region Properties
        private List<GraphVertex> GraphVertexes { get; set; } = new List<GraphVertex>();
        private List<GraphVerge> GraphVerges { get; set; } = new List<GraphVerge>();
        #endregion
        #region Constructors 
        public Graph()
        {
        }
        public Graph(List<GraphVertex> graphVertices, List<GraphVerge> graphVerges)
        {
            GraphVertexes = graphVertices;
            GraphVerges = graphVerges;
        }
        public Graph(string _path)
        {
            FillGraph(ReadFromFile(_path), SizeFromFile(_path));            
        }

        private void FillGraph(int[,] adjacencyMatrix, int length)
        {
            for (int row = 0; row < length; row++)
                GraphVertexes.Add(new GraphVertex(row + 1));
            
            for (int col = 0; col < length; col++)
            {
                for (int row = 0; row < length; row++)
                {
                    if (adjacencyMatrix[col, row] == 1)
                    {
                        GraphVertexes.Where(x => x.Name == "x" + (col + 1)).FirstOrDefault()
                            .AdjacentVertexes.Add(GraphVertexes.Where(y => y.Name == "x" + (row + 1)).FirstOrDefault());
                    }
                }
            }          
        }
        #endregion
        #region Methods
        public void Print()
        {
            Console.WriteLine("\n### Adjacent list ###");
            foreach (var graphVertex in GraphVertexes)
            {
                Console.Write(graphVertex.ToString() + "-> {");
                for(int i = 0; i < graphVertex.AdjacentVertexes.Count; i++)
                {
                    Console.Write(graphVertex.AdjacentVertexes[i].ToString() + (i != graphVertex.AdjacentVertexes.Count - 1 ? ", " : "}\n"));
                }
            }
            Console.WriteLine("### End of adjacent list ###\n");

        }
        public void Print(string _path, int[,] adjacencyMatrix = null, int size = 0)
        {
            if (adjacencyMatrix == null)
                adjacencyMatrix = ReadFromFile(_path);
            if (size == 0)
                size = SizeFromFile(_path);

            Console.WriteLine("### Adjacent matrix ###");
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    Console.Write(adjacencyMatrix[i, j] + " ");
                }
                Console.WriteLine();
            }
            Console.WriteLine("### End of adjacent matrix ###");
        }
        private int[,] ReadFromFile(string filePath)
        {
            string[] allLinesFromFile =  File.ReadAllLines(filePath);
            return ToArray(allLinesFromFile);
        }
        private int SizeFromFile(string filePath)
        {
            string[] allLinesFromFile = File.ReadAllLines(filePath);
            return allLinesFromFile.Length;
        }
        private int[,] ToArray(string[] allLinesFromFile)
        {
            var length = allLinesFromFile.Length;
            var adjacencyMatrix = new int[length,length];
                for (int i = 0; i < length; i++)
                {
                    var cLine = allLinesFromFile[i].ToCharArray();
                    var k = 0;
                    for (int j = 0; j < length; j++, k++)
                    {
                        if(cLine[k] == '1' || cLine[k] == '0')
                            adjacencyMatrix[i, j] = cLine[k] == '1' ? 1 : 0;
                    }
                }            
            return adjacencyMatrix;
        }
        #endregion
    }
}
