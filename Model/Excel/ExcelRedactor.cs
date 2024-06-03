using ClosedXML.Excel;
using System.Windows;

namespace Comment_Analyzer.Model
{
    public class ExcelRedactor
    {
        public static List<string>? GetArrayFromFile(string filePath, int column = 7)
        {
            try
            {
                using var workbook = new XLWorkbook(filePath);
                var worksheet = workbook.Worksheets.Worksheet(1);
                var columnName = worksheet.Column(column);
                var stringList = columnName.Cells()
                               .Select(c => c.Value.ToString())
                               .ToList();

                return stringList;
            }
            catch (System.IO.IOException)
            {
                throw;
            }
           
        }
    }

}
