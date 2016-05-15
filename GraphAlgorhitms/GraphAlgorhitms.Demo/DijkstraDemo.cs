using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GraphAlgorhitms.Graphonium.Tools;
using GraphAlgorhitms.Infrastructure;
using GraphAlgorhitms.IO;
using GraphAlgorhitms.Sources.MinOstTree.Kruskal;
using GraphAlgorhitms.Sources.ShortestWay.Dijkstra;

namespace GraphAlgorhitms.Demo
{
    public class DijkstraDemo : IRunnable
    {
        public void Run()
        {
            var text = TextUtils.Read(Globals.InputFilePath);

            var initGraph = GraphUtils.Get(text);
            var kruskal = new Dijkstra();
            var waysDictionary = kruskal.GetShortestWays(initGraph);

            var resultString = 
                    waysDictionary.Aggregate("", (s, pair) => s += pair.Key + " - " + pair.Value + Globals.LineSeparator);
            TextUtils.Write(Globals.OutputFilePath, resultString);
        }
    }
}
