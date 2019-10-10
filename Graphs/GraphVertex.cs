using System;
using System.Collections.Generic;
using System.Text;

namespace Graphs
{
    /// <summary>
    /// Вершина графа
    /// </summary>
    public class GraphVertex
    {
        #region Properties
        public int Number { get; set; }
        public string Name { get; set; }
        public List<GraphVertex> AdjacentVertexes { get; set; }
        #endregion
        #region Constructors
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
        #endregion
        #region Overrides
        public override string ToString()
        {
            return Name;
        }
        #endregion
        #region Methods
        public void Print()
        {
            Console.Write(this.ToString());
        }
        #endregion
    }
}
