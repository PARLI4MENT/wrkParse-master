using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParsingPrjc.Out
{
    class OutToTXTClass : ParseMain
    {
        public bool OutToTXT(string sourcePath, List<Part1> InputList)
        {
            try
            {
                FileInfo info = new FileInfo(sourcePath);
                Console.WriteLine($"\n[{info.Name}] Start Out to TextFile");

                var txtPath = OutputPath + info.Name + ".txt";

                int i = 0;
                foreach (var list in InputList)
                {
                    //Console.WriteLine($"[{list.Index}]\t{list.Lastname}\t{list.Firstname}\t{list.Patronymic}\t{list.Sex}\t{list.DOB}" + Environment.NewLine);
                    /// OldVersion with filed "Index"
                    //File.AppendAllText($"{txtPath}", $"[{list.Index}]\t{list.Lastname}\t{list.Firstname}\t{list.Patronymic}\t{list.Sex}\t{list.DOB}\t{list.Country}\t{list.Phone}\t{list.Email}" + Environment.NewLine);
                    File.AppendAllText($"{txtPath}", $"{list.Lastname}\t{list.Firstname}\t{list.Patronymic}\t{list.Sex}\t{list.Country}\t{list.Phone}\t{list.Email}" + Environment.NewLine);
                    i++;
                }

                Console.WriteLine("Output to TXT format is Done! {0}", i);
                System.Threading.Thread.Sleep(1000);
                return true;
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); return false; }
        }
    }
}
