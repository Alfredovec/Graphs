using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GraphAlgorhitms.Graphonium.Tools;
using GraphAlgorhitms.Infrastructure;
using GraphAlgorhitms.IO;
using GraphAlgorhitms.Sources.ShortestWay.BellmanFord;
using GraphAlgorhitms.Sources.ShortestWay.Dijkstra;

namespace GraphAlgorhitms.Demo
{
    public class BellmanFordDemo : IRunnable
    {
        public void Run()
        {
            var text = TextUtils.Read(Globals.InputFilePath);

            var initGraph = GraphUtils.Get(text);
            var bellmanFord = new BellmanFord();
            var waysDictionary = bellmanFord.GetShortestWays(initGraph);

            var resultString =
                    waysDictionary.Aggregate("", (s, pair) => s += pair.Key + " - " + pair.Value + Globals.LineSeparator);
            TextUtils.Write(Globals.OutputFilePath, resultString);
        }
    }
}
