using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ParsingPrjc
{
    class ParseMain
    {

        public static string InputPath { get; set; } = "C:\\_parse\\input\\";
        public static string OutputPath { get; set; } = "C:\\_parse\\output\\";
        public static string ArchivePath { private get; set; } = "C:\\_parse\\archive\\";
        public ParseMain() { }
        private string GetInputPath()
        {
            if (System.IO.File.Exists(InputPath))
            {
                return InputPath.ToString();
            }
            return null;
        }
        private string GetOutputPath()
        {
            if (System.IO.File.Exists(OutputPath))
            {
                return OutputPath.ToString();
            }
            return null;
        }
        private string GetArchivePath()
        {
            if (System.IO.File.Exists(ArchivePath))
            {
                return ArchivePath.ToString();
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

                    if (PathFiles.Count > 0)
                    {
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

                        System.Threading.Thread.Sleep(1000);
                        Console.Clear();
                        Parallel.ForEach(PathFiles, item =>
                        {
                            FileInfo fileInfo = new FileInfo(item);

                            switch (fileInfo.Extension)
                            {
                                case ".xls" or ".xlsx":
                                    if (ParseInExcelStab(item))
                                        File.Move(item, ArchivePath + fileInfo.Name);
                                    break;

                                case ".csv":
                                    if (ParseInCSV1(item))
                                        File.Move(item, ArchivePath + fileInfo.Name);
                                    break;

                                case ".txt":
                                    if (ParseInTxt1(item))
                                        File.Move(item, ArchivePath + fileInfo.Name);
                                    break;

                                default:
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.WriteLine("UNSUPPORTED FORMAT!");
                                    System.Threading.Thread.Sleep(1000);
                                    Console.ReadKey();
                                    break;
                            }
                        });
                        Console.WriteLine("\n\n\t\t***All files Parse Done!***\n\n");
                    }
                    else
                    {
                        Console.WriteLine("\n\n\t\t***Files not found!***\n\n");
                    }
                }
                System.Threading.Thread.Sleep(5000);
                Console.Clear();
                ParseFilesByMasked();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed process: {ex.Message}");
                System.Threading.Thread.Sleep(5000);
                ParseFilesByMasked();
            }
        }

        static protected void ClearedEmptyCryticalFileds(string sourcePath, ref List<Part1> InputList)
        {
            try
            {
                FileInfo info = new FileInfo(sourcePath);
                Console.WriteLine($"\n[{info.Name}] Start ClearedEmptyFilds Excel");
                uint countEmpty = 0;

                for (int i = InputList.Count - 1; i >= 0; i--)
                {
                    if (InputList[i].Phone == "" || InputList[i].Phone == " ")
                    {
                        countEmpty++;
                        //Console.WriteLine($"[{countEmpty}]\t{InputList[i].Lastname}\t{InputList[i].Firstname}\t{InputList[i].Patronymic}\t{InputList[i].Phone}");
                        InputList.RemoveAt(i);
                    }
                }

                Console.WriteLine($"Removed {countEmpty} rows");
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); return; }
        }
        static protected void ClearedEmptyCryticalFileds(string sourcePath, ref List<PartStubb> InputList)
        {
            try
            {
                FileInfo info = new FileInfo(sourcePath);
                Console.WriteLine($"\n[{info.Name}] Start ClearedEmptyFilds Excel");
                uint countEmpty = 0;

                for (int i = InputList.Count - 1; i >= 0; i--)
                {
                    if (InputList[i].Phone == "" || InputList[i].Phone == " ")
                    {
                        countEmpty++;
                        //Console.WriteLine($"[{countEmpty}]\t{InputList[i].Lastname}\t{InputList[i].Firstname}\t{InputList[i].Patronymic}\t{InputList[i].Phone}");
                        InputList.RemoveAt(i);
                    }
                }

                Console.WriteLine($"Removed {countEmpty} rows");
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); return; }
        }

        static protected void ClearedSymbol(ref string str)
        {
            try
            {
                System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(@"[?.'=!$#+]");
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

        protected string ClearedSymbol(string str)
        {
            try
            {
                //var regex = new System.Text.RegularExpressions.Regex(@"");
                string tmp = Regex.Replace(str, @"[\?\-\¦\-\+\=]", "");
                return tmp;
            }
            catch (RegexMatchTimeoutException ex) { Console.WriteLine(ex.Message); return str; }
        }

        s

    }
}
