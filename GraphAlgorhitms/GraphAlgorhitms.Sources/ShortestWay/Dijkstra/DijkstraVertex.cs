using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using GraphAlgorhitms.Graphonium.Models;

namespace GraphAlgorhitms.Sources.ShortestWay.Dijkstra
{
    internal class DijkstraVertex : Vertex
    {
        public DijkstraVertex PreviousVertex { get; set; }

        public int WayValue { get; set; }

        public bool Visited { get; set; }
    }
}
