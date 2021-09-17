using CsvHelper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CsvHelper.Configuration.Attributes;
using System.Globalization;

namespace ParseCSV_ver1
{
    class ParseMain
    {
        //public string InputPath { private get; set; } = Directory.GetCurrentDirectory() + "\\input\\";
        //public string OutputPath { private get; set; } = Directory.GetCurrentDirectory() + "\\output\\";
        public string InputPath { private get; set; } = "C:\\prjct\\ParseCSV_ver1\\input\\";
        public string OutputPath { private get; set; } = "C:\\prjct\\ParseCSV_ver1\\output\\";
        public string ArchivePath { private get; set; } = "C:\\prjct\\ParseCSV_ver1\\archive\\";

        List<Part> list;
        public ParseMain() { }

        public void SetInputPath(string _inputPath)
        {
            InputPath = _inputPath;
        }
        public string GetInputPath()
        {
            if (InputPath != String.Empty)
            {
                return InputPath.ToString();
            }

            return null;
        }

        public bool CheckExistsInputPath()
        {
            try
            {
                if (Directory.Exists(InputPath) &&
                    Directory.Exists(OutputPath) &&
                    Directory.Exists(ArchivePath))
                    return true;
                else
                {
                    Directory.CreateDirectory(InputPath);
                    Directory.CreateDirectory(OutputPath);
                    Directory.CreateDirectory(ArchivePath);
                    return true;
                }
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }

            return false;
        }
        private void PrintList(List<string> InputList)
        {
            try
            {
                if (InputList.Count != 0)
                {
                    FileInfo fileInfo = new FileInfo(InputList[0]);
                    switch (fileInfo.Extension)
                    {
                        case ".csv":
                            {
                                Console.WriteLine("\nFiles with extension CSV:");
                                int i = 1;
                                foreach (var inputList in InputList)
                                {
                                    FileInfo info = new FileInfo(inputList);
                                    Console.WriteLine($"\t[{i}] {info.Name}");
                                    i++;
                                }
                                break;
                            }
                        default:
                            {
                                break;
                            }
                    }
                }
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); return; }
        }
        public void ParseFilesByMasked()
        {
            try
            {
                Console.WriteLine("Started Parse source files by masked...");

                if (CheckExistsInputPath())
                {
                    List<string> PathFiles = new List<string>();

                    PathFiles.AddRange(System.IO.Directory.GetFiles(InputPath, "*.csv").ToList<string>());

                    if (PathFiles.Count > 0)
                    {
                        List<string> csvFiles = new List<string>();

                        foreach (var item in PathFiles)
                        {
                            FileInfo fileInfo = new FileInfo(item);
                            if (fileInfo.Extension == ".csv")
                                csvFiles.Add(item);
                        }

                        PrintList(csvFiles);

                        Thread.Sleep(1000);
                        //Console.ReadKey();
                        Console.Clear();

                        Parallel.ForEach(PathFiles, item =>
                        {
                            FileInfo fileInfo = new FileInfo(item);

                            if (fileInfo.Extension == ".csv")
                            {
                                if (ParseInCSV1(item))
                                    File.Move(item, ArchivePath + fileInfo.Name);
                            }
                            else
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("UNSUPPORTED FORMAT!");
                                Thread.Sleep(1000);
                                Console.ReadKey();
                            }
                        });
                        Console.WriteLine("\n\n\t\t***All files Parse Done!***\n\n");
                    }
                    else
                    {
                        Console.WriteLine("\n\n\t\t***Files not found!***\n\n");
                    }
                }
                Thread.Sleep(5000);
                Console.Clear();
                ParseFilesByMasked();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed process: {ex.Message}");
                Thread.Sleep(5000);
                ParseFilesByMasked();
            }
        }

        private bool ParseInCSV1(string sourcePath)
        {
            try
            {
                FileInfo info = new FileInfo(sourcePath);
                Console.WriteLine($"\n\nStart parse СSV file: {info.Name}");
                using (var streamReader = new StreamReader(sourcePath))
                {
                    var config = new CsvHelper.Configuration.CsvConfiguration(CultureInfo.InvariantCulture)
                    {
                        PrepareHeaderForMatch = args => args.Header.ToUpper(),
                        Delimiter = ",",
                        HasHeaderRecord = true,
                        Encoding = System.Text.Encoding.UTF8,
                        IgnoreBlankLines = true,
                    };
                    using (var csvReader = new CsvReader(streamReader, config))
                    {
                        csvReader.Context.RegisterClassMap(new PartInfoClassMap());
                        var records = csvReader.GetRecords<Part>().ToList();
                        Console.WriteLine($"Records: {records.Count}");

                        ClearDublicate(ref records);
                        UnitFields(ref records);
                        OutToCSV(sourcePath, ref records);
                    }
                }
                

                return true;
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); return false; }
        }

        private void UnitFields(ref List<Part> records)
        {
            foreach (var item in records)
            {
                item.Name = $"{item.FName} {item.Name}";

                Console.WriteLine($"\n{item.Name} -- {item.Numb}");
            }
        }

        private void ClearDublicate(ref List<Part> InputList)
        {
            try
            {
                Console.WriteLine($"\nStart ClearedEmptyFilds Excel:");
                Console.WriteLine($"\t\t\tStart count: {InputList.Count}");
                uint countEmpty = 0;

                for (int i = InputList.Count - 1; i >= 0; i--)
                {
                    if (InputList[i].Numb == "" || InputList[i].Numb == " ")
                    {
                        countEmpty++;
                        //Console.WriteLine($"[{countEmpty}]\t{InputList[i].Lastname}\t{InputList[i].Firstname}\t{InputList[i].Patronymic}\t{InputList[i].Phone}");
                        InputList.RemoveAt(i);
                    }
                }

                Console.WriteLine($"\t\t\tEnd count: {InputList.Count}");
                Console.WriteLine($"\t\t\tRemoved {countEmpty} rows");
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); return; }
        }

        private bool OutToCSV(string sourcePath, ref List<Part> srcList)
        {
            try
            {
                FileInfo fileInfo = new FileInfo(sourcePath);
                Console.WriteLine($"\n[ {fileInfo.Name} ] Start Out to CSV");

                var csvPath = OutputPath + fileInfo.Name + ".csv";

                using (StreamWriter streamWriter = new StreamWriter(csvPath))
                {
                    using (CsvWriter csvWriter = new CsvWriter(streamWriter, culture: CultureInfo.InvariantCulture))
                    {
                        foreach (var item in srcList)
                        {
                            //csvWriter.WriteRecord(item);
                            if ((item.FName != null) && (item.FName.Length > 1))
                            {
                                //string FName = item.FName;
                                item.FName = char.ToUpper(item.FName[0]) + item.FName.Substring(1).ToLower();
                            }
                            if ((item.Name != null) && (item.Name.Length > 1))
                            {
                                //string Name = item.Name;
                                item.Name = char.ToUpper(item.Name[0]) + item.Name.Substring(1).ToLower();
                            }
                            
                            

                            csvWriter.WriteField($"{item.FName} {item.Name};;{item.Numb};;");
                            //csvWriter.WriteField($"{FName} {item.Name};;{item.Numb};;");
                            csvWriter.NextRecord();
                        }

                        csvWriter.Dispose();
                        streamWriter.Dispose();
                    }
                }

                FileInfo info = new FileInfo(csvPath);
                Console.WriteLine($"Write file {info.Name} is Done! \n\nPress any key.");
                return true;
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); return false; }
        }
    }

    public class PartInfoClassMap : CsvHelper.Configuration.ClassMap<Part>
    {
        public PartInfoClassMap(bool requiredFields = true)
        {
            Map(m => m.FName).Name("fname");
            Map(m => m.Name).Name("name");
            Map(m => m.WSD).Name("wsd").Ignore();
            Map(m => m.MF).Name("mf").Ignore();
            Map(m => m.City).Name("city").Ignore();
            Map(m => m.Numb).Name("numb");
        }
    }
    public class Part
    {
        [Name("fname")]
        public string FName { get; set; }

        [Name("name")]
        public string Name { get; set; }
        [Name("wsd")]
        public string WSD { get; set; }
        [Name("mf")]
        public string MF { get; set; }
        [Name("city")]
        public string City { get; set; }
        [Name("numb")]
        public string Numb { get; set; }

    }
}
