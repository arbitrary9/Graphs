using System;
using System.Collections.Generic;
using System.Text;

namespace Graphs      
{
    /// <summary>
    /// Ребро графа
    /// </summary>
    public class GraphVerge
    {
        public GraphVertex GraphNodeBegin { get; set; }
        public GraphVertex GraphNodeEnd { get; set; }

        public GraphVerge()
        {
            GraphNodeBegin = new GraphVertex();
            GraphNodeEnd = new GraphVertex();
        }
        public GraphVerge(GraphVertex x1, GraphVertex x2)
        {
            GraphNodeBegin = x1;
            GraphNodeEnd = x2;
        }
    }
}
