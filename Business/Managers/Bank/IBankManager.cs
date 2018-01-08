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
        void Create(Bank bank);

        void Update(Bank bank);

        Bank Get(string id);

        IEnumerable<Bank> GetAll();

        void Delete(string id);
    }
}
