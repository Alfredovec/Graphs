﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GraphAlgorhitms.Demo;

namespace GraphAlgorhitms.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            // new KruskalDemo().Run();
            new PrimDemo().Run();

            System.Console.ReadLine();
        }
    }
}
