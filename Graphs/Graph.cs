using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Graphs
{
    /// <summary>
    /// Класс реализующий графы
    /// </summary>
    public class Graph
    {
        #region Properties
        /// <summary>
        /// Список вершин графа
        /// </summary>
        public List<GraphVertex> GraphVertices { get; set; } = new List<GraphVertex>();
        /// <summary>
        /// Список рёбер графа
        /// </summary>
        public List<GraphVerge> GraphVerges { get; set; } = new List<GraphVerge>();
        /// <summary>
        /// Список пройденных вершин графа для обхода в глубину
        /// </summary>
        public List<GraphVertex> WalkedVerticesList { get; set; } = new List<GraphVertex>();
        /// <summary>
        /// Очередь вершин для обхода в ширину
        /// </summary>
        public Queue<GraphVertex> WalkedVerticesQueue { get; set; } = new Queue<GraphVertex>();
        #endregion
        #region Constructors 
        /// <summary>
        /// Инициализация графа пустыми значениями т.к. поля уже инициализированы
        /// </summary>
        public Graph()
        {

        }
        /// <summary>
        /// Инициализация графа с имеющимся списком вершин <paramref name="graphVertices"/> и рёбер <paramref name="graphVerges"/>
        /// </summary>
        /// <param name="graphVertices">Список вершин графа</param>
        /// <param name="graphVerges">Список рёбер графа</param>
        public Graph(List<GraphVertex> graphVertices, List<GraphVerge> graphVerges)
        {
            GraphVertices = graphVertices;
            GraphVerges = graphVerges;
        }
        /// <summary>
        /// Инициализация графа по матрице смежности из файла <paramref name="_path"/>
        /// </summary>
        /// <param name="_path">Путь к файлу с матрицей смежности</param>
        public Graph(string _path)
        {
            FillGraph(ReadFromFile(_path), LengthFromFile(_path));
        }
        #endregion
        #region Methods
        /// <summary>
        /// Выполняет обход в глубину и отображает на консоли результат
        /// </summary>
        /// <param name="start">Вершина для начала обхода графа</param>
        public void WalkInDepth(GraphVertex start)
        {
            Console.WriteLine("### The walk in depth ###");
            if (!this.GraphVertices.Contains(start))
                throw new Exception("No vertex " + start.ToString() + " in graph");
            // Выполнение обхода в глубину
            DoDepthSteps(start);
            Console.WriteLine("\n### End of walk in depth ###");
        }
        /// <summary>
        /// Рекурсивный обход графа в глубину
        /// </summary>
        /// <param name="start">Вершина для начала обхода графа</param>
        /// <returns>Возвращаемое значение контролирует рекурсию</returns>
        private int DoDepthSteps(GraphVertex start)
        {
            int count = 0;
            if (WalkedVerticesList.Contains(start))
                return 0;
            if (WalkedVerticesList.Any())
                Console.Write("->");
            start.Print();
            WalkedVerticesList.Add(start);
            foreach (var con in start.AdjacentVertexes)
                count += DoDepthSteps(con);
            return count;
        }
        /// <summary>
        /// Выполняет обход в ширину и отображает на консоли результат
        /// </summary>
        /// <param name="start">Вершина для начала обхода графа</param>
        public void WalkInWide(GraphVertex start)
        {
            Console.WriteLine("### The walk in wide ###");
            if (!this.GraphVertices.Contains(start))
                throw new Exception("No vertex " + start.ToString() + " in graph");
            // 
            DoWideSteps(start);
            Console.WriteLine("\n### End of walk in wide ###");
        }
        /// <summary>
        /// Обход графа в ширину с помощью очереди
        /// </summary>
        /// <param name="start">Вершина для начала обхода графа</param>
        private void DoWideSteps(GraphVertex start)
        {
            // Очищаем пройденные вершины и очередь
            WalkedVerticesList.Clear();
            WalkedVerticesQueue.Clear();
            // Добавляем в очередь вершину
            WalkedVerticesQueue.Enqueue(start);
            // Пока в очереди что-то есть выполняем
            while (WalkedVerticesQueue.Any())
            {
                // Освобождаем из очереди вершину и печатаем ее в консоль
                GraphVertex v = WalkedVerticesQueue.Dequeue();
                v.Print();
                // Добавляем в список пройденных вершин
                WalkedVerticesList.Add(v);                
                foreach (var z in v.AdjacentVertexes)
                {
                    // Если смежные вершины не содержатся в очереди или в пройденных вершинах,
                    // то добавляем эту вершину в очередь и позже переходим на печать следующей вершины
                    // Иначе меняем вершину
                    if (WalkedVerticesList.Contains(z) || WalkedVerticesQueue.Contains(z))
                        continue;
                    WalkedVerticesQueue.Enqueue(z);
                }                
                if (WalkedVerticesQueue.Any())
                    Console.Write("->");
            }
        }
        /// <summary>
        /// Из матрицы смежности <paramref name="adjacencyMatrix"></paramref> и кол-ва вершин <paramref name="quantity"/> формируем граф
        /// </summary>
        /// <param name="adjacencyMatrix">Матрица смежности</param>
        /// <param name="quantity">Кол-во вершин</param>
        private void FillGraph(int[,] adjacencyMatrix, int quantity)
        {
            if (adjacencyMatrix == null)
                throw new Exception("Matrix cannot be null");
            if (quantity == 0)
                throw new Exception("Quantity of vertices can't be equal with 0");

            for (int row = 0; row < quantity; row++)
            {
                GraphVertices.Add(new GraphVertex(row + 1));
            }

            for (int col = 0; col < quantity; col++)
            {
                for (int row = 0; row < quantity; row++)
                {
                    if (adjacencyMatrix[col, row] == 1)
                    {
                        GraphVertices.Where(x => x.Name == "x" + (col + 1)).FirstOrDefault()
                            .AdjacentVertexes.Add(GraphVertices.Where(y => y.Name == "x" + (row + 1)).FirstOrDefault());
                    }
                }
            }

            foreach (var graphVertex in GraphVertices)
            {
                foreach (var adjVertex in graphVertex.AdjacentVertexes)
                {
                    if (GraphVerges.Where(t => (t.GraphNodeBegin == graphVertex && t.GraphNodeEnd == adjVertex) || (t.GraphNodeBegin == adjVertex && t.GraphNodeEnd == graphVertex)).Any())
                        continue;
                    GraphVerges.Add(new GraphVerge(graphVertex, adjVertex));
                }
            }

        }
        /// <summary>
        /// Вывод графа на консоль в виде списка смежности
        /// </summary>
        public void Print()
        {
            Console.WriteLine("\n### Adjacent list ###");
            foreach (var graphVertex in GraphVertices)
            {
                Console.Write(graphVertex.ToString() + "-> {");
                for (int i = 0; i < graphVertex.AdjacentVertexes.Count; i++)
                {
                    Console.Write(graphVertex.AdjacentVertexes[i].ToString() + (i != graphVertex.AdjacentVertexes.Count - 1 ? ", " : "}\n"));
                }
            }
            Console.WriteLine("### End of adjacent list ###\n");

        }
        /// <summary>
        /// Вывод на консоль матрицы смежности <paramref name="adjacencyMatrix"/> из файла по пути <paramref name="_path"/> с кол-вом вершин <paramref name="length"/>
        /// </summary>
        /// <param name="_path">Путь к файлу с матрицей смежности</param>
        /// <param name="adjacencyMatrix">Матрица смежности</param>
        /// <param name="length">Кол-во вершин графа, длина массива из матрицы</param>
        public void Print(string _path, int[,] adjacencyMatrix = null, int length = 0)
        {
            if (string.IsNullOrEmpty(_path))
                throw new Exception("FilePath can't be null or empty");
            if (adjacencyMatrix == null)
                adjacencyMatrix = ReadFromFile(_path);
            if (length == 0)
                length = LengthFromFile(_path);

            Console.WriteLine("### Adjacent matrix ###");
            for (int i = 0; i < length; i++)
            {
                for (int j = 0; j < length; j++)
                {
                    Console.Write(adjacencyMatrix[i, j] + " ");
                }
                Console.WriteLine();
            }
            Console.WriteLine("### End of adjacent matrix ###");
        }
        /// <summary>
        /// Построчное чтение из файла
        /// </summary>
        /// <param name="filePath">Путь к фалу</param>
        /// <returns>Матрицу смежности считанную из файла</returns>
        private int[,] ReadFromFile(string filePath)
        {
            if (string.IsNullOrEmpty(filePath))
                throw new Exception("FilePath can't be null or empty");
            string[] allLinesFromFile = File.ReadAllLines(filePath);
            return ToArray(allLinesFromFile);
        }
        /// <summary>
        /// Находит кол-во вершин графа, равное длине строки из матрицы смежности
        /// </summary>
        /// <param name="filePath">Путь к файлу, где записана матрица смежности</param>
        /// <returns>Количество вершин графа</returns>
        private int LengthFromFile(string filePath)
        {
            if (string.IsNullOrEmpty(filePath))
                throw new Exception("FilePath can't be null or empty");
            string[] allLinesFromFile = File.ReadAllLines(filePath);
            return allLinesFromFile.Length;
        }
        /// <summary>
        /// Привидение к типу int[,]
        /// </summary>
        /// <param name="allLinesFromFile">Массив строк считанных из файла</param>
        /// <returns>Матрицу смежности в виде матрицы int[,]</returns>
        private int[,] ToArray(string[] allLinesFromFile)
        {
            var length = allLinesFromFile.Length;
            var adjacencyMatrix = new int[length, length];
            for (int i = 0; i < length; i++)
            {
                var cLine = allLinesFromFile[i].ToCharArray();
                var k = 0;
                for (int j = 0; j < length; j++, k++)
                {
                    if (cLine[k] == '1' || cLine[k] == '0')
                        adjacencyMatrix[i, j] = cLine[k] == '1' ? 1 : 0;
                }
            }
            return adjacencyMatrix;
        }
        /// <summary>
        /// Выполняет поиск наименьшего доминирующего покрытия
        /// </summary>
        /// <returns></returns>
        public List<GraphVertex> FindMinimumDominantSet()
        {
            var result = new List<GraphVertex>();

            return result;
        }
        /// <summary>
        /// Выполняет поиск наименьшего вершинного покрытия (полный перебор)
        /// </summary>
        private List<GraphVertex> FindMinimumVertexCoversBruteForce()
        {
            var n = GraphVertices.Count;
            var rows = (int)Math.Pow(2, n);
            var boolTable = new byte[rows, n];
            //заполнение таблицы нулями и единицами
            FillBoolTable(ref boolTable, 0, n, rows);
            //все вершинные покрытия
            var allCovers = new List<Tuple<int, int>>();
            for (int i = 0; i < rows; i++)
            {
                var covers = new byte[GraphVerges.Count];
                for (int j = 0; j < n; j++)
                {
                    var vertex = GraphVertices.Where(x => x.Number == j + 1).First();
                    if (boolTable[i, j] == 1)
                    {
                        foreach (var v in GraphVertices)
                        {
                            if (v.AdjacentVertexes.Where(x => x == vertex).Any())
                            {
                                //ребро которое соединяется с данной вершиной
                                var edge = GraphVerges.Where(x => (x.GraphNodeBegin == v && x.GraphNodeEnd == vertex) || (x.GraphNodeBegin == vertex && x.GraphNodeEnd == v)).First();
                                var index = GraphVerges.IndexOf(edge);
                                covers[index] = 1;
                            }
                        }
                    }
                }
                //если покрываются все ребра
                if (covers.Sum(x => x) == GraphVerges.Count)
                {
                    var sum = 0;
                    for (int j = 0; j < n; j++)
                    {
                        if (boolTable[i, j] == 1)
                            sum++;
                    }
                    allCovers.Add(new Tuple<int, int>(sum, i));
                }
            }
            var result = allCovers.OrderBy(x => x.Item1).First();
            List<GraphVertex> data = new List<GraphVertex>();
            for (int i = 0; i < n; i++)
            {
                if (boolTable[result.Item2, i] == 1)
                    data.Add(GraphVertices[i]);
            }
            return data;
        }
        private void FillBoolTable(ref byte[,] data, int index, int n, int rows)
        {
            int realIndex = n - index - 1;
            if (realIndex >= n || realIndex < 0)
                return;
            int r = 0;
            while (r < rows)
            {
                for (byte i = 0; i <= 1; i++)
                {
                    int limit = (int)Math.Pow(2, index);
                    for (int j = 0; j < limit; j++, r++)
                    {
                        data[r, realIndex] = i;
                    }
                }
            }
            FillBoolTable(ref data, ++index, n, rows);
        }
        #endregion
    }
}
