using System;
using System.Collections.Generic;
using System.IO;
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
            for (int j = 0; j < 1; j++)
            {
                var text = TextUtils.Read(Globals.InputFilePath);
                var initGraph = GraphUtils.GetFromFile(text);
                try
                {
                    var travellingSalesman = new TravellingSalesman();
                    var minPath = travellingSalesman.GetMinPath(initGraph);
                    var result = minPath.Aggregate("", (s, i) => s += i + Globals.Space.ToString());

                    TextUtils.Write(Globals.OutputFilePath, result);

                    if (result[result.Length - 2] != '1')
                    {
                        throw new Exception("Wrooong");
                    }
                }
                catch (Exception ex)
                {
                    var badGraph = initGraph.Vertexes.Count + Globals.Space + initGraph.Edges.Count + Globals.LineSeparator;
                    foreach (var edge in initGraph.Edges)
                    {
                        badGraph += edge.VertexBegin.Number + Globals.Space + edge.VertexEnd.Number + Globals.Space + edge.Weight + Globals.LineSeparator;
                    }
                    File.WriteAllText("files/badgraph.txt", badGraph);
                    Console.Write(ex.Message);
                }
            }

            var success = true;
        }
    }
}
