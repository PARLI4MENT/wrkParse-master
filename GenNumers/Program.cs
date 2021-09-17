using System;

namespace GenNumers
{
    class Program
    {
        static void Main(string[] args)
        {
            Generator generator = new Generator();
            generator.Gen();

            Console.ReadKey();
        }
    }
}
