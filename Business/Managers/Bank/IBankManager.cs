using Business.DomainModel.Active;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Managers
{
    public interface IBankManager : IManager
    {
        Bank Create(Bank bank);

        Bank Update(Bank bank);

        Bank Get(string id);

        void Delete(Bank bank);
    }
}
