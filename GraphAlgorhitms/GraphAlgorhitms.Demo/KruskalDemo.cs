using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GraphAlgorhitms.Graphonium.Tools;
using GraphAlgorhitms.Infrastructure;
using GraphAlgorhitms.IO;
using GraphAlgorhitms.Sources.MinOstTree.Kruskal;

namespace GraphAlgorhitms.Demo
{
    public class KruskalDemo : IRunnable
    {
        public void Run()
        {
            var text = TextUtils.Read(Globals.InputFilePath);

            var initGraph = GraphUtils.Get(text);
            var kruskal = new Kruskal();
            var ostGraph = kruskal.GetMinOst(initGraph);

            var stringResult = string.Empty;
            stringResult += ostGraph.Edges.Sum(e => e.Weight).ToString() + Globals.LineSeparator;
            stringResult += ostGraph.Edges.Aggregate(string.Empty,
                (s, edge) => s += edge.VertexBegin.Number.ToString() + edge.VertexEnd.Number.ToString() + Globals.LineSeparator);

            TextUtils.Write(Globals.OutputFilePath, stringResult);
        }
    }
}
