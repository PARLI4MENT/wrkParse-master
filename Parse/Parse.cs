using System;
using System.Threading;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Globalization;
using ClosedXML.Excel;
using CsvHelper;
using System.Threading.Tasks;

namespace Parse
{
    public class ParseMain
    {
        public string InputPath { private get; set; } = Directory.GetCurrentDirectory() + "\\input\\";
        public string OutputPath { private get; set; } = Directory.GetCurrentDirectory() + "\\output\\";


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
                if (Directory.Exists(InputPath) && Directory.Exists(OutputPath))
                    return true;
                else
                {
                    Directory.CreateDirectory(InputPath);
                    Directory.CreateDirectory(OutputPath);
                    return true;
                }
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }

            return false;
        }

        public void ParseFilesByMasked()
        {
            try
            {
                Console.WriteLine("Started Parse source files by masked...");

                if (CheckExistsInputPath())
                {
                    List<string> PathFiles = new List<string>();

                    PathFiles.AddRange(System.IO.Directory.GetFiles(InputPath, "*.xls").ToList<string>());
                    PathFiles.AddRange(System.IO.Directory.GetFiles(InputPath, "*.xlsx").ToList<string>());
                    PathFiles.AddRange(System.IO.Directory.GetFiles(InputPath, "*.csv").ToList<string>());
                    PathFiles.AddRange(System.IO.Directory.GetFiles(InputPath, "*.txt").ToList<string>());


                    #region Ввести в функцию private void PrintList(List<string> InputList)
                    List<string> xlsFiles = new List<string>();
                    List<string> xlsxFiles = new List<string>();
                    List<string> csvFiles = new List<string>();
                    List<string> txtFiles = new List<string>();

                    foreach (var item in PathFiles)
                    {
                        FileInfo fileInfo = new FileInfo(item);
                        if (fileInfo.Extension == ".xls")
                            xlsFiles.Add(item);
                        if (fileInfo.Extension == ".xlsx")
                            xlsxFiles.Add(item);
                        if (fileInfo.Extension == ".csv")
                            csvFiles.Add(item);
                        if (fileInfo.Extension == ".txt")
                            csvFiles.Add(item);
                    }

                    /// Print all found files by masked
                    PrintList(xlsFiles);
                    PrintList(xlsxFiles);
                    PrintList(csvFiles);
                    PrintList(txtFiles);
                    #endregion

                    Thread.Sleep(1000);
                    Console.WriteLine("Press any key to continue..");
                    //Console.ReadKey();
                    Console.Clear();

                    #region
                    /// 
                    /// Old variant Parse by masked
                    /// 
                    //foreach (var item in PathFiles)
                    //{
                    //    FileInfo fileInfo = new FileInfo(item);

                    //    switch (fileInfo.Extension)
                    //    {
                    //        case ".xls" or ".xlsx":
                    //            ParseInExcel(item);
                    //            break;

                    //        case ".csv":
                    //            ParseInCSV1(item);
                    //            break;
                    //        case ".txt":
                    //            ParseInTxt1(item);
                    //            break;

                    //        default:
                    //            Console.ForegroundColor = ConsoleColor.Red;
                    //            Console.WriteLine("UNSUPPORTED FORMAT!");
                    //            Thread.Sleep(1000);
                    //            Console.ReadKey();
                    //            break;
                    //    }
                    //}
                    #endregion
                    Parallel.ForEach(PathFiles, item =>
                    {
                        FileInfo fileInfo = new FileInfo(item);

                        switch (fileInfo.Extension)
                        {
                            case ".xls" or ".xlsx":
                                ParseInExcel(item);
                                break;

                            case ".csv":
                                ParseInCSV1(item);
                                break;
                            case ".txt":
                                ParseInTxt1(item);
                                break;

                            default:
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("UNSUPPORTED FORMAT!");
                                Thread.Sleep(1000);
                                Console.ReadKey();
                                break;
                        }
                    });
                }
                Console.WriteLine("\n\n\t\t***All files Parse Done!***\n\n");
            }
            catch (Exception ex) { Console.WriteLine("Failed process: {0}", ex.Message); }
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
                        case ".xls":
                            {
                                Console.WriteLine("\nFiles with extension XLS:");
                                int i = 1;
                                foreach (var inputList in InputList)
                                {
                                    FileInfo info = new FileInfo(inputList);
                                    Console.WriteLine($"\t[{i}] {info.Name}");
                                    i++;
                                }
                                break;
                            }
                        case ".xlsx":
                            {
                                Console.WriteLine("\nFiles with extension XLSX:");
                                int i = 1;
                                foreach (var inputList in InputList)
                                {
                                    FileInfo info = new FileInfo(inputList);
                                    Console.WriteLine($"\t[{i}] {info.Name}");
                                    i++;
                                }
                                break;
                            }
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
                        case ".txt":
                            {
                                Console.WriteLine("\nFiles with extension TXT:");
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

        //private void ParseInExcel()
        //{
        //    try
        //    {
        //        foreach (var listFile in listFiles)
        //        {
        //            using (var Workbook = new XLWorkbook(listFile))
        //            {
        //                var DataRow = Workbook.Worksheet(1).RowsUsed();
        //                int i = 1;
        //                foreach (var dataRow in DataRow)
        //                {
        //                    string[] sourceRows =
        //                        {ClearedSymbol(dataRow.Cell(1).Value.ToString()),
        //                        ClearedSymbol(dataRow.Cell(2).Value.ToString()),
        //                        ClearedSymbol(dataRow.Cell(3).Value.ToString()),
        //                        ClearedSymbol(dataRow.Cell(4).Value.ToString())};

        //                    //Console.WriteLine($"[{i}]\t{sourceRows[0]}\t{sourceRows[1]}\t{sourceRows[2]}\t{sourceRows[3]}");

        //                    AppendDataOutput(listFile, sourceRows);

        //                    //Console.WriteLine($"[{i}]\t{strCell1}\t{strCell2.ToUpper()}\t{strCell3.ToUpper()}\t{strCell4.ToUpper()}");
        //                    i++;
        //                }
        //                Console.WriteLine("Parse Done!");
        //            }
        //        }
        //    }
        //    catch (Exception)
        //    { 
        //        throw;
        //    }
        //}
        private bool ParseInExcel(string sourcePath)
        {
            try
            {
                FileInfo info = new FileInfo(sourcePath);
                Console.WriteLine($"\n\nStart parse Excel file: {info.Name}");

                List<Part1> ExcelList = new List<Part1>();
                using (var Workbook = new XLWorkbook(sourcePath))
                {
                    var DataRow = Workbook.Worksheet(1).RowsUsed();
                    int i = 0;
                    foreach (var dataRow in DataRow)
                    {
                        ExcelList.Add(new Part1()
                        {
                            Lastname = ClearedSymbol(dataRow.Cell(1).Value.ToString().ToUpper()),
                            Firstname = ClearedSymbol(dataRow.Cell(2).Value.ToString().ToUpper()),
                            Patronymic = ClearedSymbol(dataRow.Cell(3).Value.ToString().ToUpper()),
                            Sex = ClearedSymbol(dataRow.Cell(4).Value.ToString().ToUpper()),
                            Country = ClearedSymbol(dataRow.Cell(5).Value.ToString().ToUpper()),
                            Phone = ClearedSymbol(dataRow.Cell(6).Value.ToString().ToUpper()),
                            Email = ClearedSymbol(dataRow.Cell(7).Value.ToString().ToUpper())
                        });
                        i++;
                    }
                    Console.WriteLine($"Parse {info.Name} Done! " + $"Rows is: {i}");
                    Thread.Sleep(1000);

                    /// Clear Dublicate
                    //ClearDublicates(ExcelList);

                    /// Out to TXT
                    //OutToTXT(listFile, mainList);

                    /// Out to CSV
                    OutToCSV(sourcePath, ExcelList);
                }
                return true;
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); return false; }
        }
        private void ParseInCSV1(string sourcePath)
        {
            FileInfo info = new FileInfo(sourcePath);
            Console.WriteLine($"\n\nStart parse Excel file: {info.Name}");
            using (var streamReader = new StreamReader(sourcePath))
            {
                using (var csvReader = new CsvReader(streamReader, CultureInfo.InvariantCulture))
                {
                    csvReader.Context.RegisterClassMap(new PartInfoClassMap1());
                    //var records = csvReader.GetRecords<CSVReaderMap1>().ToList();
                    var records = csvReader.GetRecords<Part2>().ToList();
                    Console.WriteLine($"Records: {records.Count}");

                    OutToCSV(sourcePath, records);
                }
            }
        }

        private void ParseInTxt1(string sourcePath)
        {
            FileInfo info = new FileInfo(sourcePath);
            Console.WriteLine($"\n\nStart parse Txt file: {info.Name}");
            using (var streamReader = new StreamReader(sourcePath))
            {
                
            }
        }

        //private void ClearDublicates(List<Part1> InputList)
        //{
        //    try
        //    {
        //        Console.WriteLine("Rows is: {0}", InputList.Count);

        //        System.Diagnostics.Stopwatch stopWatch = new System.Diagnostics.Stopwatch();

        //        var tmpList = new List<Part1>();
        //        UInt64 outIndx = 0;

        //        stopWatch.Start();

        //        #region
        //        /*
        //        foreach (var item1 in tmpList)
        //        {
        //            foreach (var item2 in tmpList)
        //            {
        //                if ((item1.Index != item2.Index)
        //                    && (item1.Firstname == item2.Firstname)
        //                    && (item1.Lastname == item2.Lastname)
        //                    && (item1.Phone == item2.Phone))

        //                {
        //                    //Console.WriteLine($"\n[{outIndx}] Search dublicate:");
        //                    //Console.WriteLine($"\tITEM1 [Index: {item1.Index}\tFirstname: {item1.Firstname}\tLastname: {item1.Lastname}\tPhone: {item1.Phone}]");
        //                    //Console.WriteLine($"\tITEM1 [Index: {tmpList[i].Index}\tFirstname: {tmpList[i].Firstname}\tLastname: {tmpList[i].Lastname}\tPhone: {tmpList[i].Phone}]");

        //                    //Console.WriteLine("Deleted Index[{0}]\n", InputList[i].Index);
        //                    //InputList.Remove(InputList[i]);
        //                }

        //                if (outIndx % 1000000000 == 0)
        //                {
        //                    Console.WriteLine($"Step: {outIndx}\tRunTime is: {stopWatch.Elapsed} ms");
        //                    stopWatch.Reset();
        //                    stopWatch.Start();
        //                }
        //                outIndx++;
        //            }
        //        }
        //        */
        //        #endregion

        //        Parallel.For(0, InputList.Count, i =>
        //        {
        //            for (int j = i; j < InputList.Count; j++)
        //            {
        //                if ((InputList[i].Index != InputList[j].Index)
        //                    && (InputList[i].Firstname == InputList[j].Firstname)
        //                    && (InputList[i].Lastname == InputList[j].Lastname)
        //                    && (InputList[i].Phone == InputList[j].Phone))

        //                {
        //                    //dublFound++;
        //                    //Console.WriteLine($"\n[{outIndx}] Search dublicate:");
        //                    //Console.WriteLine($"\tITEM1 [Index: {InputList[i].Index}\tFirstname: {InputList[i].Firstname}\tLastname: {InputList[i].Lastname}\tPhone: {InputList[i].Phone}]");
        //                    //Console.WriteLine($"\tITEM1 [Index: {InputList[j].Index}\tFirstname: {InputList[j].Firstname}\tLastname: {InputList[j].Lastname}\tPhone: {InputList[j].Phone}]");

        //                    //Console.WriteLine("Unique Index[{0}]\n", InputList[i].Index);
        //                    //tmpList.Add(InputList[i]);
        //                }

        //                if (outIndx % 1000000000 == 0)
        //                {
        //                    Console.WriteLine($"Iteration is: {outIndx}\tRunTime is: {stopWatch.Elapsed} ms");
        //                    stopWatch.Restart();
        //                }
        //                outIndx++;
        //            }
        //        });

        //        stopWatch.Stop();

        //        //Thread.Sleep(5000);
        //        //Console.WriteLine($"Result:{tmpList.Count}\tDublicate found: {dublFound}");

        //        Console.WriteLine($"Interation: {outIndx}\tCount rows is: {InputList.Count}\tRunTime is: {stopWatch.Elapsed} ms");

        //        Console.ReadKey();
        //        OutToCSV(tmpList);
        //    }
        //    catch (Exception ex) { Console.WriteLine(ex.Message); return; }
        //}

        //private void ClearDublicates(ref List<Part> InputList)
        //{
        //    try
        //    {
        //        var tmpList = new List<Part>(InputList);
        //        Console.Clear();
        //        Console.WriteLine("Rows is: {0}", InputList.Count);

        //        int i = 0;

        //        System.Diagnostics.Stopwatch stopwatch = new System.Diagnostics.Stopwatch();
        //        stopwatch.Start();
        //        foreach (var item1 in tmpList)
        //        {
        //            foreach (var item2 in tmpList)
        //            {
        //                if ((item1.Index != item2.Index)
        //                    && (item1.Firstname == item2.Firstname)
        //                    && (item1.Lastname == item2.Lastname)
        //                    && (item1.Phone == item2.Phone))
        //                {
        //                    //Console.WriteLine($"[{i}] Search dublicate:");
        //                    //Console.WriteLine($"\tITEM1 [Index: {item1.Index}\tFirstname: {item1.Firstname}\tLastname: {item1.Lastname}\tPhone: {item1.Phone}]");
        //                    //Console.WriteLine($"\tITEM1 [Index: {item2.Index}\tFirstname: {item2.Firstname}\tLastname: {item2.Lastname}\tPhone: {item2.Phone}]");

        //                    //Console.WriteLine("\nDeleted {0}", item2);
        //                    InputList.Remove(item2);
        //                    i++;
        //                }
        //            }
        //        }
        //        stopwatch.Stop();
        //        TimeSpan ts = stopwatch.Elapsed;
        //        string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
        //            ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds / 10);
        //        Thread.Sleep(5000);
        //        Console.WriteLine($"Deleted: {i} rows\nCount rows is: {InputList.Count}\nRunTime is: {elapsedTime}");
        //        OutToCSV(InputList);
        //    }
        //    catch (Exception ex) { Console.WriteLine(ex.Message); return; }
        //}

        static private void ClearedSymbol(ref string str)
        {
            try
            {
                System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(@"[!$#+]");
                System.Text.RegularExpressions.Match match = regex.Match(str.ToString());
                while (match.Success)
                {
                    str = match.Result(str.ToString());
                    Console.WriteLine("{0}: {1}", match.Index, match.Value);
                    match = match.NextMatch();
                }

                //str = str.ToString().Replace("?", "");
                return;
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); return; }
        }

        //static private void ClearedSymbol(ref List<Part> refList)
        //{
        //    try
        //    {
        //        Regex regex = new Regex(@"[!?$#+]");

        //        Match match = regex.Match(str.ToString());
        //        while (match.Success)
        //        {
        //            str = match.Result(str.ToString());
        //            Console.WriteLine("{0}: {1}", match.Index, match.Value);
        //            match = match.NextMatch();
        //        }

        //        //str = str.ToString().Replace("?", "");
        //        //str = str.ToString().Replace("'", "");
        //    }
        //    catch (Exception ex) { Console.WriteLine(ex.Message); throw; }
        //}
        private string ClearedSymbol(string str)
        {
            try
            {
                string tmp = Regex.Replace(str, @"[?.']", "");
                return tmp;
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); return str; }
        }

        private async void OutToTXT(string pathToFile, string[] str)
        {
            string tmpStr = $"{str[0]} {str[1]} {str[2]} {str[3]}";
            await File.AppendAllTextAsync($"{pathToFile}.txt", tmpStr + Environment.NewLine);
        }
        private bool OutToTXT(string pathToFile, List<Part1> InputList)
        {
            try
            {
                #region
                //List<Part> uniqueList = InputList.Distinct().ToList();
                //uniqueList.ForEach(i => File.AppendAllTextAsync($"{pathToFile}.txt", $"{i.Subpart1}\t{i.Subpart2}\t{i.Subpart3}\t{i.Subpart4}" + Environment.NewLine));


                //List<Part> UniqueList = InputList;
                //foreach (var item1 in InputList)
                //{
                //    foreach (var item2 in UniqueList)
                //    {
                //        if (item1.Index != item2.Index
                //            && item1.Subpart1 == item2.Subpart1
                //            && item1.Subpart2 == item2.Subpart2
                //            && item1.Subpart3 == item2.Subpart3
                //            && item1.Subpart4 == item2.Subpart4)
                //        {
                //            Console.WriteLine("[{0}]Unique: {1}\t{2}\t{3}\t{4}", item1.Index, item1.Subpart1, item1.Subpart2, item1.Subpart3, item1.Subpart4);
                //            Console.WriteLine("[{0}]Dublicate: {1}\t{2}\t{3}\t{4}" + Environment.NewLine, item2.Index, item2.Subpart1, item2.Subpart2, item2.Subpart3, item2.Subpart4);

                //            UniqueList.Remove(item2);
                //        }
                //    }
                //}

                //UniqueList.ForEach(i => File.AppendAllTextAsync($"{pathToFile}.txt", $"[{i.Index}]\t{i.Subpart1}\t{i.Subpart2}\t{i.Subpart3}\t{i.Subpart4}" + Environment.NewLine));
                //InputList.ForEach(i => File.AppendAllTextAsync($"{pathToFile}.txt", $"[{i.Index}]\t{i.Subpart1}\t{i.Subpart2}\t{i.Subpart3}\t{i.Subpart4}\t{i.Subpart5}" + Environment.NewLine));
                #endregion

                FileInfo fileInfo = new FileInfo(pathToFile);

                var txtPath = OutputPath + fileInfo.Name + ".txt";

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
                Thread.Sleep(1000);
                return true;
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); return false; }
        }

        private bool OutToCSV(string sourcePath, List<Part1> InputList)
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
        private bool OutToCSV(List<Part1> InputList)
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
        private bool OutToCSV(string sourcePath, List<Part2> InputList)
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