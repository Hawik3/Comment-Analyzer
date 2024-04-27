namespace Comment_Analyzer.Model
{
    using LiveChartsCore;
    using LiveChartsCore.SkiaSharpView;
    using LiveChartsCore.SkiaSharpView.Painting;
    using SkiaSharp;

    public static class CommentsTimeline
    {
        public static ISeries[]? CommentsToTimeline(string filePath, int column)
        {

            ClosedXML.Excel.XLCellValue[]? dateTimeArray = ExcelRedactor.GetArrayFromFile(filePath, column);
            if (dateTimeArray == null)
            {
                return null;
            }
            var commentsCount = GetCommentsCount(dateTimeArray, 7);
            return [new LineSeries<int>
            {
                Values = commentsCount,
                Stroke = new SolidColorPaint(SKColors.Blue) { StrokeThickness = 4 },
                GeometryFill = null,
                GeometryStroke = null,
                LineSmoothness = 0.4,

            }];
        }
        private static int[] GetCommentsCount(ClosedXML.Excel.XLCellValue[] dateTimeArray, int days)
        {
            List<DateTime> dates = dateTimeArray.Select(value => DateTime.Parse(value.ToString())).ToList();
            DateTime firstDate = dates.Min();
            DateTime limitDate = firstDate.AddDays(days);
            List<DateTime> filteredDates = dates.Where(d => d <= limitDate).ToList();
            int hours = (days * 24);
            int[] hoursCount = new int[hours]; 
            for (int i = 0; i < filteredDates.Count; i++)
            {
                int hourIndex = GetHourIndex(filteredDates[i], firstDate, hours); 
                hoursCount[hourIndex]++; 
            }

            return hoursCount;

        }
        private static int GetHourIndex(DateTime dateTime, DateTime referenceDate, int hours)
        {
            TimeSpan timeSpan = dateTime - referenceDate;  
            double totalHours = timeSpan.TotalHours; 
            int hourIndex = (int)Math.Floor(totalHours); 
            if (hourIndex < 0)
            {
                hourIndex = 0;
            }
            else if (hourIndex >= hours)
            {
                hourIndex = hours -1;
            }

            return hourIndex;
        }
    }
}
