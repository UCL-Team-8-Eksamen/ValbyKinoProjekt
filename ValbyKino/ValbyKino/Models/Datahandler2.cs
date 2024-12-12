//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using DocumentFormat.OpenXml.Spreadsheet;
//using DocumentFormat.OpenXml.Wordprocessing;
//using Microsoft.Office.Interop.Excel;
//using Excel = Microsoft.Office.Interop.Excel;



//namespace ValbyKino.Models
//{
//    public class Datahandler2
//    {
//        public Datahandler2() { }
//        public void writeExcelTest()
//        {
//            string filePath = "ExcelTest.xlsx";
//            Microsoft.Office.Interop.Excel.Application excel = new Microsoft.Office.Interop.Excel.Application();
//            Workbook wb;
//            Worksheet ws;

//            wb = excel.Workbooks.Open(filePath);
//            ws = wb.Worksheets[1];

//            Range cellRange = ws.Range["A1:A1"];
//            cellRange.Value = "1. ORIGINAL TITLE";

//            wb.SaveAs(filepath);
//            wb.close();
//        }

//        public void writeExcelTest2()
//        {
//            string filePath = "ExcelTest2.xlsx";
//            Microsoft.Office.Interop.Excel.Application excel = new Microsoft.Office.Interop.Excel.Application();
//            Workbook wb;
//            Worksheet ws;

//            wb = excel.Workbooks.Open(filePath);
//            ws = wb.Worksheets[1];

//            Range cellRange = ws.Range["A1:P1"];
//            cellRange.Value = "1.ORIGINAL TITLE", "2.LOCAL TITLE",  "3.DIRECTOR'S FIRST NAME", "4. DIRECTOR'S LAST NAME", "5.FILM'S MAIN NATIONALITY", "6. NATIONAL RELEASE DATE", "7. 1st DATE OF RELEASE IN YOUR CINEMA", "8. VO/DB/ST," "9. SCREENING FORMAT", "10. 3D", "11. ALTERNATIVE CONTENT", "12. NB OF WEEKS", "13. TOTAL SCREENINGS", "14. ADMISSIONS", "15. BOX OFFICE IN LOCAL CURRENCY", "16. YA";

//            wb.SaveAs(filepath);
//            wb.close();
//        }

//        public void ConvertToExcel(string filePath)
//        {
//            Application app = new Application();
//            Workbook wb = app.Workbooks.Open(filePath, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
//            wb.SaveAs(@"testcsv.xlsx", XlFileFormat.xlOpenXMLWorkbook, Type.Missing, Type.Missing, Type.Missing, Type.Missing, XlSaveAsAccessMode.xlExclusive, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
//            wb.Close();
//            app.Quit();
//        }

//        public void Convert_CSV_To_Excel(string filePath)
//        {

//            // Rename .csv To .xls
//            System.IO.File.Move(filePath, @"Test.csv.xls");

//            var _app = new Excel.Application();
//            var _workbooks = _app.Workbooks;

//            _workbooks.OpenText("Test.csv.xls",
//                                     DataType: Excel.XlTextParsingType.xlDelimited,
//                                     TextQualifier: Excel.XlTextQualifier.xlTextQualifierNone,
//                                     ConsecutiveDelimiter: true,
//                                     Semicolon: true);

//            // Convert To Excle 97 / 2003
//            _workbooks[1].SaveAs("NewTest.xls", Excel.XlFileFormat.xlExcel5);

//            _workbooks.Close();
//        }
//    }
//}
