using System;
using System.Collections.Generic;
using System.Linq;

namespace Graphs
{
    /// <summary>
    /// Реализация алгоритма Брона-Кербоша по нахождению наибольшего внутренне устойчивого подмножества 
    /// </summary>
    public class BronKerbosh
    {
        #region Properties
        Graph graph;
        /// <summary>
        /// Наибольшие внутренее устойчивые подмножества
        /// </summary>
        public List<List<GraphVertex>> Result { get; private set; } = new List<List<GraphVertex>>();
        /// <summary>
        /// Множество вершин, которые могут увеличить S
        /// </summary>
        public List<GraphVertex> QPlus { get; set; } = new List<GraphVertex>();
        /// <summary>
        /// Множество S
        /// </summary>
        public List<GraphVertex> S { get; set; } = new List<GraphVertex>();
        /// <summary>
        /// Множество вершин, которые уже использовались для расширения S на предыдущих шагах алгоритма
        /// </summary>
        public List<GraphVertex> QMinus { get; set; } = new List<GraphVertex>();
        #endregion
        #region Constructors
        /// <summary>
        /// Добавляет вершины в множество вершин, способных расширить НВУП
        /// </summary>
        /// <param name="graph">Неориентированный граф</param>
        public BronKerbosh(Graph graph)
        {
            if (graph == null)
                throw new ArgumentNullException("Граф не может быть пустым");
            this.graph = graph;
            QPlus = graph.GraphVertices;
        }
        #endregion

        public void Print()
        {
            Console.WriteLine("### Internally Stable Set ###");
            Console.WriteLine(this.ToString());
            Console.WriteLine("### End Internally Stable Set ###");
        }

        public override string ToString()
        {
            string result = "{ ";
            foreach(var internallyStableSet in Result)
            {
                foreach (var set in internallyStableSet)
                {
                    result += set.ToString() + (internallyStableSet.Any() ? ", ": "");
                }
            }
            return result + "}";
        }

        public void Run(List<GraphVertex> qP = null, List<GraphVertex> qM = null)
        {
            if(qP == null || qM == null)
            {
                qP = QPlus;
                qM = QMinus;
            }
            while (qP.Any() && !qP.Where(x=> qM.Contains(x)).Any())
            {
                var s = qM;
                s.Add(qP.FirstOrDefault());
                foreach(var v in s)
                {
                    foreach(var aV in v.AdjacentVertexes)
                    {
                        if(qM.Any())
                            qM.Remove(aV);
                        qP.Remove(aV);
                    }
                    qP.Remove(v);
                }
                var qPlus = qP;
                var qMinus = qM;
                if (!(qPlus.Any() && qMinus.Any()))
                {
                    Result.Add(s);
                    return;
                }
                else
                {
                    Run(qPlus, qMinus);
                }
            }
        }


    }
}
