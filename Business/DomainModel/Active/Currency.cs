using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.DomainModel.Active
{
    [Serializable]
    public class Currency
    {
        public string ccy { get; set; }
        public string base_ccy { get; set; }
        public string buy { get; set; }
        public string sale { get; set; }
    }
}
