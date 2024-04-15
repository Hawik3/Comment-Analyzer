using ClosedXML.Excel;
using Comment_Analyzer.Model.Excel;
using System.Windows;

namespace Comment_Analyzer.Model
{
    public class ExcelRedactor
    {
        public static XLCellValue[]? GetArrayFromFile(string filePath, int column)
        {
            try
            {
                using var workbook = new XLWorkbook(filePath);
                var worksheet = workbook.Worksheets.Worksheet(1);
                var columnName = worksheet.Column(column);
                return columnName.Cells().Select(c => c.Value).ToArray();
            }
            catch (System.IO.IOException ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
            
        }
    }

}
