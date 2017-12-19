using Business.DomainModel.Active;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.DataProviders
{
    public class BankDataProvider : SQLDataProvider<Bank>
    {
        public IEnumerable<Bank> GetAll()
        {
            return Execute(session =>
            {
                var criteria = session.CreateCriteria<Bank>();

                return criteria.List<Bank>();
            });
        }
    }
}
