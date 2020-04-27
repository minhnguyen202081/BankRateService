using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Data;
using System.Reflection;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml;
using System.IO;

namespace ExportToExcel
{
    //
    //  March 2012
    //  http://www.mikesknowledgebase.com
    // 
    public class CreateExcelFile
    {
        //public static bool CreateExcelDocument<T>(List<T> list, string xlsxFilePath)
        //{
        //    DataSet ds = new DataSet();
        //    ds.Tables.Add(ListToDataTable(list));

        //    return CreateExcelDocument(ds, xlsxFilePath);
        //}

        //  This function is taken from: http://www.codeguru.com/forum/showthread.php?t=450171
        public static DataTable ListToDataTable<T>(List<T> list)
        {
            DataTable dt = new DataTable();

            foreach (PropertyInfo info in typeof(T).GetProperties())
            {
                dt.Columns.Add(new DataColumn(info.Name, info.PropertyType));
            }
            foreach (T t in list)
            {
                DataRow row = dt.NewRow();
                foreach (PropertyInfo info in typeof(T).GetProperties())
                {
                    row[info.Name] = info.GetValue(t, null);
                }
                dt.Rows.Add(row);
            }
            return dt;
        }

        //public static bool CreateExcelDocument(DataTable dt, string xlsxFilePath)
        //{
        //    DataSet ds = new DataSet();
        //    ds.Tables.Add(dt);

        //    return CreateExcelDocument(dt, xlsxFilePath);
        //}

        public static bool CreateExcelDocument(DataTable dt, string excelFilename)
        {
            try
            {
                using (SpreadsheetDocument document = SpreadsheetDocument.Create(excelFilename, SpreadsheetDocumentType.Workbook))
                {
                    CreateParts(dt, document);     
                }
                
                Trace.WriteLine("Successfully created: " + excelFilename);
                return true;
            }
            catch (Exception ex)
            {
                Trace.WriteLine("Failed, exception thrown: " + ex.Message);
                return false;
            }
        }

        private static void CreateParts(DataTable dt, SpreadsheetDocument document)
        {
            WorkbookPart workbookPart = document.AddWorkbookPart();
            Workbook workbook = new Workbook();
            workbookPart.Workbook = workbook;

            //  If we don't add a "WorkbookStylesPart", OLEDB will refuse to connect to this .xlsx file !
            WorkbookStylesPart workbookStylesPart = workbookPart.AddNewPart<WorkbookStylesPart>("rIdStyles");
            Stylesheet stylesheet = new Stylesheet();
            workbookStylesPart.Stylesheet = stylesheet;

            Sheets sheets = new Sheets();

            //  Loop through each of the DataTables in our DataSet, and create a new Excel Worksheet for each.
            uint worksheetNumber = 1;
           // foreach (DataTable dt in ds.Tables)
           // {
                //  For each worksheet you want to create
                string workSheetID = "rId" + worksheetNumber.ToString();
                string worksheetName ="Table 1" ;

                WorksheetPart worksheetPart = workbookPart.AddNewPart<WorksheetPart>(workSheetID);
                WriteDataTableToExcelWorksheet(dt, worksheetPart);

                Sheet sheet = new Sheet() { Name = worksheetName, SheetId = (UInt32Value)worksheetNumber, Id = workSheetID };
                sheets.Append(sheet);

                worksheetNumber++;
           // }

            workbook.Append(sheets);
        }

        private static void WriteDataTableToExcelWorksheet(DataTable dt, WorksheetPart worksheetPart1)
        {
            Worksheet worksheet = new Worksheet();
            SheetViews sheetViews = new SheetViews();

            SheetView sheetView = new SheetView() { TabSelected = true, WorkbookViewId = (UInt32Value)0U };
            sheetViews.Append(sheetView);

            SheetData sheetData1 = new SheetData();
            string cellValue = "";

            //  Create a Header Row in our Excel file, containing one header for each Column of data in our DataTable.
            //
            //  We'll also create an array, showing which type each column of data is (Text or Numeric), so when we come to write the actual
            //  cells of data, we'll know if to write Text values or Numeric cell values.
            int numberOfColumns = dt.Columns.Count;
            bool[] IsNumericColumn = new bool[numberOfColumns];

            string[] excelColumnNames = new string[numberOfColumns];
            for (int n = 0; n < numberOfColumns; n++)
                excelColumnNames[n] = GetExcelColumnName(n);

            //
            //  Create the Header row in our Excel Worksheet
            //
            int rowIndex = 1;
            Row row1 = new Row() { RowIndex = (UInt32Value)1U };
            for (int colInx = 0; colInx < numberOfColumns; colInx++)
            {
                DataColumn col = dt.Columns[colInx];

                AppendTextCell(excelColumnNames[colInx] + "1", col.ColumnName, row1);
                IsNumericColumn[colInx] = (col.DataType.FullName == "System.Decimal");     //  eg "System.String" or "System.Decimal"
            }
            sheetData1.Append(row1);


            //
            //  Now, step through each row of data in our DataTable...
            //
            List<DataTable> dts = SplitTableIntoMultiTables(dt, 100);
            foreach (DataTable dta in dts)
            {

                foreach (DataRow dr in dta.Rows)
                {
                    // ...create a new row, and append a set of this row's data to it.
                    ++rowIndex;
                    Row newExcelRow = new Row() { RowIndex = (UInt32Value)(uint)rowIndex };

                    for (int colInx = 0; colInx < numberOfColumns; colInx++)
                    {
                        cellValue = dr.ItemArray[colInx].ToString();

                        // Create cell with data
                        if (IsNumericColumn[colInx] && !string.IsNullOrEmpty(cellValue))
                            AppendNumericCell(excelColumnNames[colInx] + rowIndex.ToString(), cellValue, newExcelRow);
                        else
                            AppendTextCell(excelColumnNames[colInx] + rowIndex.ToString(), cellValue, newExcelRow);
                    }
                    sheetData1.Append(newExcelRow);
                    newExcelRow = null;


                }
                dta.Clear();
                dta.Dispose();
                
            }
            
            worksheet.Append(sheetViews);
            worksheet.Append(sheetData1);

            worksheetPart1.Worksheet = worksheet;
        }

        private static void WriteRandomValuesSAX(string filename, int numRows, int numCols)
        {
            using (SpreadsheetDocument myDoc = SpreadsheetDocument.Open(filename, true))
            {
                WorkbookPart workbookPart = myDoc.WorkbookPart;
                WorksheetPart worksheetPart = workbookPart.WorksheetParts.First();
                string origninalSheetId = workbookPart.GetIdOfPart(worksheetPart);

                WorksheetPart replacementPart =
                workbookPart.AddNewPart<WorksheetPart>();
                string replacementPartId = workbookPart.GetIdOfPart(replacementPart);

                OpenXmlReader reader = OpenXmlReader.Create(worksheetPart);
                OpenXmlWriter writer = OpenXmlWriter.Create(replacementPart);

                Row r = new Row();
                Cell c = new Cell();
                CellFormula f = new CellFormula();
                f.CalculateCell = true;
                f.Text = "RAND()";
                c.Append(f);
                CellValue v = new CellValue();
                c.Append(v);

                while (reader.Read())
                {
                    if (reader.ElementType == typeof(SheetData))
                    {
                        if (reader.IsEndElement)
                            continue;
                        writer.WriteStartElement(new SheetData());

                        for (int row = 0; row < numRows; row++)
                        {
                            writer.WriteStartElement(r);
                            for (int col = 0; col < numCols; col++)
                            {
                                writer.WriteElement(c);
                            }
                            writer.WriteEndElement();
                        }

                        writer.WriteEndElement();
                    }
                    else
                    {
                        if (reader.IsStartElement)
                        {
                            writer.WriteStartElement(reader);
                        }
                        else if (reader.IsEndElement)
                        {
                            writer.WriteEndElement();
                        }
                    }
                }
                reader.Close();
                writer.Close();

                Sheet sheet = workbookPart.Workbook.Descendants<Sheet>()
                .Where(s => s.Id.Value.Equals(origninalSheetId)).First();
                sheet.Id.Value = replacementPartId;
                workbookPart.DeletePart(worksheetPart); 
            }
        }

        public static void CreateCSVFile(DataTable dt, string strFilePath, string Delimiter)
        {
            

            // Create the CSV file to which grid data will be exported.

            StreamWriter sw = new StreamWriter(strFilePath, false,Encoding.UTF8);

            // First we will write the headers.

            //DataTable dt = m_dsProducts.Tables[0];

            int iColCount = dt.Columns.Count;
            bool flag = false;
            for (int i = 0; i < iColCount; i++)
            {
                if (dt.Columns[i].ToString() != "#")
                {
                    sw.Write(dt.Columns[i]);
                }
                else
                {
                    flag = true;
                }

                if (i < iColCount - 1)
                {

                    sw.Write(Delimiter);

                }

            }
            if (flag != true)
            {
                sw.Write(sw.NewLine);
            }
            // Now write all the rows.
            int count=1;
            foreach (DataRow dr in dt.Rows)
            {

                for (int i = 0; i < iColCount; i++)
                {

                    if (!Convert.IsDBNull(dr[i]))
                    {

                        sw.Write(dr[i].ToString().Replace("\n","CHAR(10)" ));
                  

                    }
                    //else
                    //{
                    //    sw.Write(" " + Delimiter);
                    //}

                    if (i < iColCount - 1)
                    {

                        sw.Write(Delimiter);

                    }

                }
                count ++;
                sw.Write(sw.NewLine);
                if (count == 10000)
                {
                    sw.Flush();
                    count = 1;
                }
            }
            sw.Close();

          

        }

        private static List<DataTable> SplitTableIntoMultiTables(DataTable dt, int rows)
        {
           
            int i,currentRow = 0;
            List <DataTable> dts = new List<DataTable>();
            while (currentRow + rows < dt.Rows.Count)
            {
                DataTable otherTable = new DataTable();
                otherTable = dt.Clone();
                if (currentRow + rows <= dt.Rows.Count)
                {
                    for (i = currentRow; i < currentRow + rows; i++)
                    {
                        otherTable.ImportRow(dt.Rows[i]); //Imports (copies) the row from the original table to the new one
                        dt.Rows[i].Delete(); //Marks row for deletion
                    }
                    currentRow = i + 1;
                    dt.AcceptChanges();
                    dts.Add(otherTable);
                }
            }
                 DataTable otherTable1 = dt.Clone();
                    for (i = 0; i <= dt.Rows.Count -1 ; i++)
                    {
                        otherTable1.ImportRow(dt.Rows[i]); //Imports (copies) the row from the original table to the new one
                        dt.Rows[i].Delete(); //Marks row for deletion
                    }
                    
                    dt.AcceptChanges();
                    dts.Add(otherTable1);
                    return dts;
            
          
        }
        private static void AppendTextCell(string cellReference, string cellStringValue, Row excelRow)
        {
            //  Add a new Excel Cell to our Row 
            Cell cell = new Cell() { CellReference = cellReference, DataType = CellValues.String };
            CellValue cellValue = new CellValue();
            cellValue.Text = cellStringValue;
            cell.Append(cellValue);
            excelRow.Append(cell);
        }

        private static void AppendNumericCell(string cellReference, string cellStringValue, Row excelRow)
        {
            //  Add a new Excel Cell to our Row 
            Cell cell = new Cell() { CellReference = cellReference };
            CellValue cellValue = new CellValue();
            cellValue.Text = cellStringValue;
            cell.Append(cellValue);
            excelRow.Append(cell);
        }

        private static string GetExcelColumnName(int columnIndex)
        {
            //  Convert a zero-based column index into an Excel column reference (A, B, C.. Y, Y, AA, AB, AC... AY, AZ, B1, B2..)
            //  Each Excel cell we write must have the cell name stored with it.
            //
            if (columnIndex < 26)
                return ((char)('A' + columnIndex)).ToString();

            char firstChar = (char)('A' + (columnIndex / 26) - 1);
            char secondChar = (char)('A' + (columnIndex % 26));

            return string.Format("{0}{1}", firstChar, secondChar);
        }
    }
}
