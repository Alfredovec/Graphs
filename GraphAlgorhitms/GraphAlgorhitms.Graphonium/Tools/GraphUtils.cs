using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GraphAlgorhitms.Graphonium.Models;
using GraphAlgorhitms.Infrastructure;

namespace GraphAlgorhitms.Graphonium.Tools
{
    public static class GraphUtils
    {
        public static Graph GetFromFile(string text)
        {
            var random = new Random();
            var graph = new Graph();

            var lines = text.Split(new[] { Globals.LineSeparator }, StringSplitOptions.RemoveEmptyEntries);

            var metadata = lines[0].Split(new [] { Globals.Space }, StringSplitOptions.RemoveEmptyEntries);
            var vertexesCount = int.Parse(metadata[0]);
            var edgesCount = int.Parse(metadata[1]);

            for (var i = 0; i < vertexesCount; i++)
            {
                graph.Vertexes.Add(new Vertex() { Number = i + 1 });
            }

            for (var i = 1; i < edgesCount + 1; i++)
            {
                var edgeInfo = lines[i].Split(new []{ Globals.Space }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(int.Parse).ToArray();
                var vertexBegin = graph.Vertexes.Single(v => v.Number == edgeInfo[0]);
                var vertexEnd = graph.Vertexes.Single(v => v.Number == edgeInfo[1]);

                var edge = new Edge()
                {
                    VertexBegin = vertexBegin,
                    VertexEnd = vertexEnd,
                    Weight = edgeInfo[2]
                };

                if (!graph.Edges.Any(e => e.VertexBegin == edge.VertexBegin && e.VertexEnd == edge.VertexEnd ||
                                          e.VertexBegin == edge.VertexEnd && e.VertexEnd == edge.VertexBegin))
                {
                    graph.Edges.Add(edge);
                    vertexBegin.Edges.Add(edge);
                    vertexEnd.Edges.Add(edge);
                }
                else
                {
                    var d = "duplicate";
                }
            }

            for (int i = 0; i < graph.Vertexes.Count; i++)
            {
                var firstVertex = graph.Vertexes.ElementAt(i);
                for (int j = 0; j < graph.Vertexes.Count; j++)
                {
                    var secondVertex = graph.Vertexes.ElementAt(j);
                    if (i != j && !graph.Edges.Any(e => e.VertexBegin == firstVertex && e.VertexEnd == secondVertex ||
                                             e.VertexEnd == firstVertex && e.VertexBegin == secondVertex))
                    {
                        var edge = new Edge()
                        {
                            Weight = 10000,
                            VertexBegin = firstVertex,
                            VertexEnd = secondVertex
                        };

                        graph.Edges.Add(edge);
                        firstVertex.Edges.Add(edge);
                        secondVertex.Edges.Add(edge);
                    }
                }
            }

            return graph;
        }

        public static Graph Get(string text)
        {
            const int dimesion = 20;
            var random = new Random();
            var graph = new Graph();

            for (var i = 0; i < dimesion; i++)
            {
                graph.Vertexes.Add(new Vertex()
                {
                    Number = i + 1
                });
            }

            for (var i = 0; i < dimesion; i++)
            {
                var firstVertex = graph.Vertexes.Single(v => v.Number == i + 1);
                for (var j = i; j < dimesion; j++)
                {
                    if (i != j)
                    {
                        var secondVertex = graph.Vertexes.Single(v => v.Number == j + 1);
                        var edge = new Edge()
                        {
                            VertexBegin = firstVertex,
                            VertexEnd = secondVertex,
                            Weight = random.Next(1, 10)
                        };

                        graph.Edges.Add(edge);
                        firstVertex.Edges.Add(edge);
                        secondVertex.Edges.Add(edge);
                    }
                }
            }

            return graph;
        }
    }
}
