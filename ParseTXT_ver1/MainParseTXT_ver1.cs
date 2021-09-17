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


/// <summary>
/// Parsing 2 files and Delet Dublication
/// </summary>
namespace ParseTXT_ver1
{
    public class Item1
    {
        public string Field { get; set; }
    }
    public class ParseMain
    {
        //public string InputPath { private get; set; } = Directory.GetCurrentDirectory() + "\\input\\";
        //public string OutputPath { private get; set; } = Directory.GetCurrentDirectory() + "\\output\\";
        public string InputPath { private get; set; } = "C:\\prjct\\ParseTXT_ver1\\input\\";
        public string OutputPath { private get; set; } = "C:\\prjct\\ParseTXT_ver1\\output\\";
        public string ArchivePath { private get; set; } = "C:\\prjct\\ParseTXT_ver1\\archive\\";

        public List<string> fileList1;
        public List<string> fileList2;
        public List<string> fileResult;

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
                    PathFiles.AddRange(System.IO.Directory.GetFiles(InputPath, "*.txt").ToList<string>());

                    if (PathFiles.Count == 2)
                    {
                        #region Ввести в функцию private void PrintList(List<string> InputList)
                        List<string> txtFiles = new List<string>();

                        foreach (var item in PathFiles)
                        {
                            FileInfo fileInfo = new FileInfo(item);
                            if (fileInfo.Extension == ".txt")
                                txtFiles.Add(item);
                        }

                        PrintList(txtFiles);

                        Thread.Sleep(1000);
                        //Console.ReadKey();
                        Console.Clear();
                        #endregion

                        fileList1 = ParseInTxt1(PathFiles[0]);
                        fileList2 = ParseInTxt1(PathFiles[1]);

                        ClearDublication(ref fileList1, ref fileList2);

                        OutToCSV(fileResult, PathFiles[0], PathFiles[1]);

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

        private List<string> ParseInTxt1(string sourcePath)
        {
            try
            {
                //List<Item1> tmpList = new List<Item1>();
                FileInfo info = new FileInfo(sourcePath);
                Console.WriteLine($"\n\nStart parse Txt file: {info.Name}");

                List<string> tmpList = File.ReadAllLines(sourcePath).ToList();

                return tmpList;
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); return null; }
        }

        private void ClearDublication(ref List<string> file1, ref List<string> file2)
        {
            file1.AddRange(file2);
            fileResult = new List<string>();
            fileResult = file1.Distinct().ToList();

            Console.WriteLine($"Source lines with dublications: {file1.Count}");
            Console.WriteLine($"Result no dublication lines: {fileResult.Count}");
            Console.WriteLine();
        }
        private bool OutToCSV(List<string> InputList, string path1, string path2)
        {
            try
            {
                FileInfo fileInfo1 = new FileInfo(path1);
                FileInfo fileInfo2 = new FileInfo(path2);
                Console.WriteLine($"\n[{fileInfo1.Name} + {fileInfo2.Name}] Start Out to CSV");

                var csvPath = OutputPath + fileInfo1.Name + " " + fileInfo2.Name + ".csv";

                using (StreamWriter streamWriter = new StreamWriter(csvPath))
                {
                    using (CsvWriter csvWriter = new CsvWriter(streamWriter, culture: CultureInfo.InvariantCulture))
                    {
                        foreach (var item in InputList)
                        {
                            //csvWriter.WriteRecord(item);
                            csvWriter.WriteField(item);
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


}
