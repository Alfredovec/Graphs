using GraphAlgorhitms.Graphonium.Models;

namespace GraphAlgorhitms.Sources.ShortestWay.BellmanFord
{
    public class BellmanFordVertex : Vertex
    {
        public BellmanFordVertex PreviousVertex { get; set; }

        public int WayValue { get; set; }
    }
}
