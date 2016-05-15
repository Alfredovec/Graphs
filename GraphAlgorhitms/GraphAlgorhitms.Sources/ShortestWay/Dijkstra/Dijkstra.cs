using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GraphAlgorhitms.Graphonium.Models;
using GraphAlgorhitms.Infrastructure;

namespace GraphAlgorhitms.Sources.ShortestWay.Dijkstra
{
    public class Dijkstra
    {
        public Dictionary<int, int> GetShortestWays(Graph initGraph)
        {
            var dijkstraVertexes = initGraph.Vertexes.Select(v => new DijkstraVertex()
            {
                Edges = v.Edges,
                Number = v.Number,
                Visited = false,
                PreviousVertex = null,
                WayValue = int.MaxValue
            }).ToList();

            var originVertex = dijkstraVertexes.First();
            originVertex.WayValue = 0;

            while (dijkstraVertexes.Any(v => !v.Visited))
            {
                var currentVertex = dijkstraVertexes.Where(v => !v.Visited).MinBy(v => v.WayValue);

                foreach (var edge in currentVertex.Edges)
                {
                    var endpointVertex = edge.VertexBegin.Number != currentVertex.Number
                        ? edge.VertexBegin
                        : edge.VertexEnd;

                    var endpointDijkstraVertex = dijkstraVertexes.Single(v => v.Number == endpointVertex.Number);

                    if (endpointDijkstraVertex.WayValue > currentVertex.WayValue + edge.Weight)
                    {
                        endpointDijkstraVertex.WayValue = currentVertex.WayValue + edge.Weight;
                        endpointDijkstraVertex.PreviousVertex = currentVertex;
                    }
                }

                currentVertex.Visited = true;
            }

            var result = dijkstraVertexes.ToDictionary(v => v.Number, v => v.WayValue);

            return result;
        }
    }
}
