using System.Collections.Generic;
using System.Linq;
using GraphAlgorhitms.Graphonium.Models;
using GraphAlgorhitms.Infrastructure;

namespace GraphAlgorhitms.Sources.MinOstTree.Kruskal
{
    public class Kruskal
    {
        public Graph GetMinOst(Graph initGraph)
        {
            var result = new Graph() { Vertexes = initGraph.Vertexes };
            result.Vertexes.ForEach(v => v.Edges = new List<Edge>());

            var orderedEdges = initGraph.Edges.OrderBy(e => e.Weight).ToList();

            for (var i = 0; i < orderedEdges.Count && result.Edges.Count < result.Vertexes.Count - 1 ; i++)
            {
                var edge = orderedEdges[i];
                var fromVertex = result.Vertexes.Single(v => v.Number == edge.VertexBegin.Number);
                var toVertex = result.Vertexes.Single(v => v.Number == edge.VertexEnd.Number);

                result.Edges.Add(edge);
                fromVertex.Edges.Add(edge);
                toVertex.Edges.Add(edge);

                if (AnyLoop(result))
                {
                    result.Edges.Remove(edge);
                    fromVertex.Edges.Remove(edge);
                    toVertex.Edges.Remove(edge);
                }
            }

            return result;
        }

        private bool AnyLoop(Graph graph)
        {
            var visitedVertexes = new List<Vertex>();
            var visitedEdges = new List<Edge>();
            var result = false;

            while (graph.Edges.Count > visitedEdges.Count)
            {
                var vertex = graph.Edges.First(e => !visitedEdges.Contains(e)).VertexBegin;

                result |= SubtreeHasLoops(vertex, ref visitedVertexes, ref visitedEdges);
            }

            return result;
        }

        private bool SubtreeHasLoops(Vertex vertex, ref List<Vertex> visitedVertexes, ref List<Edge> visitedEdges)
        {
            var result = false;
            if (visitedVertexes.Contains(vertex))
            {
                return true;
            }

            visitedVertexes.Add(vertex);
            var localVisitedEdges = visitedEdges;
            var nextVertexes = vertex.Edges
                                        .Where(e => !localVisitedEdges.Contains(e))
                                        .Select(e => e.VertexBegin != vertex? e.VertexBegin : e.VertexEnd)
                                        .ToList();

            foreach (var edge in vertex.Edges)
            {
                if (!visitedEdges.Contains(edge))
                {
                    visitedEdges.Add(edge);
                }
            }

            foreach (var nextVertex in nextVertexes)
            {
                result |= SubtreeHasLoops(nextVertex, ref visitedVertexes, ref visitedEdges);
            }

            return result;
        }
    }
}
