using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Business
{
    public class ChartLine
    {
        public ChartLine(string type, List<decimal?> points)
        {
            Type = type;
            Points = new List<decimal?>(points);
        }

        public string Type { get; set; }

        public List<decimal?> Points { get; set; }
    }
}