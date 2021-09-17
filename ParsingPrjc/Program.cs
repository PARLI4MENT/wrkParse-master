using System;

namespace ParsingPrjc
{
    class Program
    {
        static string InputDir = System.IO.Directory.GetCurrentDirectory() + "\\input\\";
        static string OutputDir = System.IO.Directory.GetCurrentDirectory() + "\\output\\";

        private void Main(string[] args)
        {
            ParseMain parseMain = new ParseMain();
            Console.Clear();
            parseMain.ParseFilesByMasked();


            Console.ReadKey();
            Environment.Exit(0);
        }
    }
}
