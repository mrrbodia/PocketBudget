using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.DomainModel.Account
{
    public class Account
    {
        public virtual string Id { get; set; }

        public virtual string Email { get; set; }

        public virtual string Password { get; set; }

        public virtual Role Role { get; set; }
    }
}
