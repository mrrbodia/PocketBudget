using System.Collections.Generic;

namespace Business
{
    public class ChartLine
    {
        public ChartLine(string type, List<decimal?> points, bool isActive = true)
        {
            Type = type;
            Points = new List<decimal?>(points);
            IsActive = isActive;
        }

        public bool IsActive { get; set; }

        public string Type { get; set; }

        public List<decimal?> Points { get; set; }
    }
}