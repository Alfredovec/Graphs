using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GraphAlgorhitms.Graphonium.Models;
using GraphAlgorhitms.Infrastructure;

namespace GraphAlgorhitms.Sources.ShortestWay.Floyd
{
    public class Floyd
    {
        public double[,] GetWeightMatrix(Graph initGraph)
        {
            var vertexes = initGraph.Vertexes;
            var vertexesCount = vertexes.Count;
            var matrixWeight = new double[vertexesCount, vertexesCount];
            var matrixHistory = new double[vertexesCount, vertexesCount];

            FillMatrix(ref matrixWeight);
            foreach (var vertex in vertexes)
            {
                foreach (var edge in vertex.Edges)
                {
                    var secondVertex = edge.VertexBegin != vertex ? edge.VertexBegin : edge.VertexEnd;
                    matrixWeight[vertex.Number - 1, secondVertex.Number - 1] = edge.Weight;
                    matrixHistory[vertex.Number - 1, secondVertex.Number - 1] = edge.VertexBegin.Number;
                }
            }

            foreach (var vertex1 in vertexes)
            {
                foreach (var vertex2 in vertexes)
                {
                    if (!double.IsPositiveInfinity(matrixWeight[vertex1.Number - 1, vertex2.Number - 1]))
                    {
                        foreach (var vertex3 in vertexes)
                        {
                            if (matrixWeight[vertex1.Number - 1, vertex3.Number - 1] >
                                    matrixWeight[vertex1.Number - 1, vertex2.Number - 1] +
                                    matrixWeight[vertex2.Number - 1, vertex3.Number - 1])
                            {
                                matrixWeight[vertex1.Number - 1, vertex3.Number - 1] =
                                    matrixWeight[vertex1.Number - 1, vertex2.Number - 1] +
                                    matrixWeight[vertex2.Number - 1, vertex3.Number - 1];

                                matrixHistory[vertex1.Number - 1, vertex3.Number - 1] =
                                    matrixHistory[vertex1.Number - 1, vertex2.Number - 1];
                            }
                        }
                    }
                }
            }

            return matrixWeight;
        }

        private void FillMatrix(ref double[,] matrix)
        {
            for (var i = 0; i < matrix.GetLength(0); i++)
            {
                for (var j = 0; j < matrix.GetLength(1); j++)
                {
                    matrix[i, j] = i == j ? 0 : double.PositiveInfinity;
                }
            }
        }
    }
}
