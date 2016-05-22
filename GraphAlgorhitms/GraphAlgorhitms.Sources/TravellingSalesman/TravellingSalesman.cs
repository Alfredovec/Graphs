using System;
using System.Collections.Generic;
using System.Linq;
using GraphAlgorhitms.Graphonium.Models;

namespace GraphAlgorhitms.Sources.TravellingSalesman
{
    public class TravellingSalesman
    {
        List<Tuple<int, int>> _result = new List<Tuple<int, int>>();
        private double[,] matrix;

        public int[] GetMinPath(Graph initGraph)
        {
            InitMatrix(initGraph);

            while (_result.Count < initGraph.Vertexes.Count)
            {
                RowMinimize();
                ColumnMinimize();
                var elementToDelete = CalculatePenalties();
                _result.Add(elementToDelete);
                RemoveItem(elementToDelete);
            }

            _result = _result.Select(t => new Tuple<int,int>(t.Item1 + 1, t.Item2 + 1))
                .OrderBy(r => r.Item1)
                .ToList();

            var firstVertex = _result.First().Item1;
            var current = firstVertex;
            var intResult = new int[_result.Count + 1];
            intResult[0] = current;
            for (var i = 1; i < _result.Count + 1; i++)
            {
                var tuple = _result.Single(t => t.Item1 == current);
                current = tuple.Item2;
                intResult[i] = tuple.Item2;
            }

            return intResult;
        }

        private void RemoveItem(Tuple<int, int> elementToDelete)
        {
            for (var k = 0; k < matrix.GetLength(0); k++)
            {
                matrix[elementToDelete.Item1, k] = double.PositiveInfinity;
            }

            for (var k = 0; k < matrix.GetLength(1); k++)
            {
                matrix[k, elementToDelete.Item2] = double.PositiveInfinity;
            }

            matrix[elementToDelete.Item2, elementToDelete.Item1] = double.PositiveInfinity;
        }

        private void InitMatrix(Graph initGraph)
        {
            var vertexes = initGraph.Vertexes;
            var vertexesCount = vertexes.Count;
            matrix = new double[vertexesCount, vertexesCount];

            foreach (var vertex in vertexes)
            {
                foreach (var edge in vertex.Edges)
                {
                    var secondVertex = edge.VertexBegin != vertex ? edge.VertexBegin : edge.VertexEnd;
                    matrix[vertex.Number - 1, secondVertex.Number - 1] = edge.Weight;
                }
            }

            for (var i = 0; i < matrix.GetLength(0); i++)
            {
                for (var j = 0; j < matrix.GetLength(1); j++)
                {
                    // ReSharper disable once CompareOfFloatsByEqualityOperator
                    if (matrix[i, j] == 0)
                    {
                        matrix[i, j] = double.PositiveInfinity;
                    }
                }
            }
        }

        private void RowMinimize()
        {
            for (var i = 0; i < matrix.GetLength(0); i++)
            {
                if (_result.All(t => t.Item1 != i))
                {
                    var min = double.PositiveInfinity;
                    for (var j = 0; j < matrix.GetLength(1); j++)
                    {
                        if (min > matrix[i, j])
                        {
                            min = matrix[i, j];
                        }
                    }

                    for (var j = 0; j < matrix.GetLength(1); j++)
                    {
                        matrix[i, j] -= min;
                    }
                }
            }
        }

        private void ColumnMinimize()
        {
            for (var i = 0; i < matrix.GetLength(0); i++)
            {
                if (_result.All(t => t.Item2 != i))
                {
                    var min = double.PositiveInfinity;
                    for (var j = 0; j < matrix.GetLength(1); j++)
                    {
                        if (min > matrix[j, i])
                        {
                            min = matrix[j, i];
                        }
                    }

                    for (var j = 0; j < matrix.GetLength(1); j++)
                    {
                        matrix[j, i] -= min;
                    }
                }
            }
        }

        private Tuple<int, int> CalculatePenalties()
        {
            var maxRow = -1;
            var maxColumn = -1;
            var maxSum = -1.0;
            var minElement = double.PositiveInfinity;

            for (var i = 0; i < matrix.GetLength(0); i++)
            {
                for (var j = 0; j < matrix.GetLength(1); j++)
                {
                    if (!double.IsPositiveInfinity(matrix[i, j]) && _result.All(t => t.Item1 != i) && _result.All(t => t.Item2 != j))
                    {
                        var minRow = double.PositiveInfinity;
                        for (var k = 0; k < matrix.GetLength(0); k++)
                        {
                            if (k != j && matrix[i, k] < minRow)
                            {
                                minRow = matrix[i, k];
                            }
                        }

                        var minColumn = double.PositiveInfinity;
                        for (var k = 0; k < matrix.GetLength(1); k++)
                        {
                            if (k != i && matrix[k, j] < minColumn)
                            {
                                minColumn = matrix[k, j];
                            }
                        }

                        if (minColumn + minRow > maxSum && 
                            !ElementCreatesLoop(i, j) &&
                            matrix[i, j] < minElement)
                        {
                            maxSum = minColumn + minRow;
                            maxRow = i;
                            maxColumn = j;
                            minElement = matrix[i, j];
                        }
                    }
                }
            }

            var result = new Tuple<int, int>(maxRow, maxColumn);

            return result;
        }

        private bool ElementCreatesLoop(int i, int j)
        {
            var origin = _result.SingleOrDefault(t => t.Item1 == j);

            if (origin == null)
            {
                return false;
            }

            for (var k = 0; k < matrix.GetLength(0) - 2 && origin != null; k++)
            {
                if (origin.Item2 == i)
                {
                    return true;
                }

                origin = _result.SingleOrDefault(t => t.Item1 == origin.Item2);
            }

            return false;
        }
    }
}
