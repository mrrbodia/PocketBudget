using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.DomainModel.Cost
{
    public interface IAdditionalCost
    {
        short From { get; set; }

        short To { get; set; }

        decimal GetCostPerYear();
    }
}
