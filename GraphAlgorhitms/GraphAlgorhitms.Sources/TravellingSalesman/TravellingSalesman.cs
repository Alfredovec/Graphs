using System;
using System.Collections.Generic;
using System.Linq;
using GraphAlgorhitms.Graphonium.Models;

namespace GraphAlgorhitms.Sources.TravellingSalesman
{
    public class TravellingSalesman
    {
        public int[] GetMinPath(Graph initGraph)
        {
            var initMatrix = GetMatrix(initGraph);
            var result = new List<Tuple<int, int>>();

            while (result.Count < initGraph.Vertexes.Count)
            {
                RowMinimize(ref initMatrix);
                ColumnMinimize(ref initMatrix);
                var elementToDelete = CalculatePenalties(ref initMatrix);
                result.Add(elementToDelete);
                RemoveItem(elementToDelete, ref initMatrix);
            }

            result = result.Select(t => new Tuple<int,int>(t.Item1 + 1, t.Item2 + 1))
                .OrderBy(r => r.Item1)
                .ToList();

            var firstVertex = result.First().Item1;
            var current = firstVertex;
            var intResult = new int[result.Count + 1];
            intResult[0] = current;
            for (var i = 1; i < result.Count + 1; i++)
            {
                var tuple = result.Single(t => t.Item1 == current);
                current = tuple.Item2;
                intResult[i] = tuple.Item2;
            }

            return intResult;
        }

        private void RemoveItem(Tuple<int, int> elementToDelete, ref double[,] matrix)
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

        private double[,] GetMatrix(Graph initGraph)
        {
            var vertexes = initGraph.Vertexes;
            var vertexesCount = vertexes.Count;
            var matrix = new double[vertexesCount, vertexesCount];

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

            return matrix;
        }

        private void RowMinimize(ref double[,] matrix)
        {
            for (var i = 0; i < matrix.GetLength(0); i++)
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

        private void ColumnMinimize(ref double[,] matrix)
        {
            for (var i = 0; i < matrix.GetLength(0); i++)
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

        private Tuple<int, int> CalculatePenalties(ref double[,] matrix)
        {
            var maxRow = 0;
            var maxColumn = 0;
            var maxSum = 0.0;

            for (var i = 0; i < matrix.GetLength(0); i++)
            {
                for (var j = 0; j < matrix.GetLength(1); j++)
                {
                    if (matrix[i, j] == 0)
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
                        for (var k = 0; k < matrix.GetLength(0); k++)
                        {
                            if (k != i && matrix[k, j] < minColumn)
                            {
                                minColumn = matrix[k, j];
                            }
                        }

                        if (minColumn + minRow > maxSum)
                        {
                            maxSum = minColumn + minRow;
                            maxRow = i;
                            maxColumn = j;
                        }
                    }
                }
            }

            var result = new Tuple<int, int>(maxRow, maxColumn);

            return result;
        }
    }
}
