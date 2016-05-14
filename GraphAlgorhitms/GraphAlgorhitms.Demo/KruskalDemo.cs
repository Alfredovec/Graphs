using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GraphAlgorhitms.Graphonium.Tools;
using GraphAlgorhitms.Infrastructure;
using GraphAlgorhitms.IO;
using GraphAlgorhitms.Sources.Kruskal;

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

            TextUtils.Write(Globals.OutputFilePath, ostGraph.Edges.Sum(e => e.Weight).ToString());
        }
    }
}
