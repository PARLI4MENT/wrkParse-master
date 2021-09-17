using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParsingPrjc
{
    class ParseXLS: ParseMain
    {
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
                Console.WriteLine($"\n[{info.Name}] Start parse Excel file");

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
                    System.Threading.Thread.Sleep(1000);

                    /// ClearedEmptyFields
                    ClearedEmptyCryticalFileds(sourcePath, ref ExcelList);

                    /// Out to CSV
                    OutToCSV(sourcePath, ExcelList);
                }
                return true;
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); return false; }
        }
        public string GetColumnName(IXLTable table, string columnHeader)
        {
            var cell = table.HeadersRow().CellsUsed(c => c.Value.ToString() == columnHeader).FirstOrDefault();
            if (cell != null)
            {
                return cell.WorksheetColumn().ColumnLetter();
            }
            return null;
        }

        public bool ParseInExcelStab(string sourcePath)
        {
            try
            {
                FileInfo info = new FileInfo(sourcePath);
                Console.WriteLine($"\n[ {info.Name} ] Start parse Excel file");

                List<PartStubb> ExcelList = new List<PartStubb>();
                using (var Workbook = new XLWorkbook(sourcePath))
                {
                    var ws = Workbook.Worksheet(1);
                    var range = ws.RangeUsed();
                    var table = range.AsTable();

                    string City = GetColumnName(table, "Город");
                    string Name = GetColumnName(table, "Имя");
                    string Phone = GetColumnName(table, "Телефон");

                    var DataRow = Workbook.Worksheet(1).RowsUsed();

                    int i = 0;
                    foreach (var dataRow in DataRow)
                    {
                        ExcelList.Add(new PartStubb()
                        {
                            City = ClearedSymbol(dataRow.Cell(City).Value.ToString().ToUpper()),
                            Name = ClearedSymbol(dataRow.Cell(Name).Value.ToString().ToUpper()),
                            Phone = ClearedSymbol(dataRow.Cell(Phone).Value.ToString().ToUpper())
                        });
                        i++;
                    }
                    Console.WriteLine($"Parse {info.Name} Done! " + $"Rows is: {i}");
                    System.Threading.Thread.Sleep(1000);

                    /// ClearedEmptyFields
                    ClearedEmptyCryticalFileds(sourcePath, ref ExcelList);

                    /// Clear Dublicate
                    //ClearDublicates(ExcelList);

                    /// Out to TXT
                    //OutToTXT(listFile, mainList);

                    /// Out to CSV
                    OutputToCSV.OutputToCSV.OutToCSV(sourcePath, ref ExcelList);
                }
                return true;
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); return false; }
        }
    }
}
