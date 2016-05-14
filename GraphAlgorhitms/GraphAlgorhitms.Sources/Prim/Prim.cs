using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using GraphAlgorhitms.Graphonium.Models;
using GraphAlgorhitms.Infrastructure;

namespace GraphAlgorhitms.Sources.Prim
{
    public class Prim
    {
        public Graph GetMinOst(Graph initGraph)
        {
            var result = new Graph()
            {
                Vertexes = initGraph.Vertexes
                                .Select(v => new Vertex() { Number = v.Number })
                                .ToList()
            };

            var firstVertex = result.Vertexes.First();
            var includedVertexNumbers = new List<int>() { firstVertex.Number };

            while (result.Edges.Count != result.Vertexes.Count - 1)
            {
                var allAvailableEdges = includedVertexNumbers
                    .SelectMany(num => initGraph
                                        .Vertexes
                                        .Single(vr => vr.Number == num).Edges);

                var minEdge = allAvailableEdges
                    .Where(e => !(includedVertexNumbers.Contains(e.VertexBegin.Number) && 
                                  includedVertexNumbers.Contains(e.VertexEnd.Number)))
                    .MinBy(e => e.Weight);

                result.Edges.Add(minEdge);
                includedVertexNumbers = result.Vertexes
                        .Where(
                            v => result.Edges.Select(e => e.VertexBegin)
                                .Union(result.Edges.Select(e => e.VertexEnd)).Any(ver => ver.Number == v.Number))
                        .Select(v => v.Number)
                        .ToList();
            }

            return result;
        }
    }
}
