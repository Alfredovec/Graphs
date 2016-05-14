using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphAlgorhitms.IO
{
    public static class TextUtils
    {
        public static string Read(string filePath)
        {
            var allText = File.ReadAllText(filePath);

            return allText;
        }

        public static void Write(string filePath, string text)
        {
            File.WriteAllText(filePath, text); 
        }
    }
}
