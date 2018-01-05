using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Models
{
    public class ChartLine
    {
        public List<double?> Points { get; set; }

        public List<Fork> Forks { get; set; }
    }
}
