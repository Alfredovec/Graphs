using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GraphAlgorhitms.Graphonium.Models;
using GraphAlgorhitms.Infrastructure;

namespace GraphAlgorhitms.Graphonium.Tools
{
    public static class GraphUtils
    {
        public static Graph Get(string text)
        {
            var graph = new Graph();

            var lines = text.Split(new[] { Globals.LineSeparator }, StringSplitOptions.RemoveEmptyEntries);

            var metadata = lines[0].Split(Globals.Space);
            var vertexesCount = int.Parse(metadata[0]);
            var edgesCount = int.Parse(metadata[1]);

            for (var i = 0; i < vertexesCount; i++)
            {
                graph.Vertexes.Add(new Vertex() { Number = i + 1 });
            }

            for (var i = 1; i < edgesCount + 1; i++)
            {
                var edgeInfo = lines[i].Split(Globals.Space).Select(int.Parse).ToArray();
                var vertexBegin = graph.Vertexes.Single(v => v.Number == edgeInfo[0]);
                var vertexEnd = graph.Vertexes.Single(v => v.Number == edgeInfo[1]);

                var edge = new Edge()
                {
                    VertexBegin = vertexBegin,
                    VertexEnd = vertexEnd,
                    Weight = edgeInfo[2]
                };

                graph.Edges.Add(edge);
                vertexBegin.Edges.Add(edge);
                vertexEnd.Edges.Add(edge);
            }

            return graph;
        }
    }
}
