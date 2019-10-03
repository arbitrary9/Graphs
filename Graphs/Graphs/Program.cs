using System;

namespace Graphs
{
    class Program
    {
        const string _matrixpath = @"F:\USM Study\Course 2\Алгоритмы графов\Лабораторная 1\Graphs\Graphs\bin\Debug\graph19.txt";
        static void Main(string[] args)
        {
            var graph = new Graph(_matrixpath);
            graph.Print(_matrixpath);
            graph.Print();
        }
    }
}
