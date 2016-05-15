using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GraphAlgorhitms.Graphonium.Tools;
using GraphAlgorhitms.Infrastructure;
using GraphAlgorhitms.IO;
using GraphAlgorhitms.Sources.MaxFlow.FordFulkerson;

namespace GraphAlgorhitms.Demo
{
    public class FordFulkersonDemo : IRunnable
    {
        public void Run()
        {
            var text = TextUtils.Read(Globals.InputFilePath);

            var initGraph = GraphUtils.Get(text);
            var bellmanFord = new FordFulkerson();
            var maxFlow = bellmanFord.GetMaxFlow(initGraph);

            TextUtils.Write(Globals.OutputFilePath, maxFlow.ToString());
        }
    }
}
