using System;
using System.IO;

namespace ParseTXT_ver1
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
