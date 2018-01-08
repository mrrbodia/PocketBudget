using Business.DataProviders;
using Business.DomainModel.Active;
using System;
using System.Collections.Generic;

namespace Business.Managers
{
    public class BankManager : IBankManager
    {
        public BankDataProvider Provider = new BankDataProvider();

        public void Create(Bank bank)
        {
            Provider.Create(bank);
        }

        public void Update(Bank bank)
        {
            Provider.Update(bank);
        }

        public Bank Get(string id)
        {
            return Provider.Get(id);
        }

        public void Delete(string id)
        {
            Provider.Delete(id);
        }

        public IEnumerable<Bank> GetAll()
        {
            return Provider.GetAll();
        }
    }
}
