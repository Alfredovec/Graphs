using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace GraphAlgorhitms.Graphonium.Models
{
    public class Vertex
    {
        public Vertex()
        {
            Edges = new List<Edge>();
        }

        public int Number { get; set; }

        public ICollection<Edge> Edges { get; set; }

        public virtual ICollection<Edge> OutgoingEdges
        {
            get { return Edges.Where(e => e.VertexBegin == this).ToList(); }
        }

        public virtual ICollection<Edge> IncomingEdges
        {
            get { return Edges.Where(e => e.VertexEnd == this).ToList(); }
        }
    }
}
