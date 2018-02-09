using System.Collections.Generic;

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