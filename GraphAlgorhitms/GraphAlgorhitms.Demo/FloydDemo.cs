using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using GraphAlgorhitms.Graphonium.Tools;
using GraphAlgorhitms.Infrastructure;
using GraphAlgorhitms.IO;
using GraphAlgorhitms.Sources.MinOstTree.Kruskal;
using GraphAlgorhitms.Sources.ShortestWay.Floyd;

namespace GraphAlgorhitms.Demo
{
    public class FloydDemo : IRunnable
    {
        public void Run()
        {
            var text = TextUtils.Read(Globals.InputFilePath);

            var initGraph = GraphUtils.Get(text);
            var floyd = new Floyd();
            var matrix = floyd.GetWeightMatrix(initGraph);

            var resultString = string.Empty;
            for (var i = 0; i < matrix.GetLength(0); i++)
            {
                for (var j = 0; j < matrix.GetLength(1); j++)
                {
                    resultString += matrix[i, j] + Globals.Space.ToString();
                }
                resultString += Globals.LineSeparator;
            }

            TextUtils.Write(Globals.OutputFilePath, resultString);
        }
    }
}
