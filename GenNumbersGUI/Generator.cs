using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsvHelper;

namespace GenNumers
{
    class Generator
    {
        List<string> result;
        string format = "000 00 00";
        private string maskedInput { get; set; } = "C:\\prjct\\GenNumbers\\Gen_ver1\\masked";
        private string pathOutput { get; set; } = "C:\\prjct\\GenNumbers\\Gen_ver1\\output";
        public Generator() { }


        public void Gen(uint startPoint = 0, uint endPoint = 9999999)
        {
            Console.WriteLine($"\n==== Generate ny Masked {format} ====");

            result = new List<string>();
            for (int i = (int)startPoint; i <= (int)endPoint; i++)
            {
                //Console.WriteLine($"[{i}]\t{i.ToString(format)}");
                result.Add(i.ToString(format));
            }

            //OutToCSV1(ref result);

            Console.WriteLine($"[ {result[0]} ]End!");
            Console.WriteLine($"[ {result[result.Count-1]} ]End!");
            Console.WriteLine($"[ {result.Count} ]End!");
            Console.ReadKey();
        }

        private void OutToCSV1(ref List<string> result)
        {
            try
            {
                Console.WriteLine("\n==== Write to CSV ====");


            }
            catch (Exception ex) { Console.WriteLine(ex.Message); return; }
        }

        public void OutToTXT1(ref List<string> result)
        {

        }

    }
}
