using System.Collections.Generic;
using System.Linq;
using GraphAlgorhitms.Graphonium.Models;

namespace GraphAlgorhitms.Sources.ShortestWay.BellmanFord
{
    public class BellmanFord
    {
        public Dictionary<int, int> GetShortestWays(Graph initGraph)
        {
            var bellmanFordVertexes = initGraph.Vertexes.Select(v => new BellmanFordVertex()
            {
                Edges = v.Edges,
                Number = v.Number,
                PreviousVertex = null,
                WayValue = int.MaxValue
            }).ToList();

            var originVertex = bellmanFordVertexes.First();
            originVertex.WayValue = 0;

            for (var i = 0; i < bellmanFordVertexes.Count - 1; i++)
            {
                for (var j = 0; j < bellmanFordVertexes.Count; j++)
                {
                    var currentVertex = bellmanFordVertexes.ElementAt(j);

                    foreach (var edge in currentVertex.OutgoingEdges)
                    {
                        var bellmanFordEndVertex = bellmanFordVertexes.Single(v => v.Number == edge.VertexEnd.Number);

                        if (bellmanFordEndVertex.WayValue > currentVertex.WayValue + edge.Weight)
                        {
                            bellmanFordEndVertex.WayValue = currentVertex.WayValue + edge.Weight;
                            bellmanFordEndVertex.PreviousVertex = currentVertex;
                        }
                    }
                }
            }

            var result = bellmanFordVertexes.ToDictionary(v => v.Number, v => v.WayValue);

            return result;
        }
    }
}
