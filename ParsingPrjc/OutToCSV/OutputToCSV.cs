using CsvHelper;
using CsvHelper.Configuration.Attributes;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ParsingPrjc.OutputToCSV
{
    class OutputToCSV : ParseMain
    {
        public bool OutToCSV(string sourcePath, List<Part1> InputList)
        {
            try
            {
                FileInfo fileInfo = new FileInfo(sourcePath);
                Console.WriteLine($"\n[{fileInfo.Name}] Start Out to CSV");

                var csvPath = OutputPath + fileInfo.Name + ".csv";

                var streamWriter = new StreamWriter(csvPath);
                var config = new CsvHelper.Configuration.CsvConfiguration(CultureInfo.InvariantCulture)
                {
                    PrepareHeaderForMatch = args => args.Header.ToUpper(),
                    Delimiter = ";;",
                    HasHeaderRecord = false,
                };
                var csvWriter = new CsvWriter(streamWriter, config);
                csvWriter.Context.RegisterClassMap(new PartInfoClassMapOut1(true));
                csvWriter.WriteRecords(InputList);
                csvWriter.Dispose();
                streamWriter.Dispose();

                FileInfo info = new FileInfo(csvPath);
                Console.WriteLine($"Write file {info.Name} is Done! \n\nPress any key.");
                return true;
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); return false; }
        }
        public bool OutToCSV(string sourcePath, ref List<PartStubb> InputList)
        {
            try
            {
                FileInfo fileInfo = new FileInfo(sourcePath);
                Console.WriteLine($"\n[{fileInfo.Name}] Start Out to CSV");

                var csvPath = OutputPath + fileInfo.Name + ".csv";

                var streamWriter = new StreamWriter(csvPath);
                var config = new CsvHelper.Configuration.CsvConfiguration(CultureInfo.InvariantCulture)
                {
                    PrepareHeaderForMatch = args => args.Header.ToUpper(),
                    Delimiter = ";;",
                    HasHeaderRecord = false,
                };
                var csvWriter = new CsvWriter(streamWriter, config);
                csvWriter.Context.RegisterClassMap(new PartInfoClassMapOut1(true));
                csvWriter.WriteRecords(InputList);
                csvWriter.Dispose();
                streamWriter.Dispose();

                FileInfo info = new FileInfo(csvPath);
                Console.WriteLine($"Write file {info.Name} is Done! \n\nPress any key.");
                return true;
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); return false; }
        }
        public bool OutToCSV(string sourcePath, ref List<Part1> InputList)
        {
            try
            {
                FileInfo fileInfo = new FileInfo(sourcePath);
                Console.WriteLine($"\n[{fileInfo.Name}] Start Out to CSV");

                var csvPath = OutputPath + fileInfo.Name + ".csv";

                var streamWriter = new StreamWriter(csvPath);
                var config = new CsvHelper.Configuration.CsvConfiguration(CultureInfo.InvariantCulture)
                {
                    PrepareHeaderForMatch = args => args.Header.ToUpper(),
                    Delimiter = ";;",
                    HasHeaderRecord = false,
                    Encoding = System.Text.Encoding.UTF8,
                };
                var csvWriter = new CsvWriter(streamWriter, config);
                csvWriter.Context.RegisterClassMap(new PartInfoClassMapOut1(true));
                csvWriter.WriteRecords(InputList);
                csvWriter.Dispose();
                streamWriter.Dispose();

                FileInfo info = new FileInfo(csvPath);
                Console.WriteLine($"Write file {info.Name} is Done! \n\nPress any key.");
                return true;
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); return false; }
        }

        public bool OutToCSV(List<Part1> InputList)
        {
            try
            {
                var csvPath = "C:\\" + DateTime.Now.ToString() + ".csv";

                var streamWriter = new StreamWriter(csvPath);
                var config = new CsvHelper.Configuration.CsvConfiguration(CultureInfo.InvariantCulture)
                {
                    PrepareHeaderForMatch = args => args.Header.ToUpper(),
                    Delimiter = ";;",
                    HasHeaderRecord = false,
                };
                var csvWriter = new CsvWriter(streamWriter, config);
                csvWriter.Context.RegisterClassMap(new PartInfoClassMapOut1(true));
                csvWriter.WriteRecords(InputList);
                csvWriter.Dispose();
                streamWriter.Dispose();

                FileInfo info = new FileInfo(csvPath);
                Console.WriteLine($"Write file {info.Name} is Done! \n\nPress any key.");
                return true;
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); return false; }
        }

        //private bool OutToCSV(string sourcePath, List<Part> InputList)
        //{
        //    try
        //    {
        //        FileInfo fileInfo = new FileInfo(sourcePath);
        //        Console.WriteLine($"Started output parsed file: {fileInfo.Name}...");
        //        var csvPath = OutputPath + fileInfo.Name + ".csv";

        //        var streamWriter = new StreamWriter(csvPath);
        //        var config = new CsvHelper.Configuration.CsvConfiguration(CultureInfo.InvariantCulture)
        //        {
        //            PrepareHeaderForMatch = args => args.Header.ToUpper(),
        //            Delimiter = ";;",
        //            HasHeaderRecord = false,
        //        };
        //        var csvWriter = new CsvWriter(streamWriter, config);
        //        csvWriter.Context.RegisterClassMap(new PartInfoClassMapOut(true));
        //        csvWriter.WriteRecords(InputList);
        //        csvWriter.Dispose();
        //        streamWriter.Dispose();

        //        FileInfo info = new FileInfo(csvPath);
        //        Console.WriteLine($"Write file {info.Name} is Done! \n\nPress any key.");
        //        return true;
        //    }
        //    catch (Exception ex) { Console.WriteLine(ex.Message); return false; }
        //}
        
        public bool OutToCSV(string sourcePath, List<Part2> InputList)
        {
            try
            {
                FileInfo fileInfo = new FileInfo(sourcePath);
                Console.WriteLine($"Started output parsed file: {fileInfo.Name}...");
                var csvPath = OutputPath + fileInfo.Name + ".csv";

                var streamWriter = new StreamWriter(csvPath);
                var config = new CsvHelper.Configuration.CsvConfiguration(CultureInfo.InvariantCulture)
                {
                    PrepareHeaderForMatch = args => args.Header.ToUpper(),
                    Delimiter = ";;",
                    HasHeaderRecord = false,
                };
                var csvWriter = new CsvWriter(streamWriter, config);
                csvWriter.Context.RegisterClassMap(new PartInfoClassMapOut2(true));
                csvWriter.WriteRecords(InputList);
                csvWriter.Dispose();
                streamWriter.Dispose();

                FileInfo info = new FileInfo(csvPath);
                Console.WriteLine($"Write file {info.Name} is Done! \n\nPress any key.");
                return true;
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); return false; }
        }
    }
}
