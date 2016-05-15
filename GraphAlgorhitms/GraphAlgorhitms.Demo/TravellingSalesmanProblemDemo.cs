using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GraphAlgorhitms.Graphonium.Tools;
using GraphAlgorhitms.Infrastructure;
using GraphAlgorhitms.IO;
using GraphAlgorhitms.Sources.MaxFlow.FordFulkerson;
using GraphAlgorhitms.Sources.TravellingSalesman;

namespace GraphAlgorhitms.Demo
{
    public class TravellingSalesmanProblemDemo : IRunnable
    {
        public void Run()
        {
            var text = TextUtils.Read(Globals.InputFilePath);

            var initGraph = GraphUtils.Get(text);
            var travellingSalesman = new TravellingSalesman();
            var minPath = travellingSalesman.GetMinPath(initGraph);

            var result = minPath.Aggregate("", (s, i) => s += i + Globals.Space.ToString());

            TextUtils.Write(Globals.OutputFilePath, result);
        }
    }
}
