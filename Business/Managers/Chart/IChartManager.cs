using Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Managers.Chart
{
    public interface IChartManager
    {
        List<List<decimal>> GetChartLines(PathModel path);
    }
}
