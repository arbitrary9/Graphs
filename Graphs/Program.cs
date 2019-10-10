using System;
using System.Collections.Generic;
using System.Linq;

namespace Graphs
{
    class Program
    {
        const string _matrixpath = @"F:\USM Study\Course 2\Алгоритмы графов\Лабораторная 1\Graphs\Graphs\bin\Debug\graph19.txt";
        static void Main(string[] args)
        {
            var graph = new Graph(_matrixpath);
            graph.Print();
            //graph.Print(_matrixpath);
            var pointName = StartPoint(graph.GraphVertices);
            graph.WalkInDepth(graph.GraphVertices.Where(x=>x.Name == pointName).FirstOrDefault());
            pointName = StartPoint(graph.GraphVertices);
            graph.WalkInWide(graph.GraphVertices.Where(x => x.Name == pointName).FirstOrDefault());
            var bronKerbosh = new BronKerbosh(graph);
            bronKerbosh.Run();
            Console.WriteLine();
            bronKerbosh.Print();
        }

        private static string StartPoint(List<GraphVertex> vertexes)
        {
            Console.Write("\nStart point to walk ->");
            var input = Convert.ToString(Console.ReadLine());
            if (!vertexes.Where(x => x.Name == input).Any())
                return StartPoint(vertexes);
            return Convert.ToString(input);
        }
    }
}
