using System;
using System.Collections.Generic;
using System.Text;

namespace Graphs
{
    public class GraphVertex
    {
        public int Number { get; set; }
        public string Name { get; set; }
        public List<GraphVertex> AdjacentVertexes { get; set; }
        public GraphVertex()
        {
            Number = Int32.MinValue;
            Name = null;
            AdjacentVertexes = null;
        }
        public GraphVertex(int number)
        {
            Number = number;
            Name = "x" + number;
            AdjacentVertexes = new List<GraphVertex>();
        }
    }
}
