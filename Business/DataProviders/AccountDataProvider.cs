using Business.DomainModel.Account;
using NHibernate.Criterion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.DataProviders
{
    public class AccountDataProvider : SQLDataProvider<Account>
    {
        public Account GetByEmail(string email)
        {
            return Execute(session =>
            {
                var criteria = session.CreateCriteria<Account>();
                criteria.Add(Restrictions.Eq("Email", email));
                return criteria.UniqueResult<Account>();
            });
        }
    }
}
