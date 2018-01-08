using Business.DataProviders;
using Business.DomainModel.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Managers
{
    public class AccountManager : IAccountManager
    {
        public AccountDataProvider Provider = new AccountDataProvider();

        public void Register(Account account)
        {
            //TODO: add password salt
            account.Id = account.Id ?? Guid.NewGuid().ToString();
            Provider.Create(account);
        }

        public Account GetByEmail(string email)
        {
            return Provider.GetByEmail(email);
        }
    }
}
