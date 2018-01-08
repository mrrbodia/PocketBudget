using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.DomainModel.Active
{
    public class Bank
    {
        public virtual string Id { get; set; }

        public virtual string Name { get; set; }

        public virtual short Rating { get; set; }

        public virtual IEnumerable<Deposit> Deposits { get; set; }
    }
}
