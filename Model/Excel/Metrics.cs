using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comment_Analyzer.Model.Excel
{
   
    public class ExcelSentimentTable
    {
        public float Score {  get; set; }
        public string? CommentText { get; set; }
    }
}
