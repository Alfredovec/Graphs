using GraphAlgorhitms.Graphonium.Models;

namespace GraphAlgorhitms.Sources.MaxFlow.FordFulkerson
{
    public class FordFulkersonEdge : Edge
    {
        public int Flow { get; set; }
    }
}
