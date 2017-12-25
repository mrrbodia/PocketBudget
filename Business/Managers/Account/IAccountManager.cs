using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.DomainModel.Account;

namespace Business.Managers
{
    public interface IAccountManager: IManager
    {
        void Register(Account account);

        Account GetByEmail(string email);
    }
}
