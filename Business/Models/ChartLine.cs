using System;
using System.Collections.Generic;

namespace Business
{
    public class ChartLine
    {
        public ChartLine(string type, List<decimal?> points, bool isHidden = true)
        {
            Id = Guid.NewGuid();
            Type = type;
            Points = new List<decimal?>(points);
            IsHidden = isHidden;
        }

        public Guid Id { get; set; }

        public bool IsHidden { get; set; }

        public string Type { get; set; }

        public List<decimal?> Points { get; set; }
    }
}