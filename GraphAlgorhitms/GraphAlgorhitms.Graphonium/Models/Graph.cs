using System.Collections.Generic;
using System.Text;

namespace GraphAlgorhitms.Graphonium.Models
{
    public class Graph
    {
        public Graph()
        {
            Vertexes = new List<Vertex>();
            Edges = new List<Edge>();
        }
        public ICollection<Vertex> Vertexes { get; set; }

        public ICollection<Edge> Edges { get; set; }
    }
}
