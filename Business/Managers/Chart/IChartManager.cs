using Business.Models;
using System.Collections.Generic;

namespace Business.Managers.Chart
{
    public interface IChartManager
    {
        List<ChartLine> GetChartLines(PathModel path);
    }
}
