using System;
using System.IO;
using System.Collections;
using Parse;

namespace parse_prjct
{
    class Program
    {
        static string InputDir = Directory.GetCurrentDirectory() + "\\input\\";
        static string OutputDir = Directory.GetCurrentDirectory() + "\\output\\";

        private static void Main(string[] args)
        {
            ParseMain parseMain = new ParseMain();
            Console.Clear();
            parseMain.ParseFilesByMasked();


            Console.ReadKey();
            Environment.Exit(0);
        }
    }
}
