﻿using Business.DataProviders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Managers
{
    public class BankManager : IBankManager
    {
        public BankDataProvider Provider { get; set; }

        public DomainModel.Active.Bank Create(DomainModel.Active.Bank bank)
        {
            throw new NotImplementedException();
        }

        public DomainModel.Active.Bank Update(DomainModel.Active.Bank bank)
        {
            throw new NotImplementedException();
        }

        public DomainModel.Active.Bank Get(string id)
        {
            throw new NotImplementedException();
        }

        public void Delete(DomainModel.Active.Bank bank)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<DomainModel.Active.Bank> GetAll()
        {
            throw new NotImplementedException();
        }
    }
}
