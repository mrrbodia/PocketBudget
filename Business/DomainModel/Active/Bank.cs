using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.DomainModel.Active
{
    public class Bank
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public short Rating { get; set; }

        public IEnumerable<Deposit> Deposits { get; set; }
    }
}
