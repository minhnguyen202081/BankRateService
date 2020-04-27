using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using Oracle.DataAccess.Client;
using System.Diagnostics;

namespace ExportToExcel
{
    public static class ExportExcel
    {
        static private int rowsPerSheet = 5000;
     
        public static bool ExportExcelFile(DataTableReader reader, bool firstTime)
        {
            

                int c = 0;
             
                DataTable ResultsData = new DataTable();
                //Get the Columns names, types, this will help
                //when we need to format the cells in the excel sheet.
                DataTable dtSchema = reader.GetSchemaTable();
                var listCols = new List<DataColumn>();
                if (dtSchema != null)
                {
                    foreach (DataRow drow in dtSchema.Rows)
                    {
                        string columnName = Convert.ToString(drow["ColumnName"]);
                        var column = new DataColumn(columnName, (Type)(drow["DataType"]));
                        column.Unique = (bool)drow["IsUnique"];
                        column.AllowDBNull = (bool)drow["AllowDBNull"];
                        column.AutoIncrement = (bool)drow["IsAutoIncrement"];
                        listCols.Add(column);
                        ResultsData.Columns.Add(column);
                    }
                }

                // Call Read before accessing data. 
                while (reader.Read())
                {
                    DataRow dataRow = ResultsData.NewRow();
                    for (int i = 0; i < listCols.Count; i++)
                    {
                        dataRow[(listCols[i])] = reader[i];
                    }
                    ResultsData.Rows.Add(dataRow);
                    c++;
                    if (c == rowsPerSheet)
                    {
                        c = 0;
                        ExportToOxml(firstTime, ResultsData);
                        ResultsData.Clear();
                        firstTime = false;
                    }
                }
                if (ResultsData.Rows.Count > 0)
                {
                    ExportToOxml(firstTime, ResultsData);
                    ResultsData.Clear();
                }
                // Call Close when done reading.
                reader.Close();
                return firstTime;
            }


        private static void ExportToOxml(bool firstTime, DataTable ResultsData)
        {
            const string fileName = @"D:\MyExcel.xlsx";

            //Delete the file if it exists. 
            if (firstTime && File.Exists(fileName))
            {
                File.Delete(fileName);
            }

            uint sheetId = 1; //Start at the first sheet in the Excel workbook.
          
            if (firstTime)
            {
                //This is the first time of creating the excel file and the first sheet.
                // Create a spreadsheet document by supplying the filepath.
                // By default, AutoSave = true, Editable = true, and Type = xlsx.
                SpreadsheetDocument spreadsheetDocument = SpreadsheetDocument.
                    Create(fileName, SpreadsheetDocumentType.Workbook);

                // Add a WorkbookPart to the document.
                WorkbookPart workbookpart = spreadsheetDocument.AddWorkbookPart();
                workbookpart.Workbook = new Workbook();

                // Add a WorksheetPart to the WorkbookPart.
                var worksheetPart = workbookpart.AddNewPart<WorksheetPart>();
                var sheetData = new SheetData();
                worksheetPart.Worksheet = new Worksheet(sheetData);


                var bold1 = new Bold();
                CellFormat cf = new CellFormat();


                // Add Sheets to the Workbook.
                Sheets sheets;
                sheets = spreadsheetDocument.WorkbookPart.Workbook.
                    AppendChild<Sheets>(new Sheets());

                // Append a new worksheet and associate it with the workbook.
                var sheet = new Sheet()
                {
                    Id = spreadsheetDocument.WorkbookPart.
                        GetIdOfPart(worksheetPart),
                    SheetId = sheetId,
                    Name = "Sheet" + sheetId
                };
                sheets.Append(sheet);

                //Add Header Row.
                var headerRow = new Row();
                foreach (DataColumn column in ResultsData.Columns)
                {
                    var cell = new Cell
                    {
                        DataType = CellValues.String,
                        CellValue = new CellValue(column.ColumnName)
                    };
                    headerRow.AppendChild(cell);
                }
                sheetData.AppendChild(headerRow);

                foreach (DataRow row in ResultsData.Rows)
                {
                    var newRow = new Row();
                    foreach (DataColumn col in ResultsData.Columns)
                    {
                        var cell = new Cell
                        {
                            DataType = CellValues.String,
                            CellValue = new CellValue(row[col].ToString())
                        };
                        newRow.AppendChild(cell);
                    }

                    sheetData.AppendChild(newRow);
                }
                workbookpart.Workbook.Save();

                spreadsheetDocument.Close();
                sheetData.Remove();
                spreadsheetDocument.Dispose();
                //Process currentProcess = System.Diagnostics.Process.GetCurrentProcess();
                //long totalBytesOfMemoryUsed = currentProcess.WorkingSet64;
               // WriteEventLog("Before: " + totalBytesOfMemoryUsed.ToString());
                GC.Collect();
                GC.WaitForPendingFinalizers();

               // currentProcess = System.Diagnostics.Process.GetCurrentProcess();
               //totalBytesOfMemoryUsed = currentProcess.WorkingSet64;
               // WriteEventLog("After: " + totalBytesOfMemoryUsed.ToString());
            }
            else
            {
                // Open the Excel file that we created before, and start to add sheets to it.
                var spreadsheetDocument = SpreadsheetDocument.Open(fileName, true);

                var workbookpart = spreadsheetDocument.WorkbookPart;
                //if (workbookpart.Workbook == null)
                //    workbookpart.Workbook = new Workbook();
                var sheet = workbookpart.Workbook.Sheets.First();

               Worksheet worksheet1 =workbookpart.WorksheetParts.First().Worksheet;
               SheetData sheetData = (SheetData)worksheet1.First();
           
                var worksheetPart = workbookpart.WorksheetParts;// AddNewPart<WorksheetPart>();
              //  var sheetData = new SheetData();
                //var worksheet = worksheetPart.First();
                //worksheet.Worksheet = new Worksheet(sheetData);
                //var sheets = spreadsheetDocument.WorkbookPart.Workbook.Sheets;
              
                //if (sheets.Elements<Sheet>().Any())
                //{
                //    //Set the new sheet id
                //    sheetId = sheets.Elements<Sheet>().Max(s => s.SheetId.Value) + 1;
                //}
                //else
                //{
                //    sheetId = 1;
                //}

                //// Append a new worksheet and associate it with the workbook.
                //var sheet = new Sheet()
                //{
                //    Id = spreadsheetDocument.WorkbookPart.
                //        GetIdOfPart(worksheetPart),
                //    SheetId = sheetId,
                //    Name = "Sheet" + sheetId
                //};
                //sheets.Append(sheet);

                ////Add the header row here.
                //var headerRow = new Row();

                //foreach (DataColumn column in ResultsData.Columns)
                //{
                //    var cell = new Cell
                //    {
                //        DataType = CellValues.String,
                //        CellValue = new CellValue(column.ColumnName)
                //    };
                //    headerRow.AppendChild(cell);
                //}
                //sheetData.AppendChild(headerRow);

                foreach (DataRow row in ResultsData.Rows)
                {
                    var newRow = new Row();

                    foreach (DataColumn col in ResultsData.Columns)
                    {
                        var cell = new Cell
                        {
                            DataType = CellValues.String,
                            CellValue = new CellValue(row[col].ToString())
                        };
                        newRow.AppendChild(cell);
                    }

                    sheetData.InsertAfter(newRow,sheetData.LastChild);
                }

                workbookpart.Workbook.Save();

                // Close the document.
                spreadsheetDocument.Close();
                sheetData.Remove();
                spreadsheetDocument.Dispose();
                //Process currentProcess = System.Diagnostics.Process.GetCurrentProcess();
                //long totalBytesOfMemoryUsed = currentProcess.WorkingSet64;
                //WriteEventLog("Before: " + totalBytesOfMemoryUsed.ToString());
                GC.Collect();
                GC.WaitForPendingFinalizers();

                //currentProcess = System.Diagnostics.Process.GetCurrentProcess();
                //totalBytesOfMemoryUsed = currentProcess.WorkingSet64;
                //WriteEventLog("After: " + totalBytesOfMemoryUsed.ToString());
            }
        }

        private static void WriteEventLog(string sEvent)
        {
            string sSource;
            string sLog;
           

            sSource = "dotNETSampleApp";
            sLog = "Application";
          
            EventLog m_EventLog = new EventLog("");
            m_EventLog.Source = "MySampleEventLog";
            if (!EventLog.SourceExists(sSource))
                EventLog.CreateEventSource(sSource, sLog);

            EventLog.WriteEntry(sSource, sEvent);
        }
    }
}