namespace Comment_Analyzer.Model
{
    using LiveChartsCore;
    using LiveChartsCore.SkiaSharpView;
    using LiveChartsCore.SkiaSharpView.Painting;
    using SkiaSharp;

    public class CommentsTimeline
    {
        public static ISeries[]? CommentsToTimeline(string filePath, int column)
        {
            // RENAME VALUES NAME!!!!!!!!!!
            ClosedXML.Excel.XLCellValue[]? dateTimeArray = ExcelRedactor.GetArrayFromFile(filePath, column);
            if(dateTimeArray == null)
            {
                return null;
            }
            var val = GetCommentsCount(dateTimeArray);
            return [new LineSeries<int>
                     {
                        Values = val.Values,Stroke = new SolidColorPaint(SKColors.Blue) { StrokeThickness = 4 },
                        GeometryFill = null,
                         GeometryStroke = null 
                     }];

            
            
        }
        private static Dictionary<DateTime, int> GetCommentsCount(ClosedXML.Excel.XLCellValue[] dateTimeArray)
        {
            List<string> str = [];
            foreach (ClosedXML.Excel.XLCellValue strs in dateTimeArray)
            {
                str.Add(strs.ToString());
            }
            List<DateTime> dates = [.. str.Select(DateTime.Parse)];
            DateTime firstDate = dates.Min();
            var limitDate = firstDate.AddDays(30);
            List<DateTime> filteredDates = dates.Where(d => d <= limitDate).ToList();
            var firstDates = new DateTime(firstDate.Year, firstDate.Month, firstDate.Day, firstDate.Hour, 0, 0);
            Dictionary<DateTime, int> commentsCount = [];
            for (int i = 0; i < 721; i++)
            {
                commentsCount.Add(firstDates.AddHours(i), 0);
            }
            foreach (var date in filteredDates)
            {
                commentsCount[new DateTime(date.Year, date.Month, date.Day, date.Hour, 0, 0)]++;
            }
            return commentsCount;
        }
    }
}
