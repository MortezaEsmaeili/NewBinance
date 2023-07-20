
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Skender.Stock.Indicators;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;

namespace TestArea
{
    public class MyCustomQuote : IQuote
    {
        // required base properties
        public DateTime Date { get; set; }
        public decimal Open { get; set; }
        public decimal High { get; set; }
        public decimal Low { get; set; }
        public decimal Close { get; set; }
        public decimal Volume { get; set; }

        // custom properties
        public int MyOtherProperty { get; set; }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            List<decimal> TestData = new List<decimal> { 9, 8, 5, 9, 10, 11, 12, 13, 15, 17, 15, 13, 14, 19, 13, 11, 10, 9, 8, 3, 5 };

          //  List<double> data = new List<double> { 1, 2, 3, 8, 9, 10, 16, 17, 18 };
            List<double> data = new List<double> { 43327, 43083, 42801, 42603, 42669, 41699, 46278 };
            // var breaks = JenksFisher.CreateJenksFisherBreaksArray(data, 2);
            var breaks = PercentClassifier.Classify(data, 5);
            var test = new PersonalMath(TestData);
            test.GetLocalMinMax(100, out List<Point> mins, out List<Point> maxes);


            Console.WriteLine("Local Mins:");
            mins.ForEach(m => Console.WriteLine(m.value));
            Console.WriteLine("Local Max:");
            maxes.ForEach(m => Console.WriteLine(m.value));
            Console.WriteLine("End");
            Console.ReadLine();

        }


        public class Author
        {
            public int Id { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
        }

        static void WriteExcelFile()
        {
            List<Author> authors = new List<Author>
{
                new Author { Id = 1, FirstName = "Joydip", LastName = "Kanjilal" },
                new Author { Id = 2, FirstName = "Steve", LastName = "Smith" },
                new Author { Id = 3, FirstName = "Anand", LastName = "Narayaswamy"}
            };

            // Lets converts our object data to Datatable for a simplified logic.
            // Datatable is most easy way to deal with complex datatypes for easy reading and formatting. 
            DataTable table = (DataTable)JsonConvert.DeserializeObject(JsonConvert.SerializeObject(authors), (typeof(DataTable)));

            using (SpreadsheetDocument document = SpreadsheetDocument.Create("D:\\TestNewData.xlsx", SpreadsheetDocumentType.Workbook))
            {
                WorkbookPart workbookPart = document.AddWorkbookPart();
                workbookPart.Workbook = new Workbook();

                WorksheetPart worksheetPart = workbookPart.AddNewPart<WorksheetPart>();
                var sheetData = new SheetData();
                worksheetPart.Worksheet = new Worksheet(sheetData);

                Sheets sheets = workbookPart.Workbook.AppendChild(new Sheets());
                Sheet sheet = new Sheet() { Id = workbookPart.GetIdOfPart(worksheetPart), SheetId = 1, Name = "Sheet1" };

                sheets.Append(sheet);

                Row headerRow = new Row();

                List<String> columns = new List<string>();
                foreach (System.Data.DataColumn column in table.Columns)
                {
                    columns.Add(column.ColumnName);

                    Cell cell = new Cell();
                    cell.DataType = CellValues.String;
                    cell.CellValue = new CellValue(column.ColumnName);
                    headerRow.AppendChild(cell);
                }

                sheetData.AppendChild(headerRow);

                foreach (DataRow dsrow in table.Rows)
                {
                    Row newRow = new Row();
                    foreach (String col in columns)
                    {
                        Cell cell = new Cell();
                        cell.DataType = CellValues.String;
                        cell.CellValue = new CellValue(dsrow[col].ToString());
                        newRow.AppendChild(cell);
                    }

                    sheetData.AppendChild(newRow);
                }

                workbookPart.Workbook.Save();
            }
        }
    }
}
