using Business.NHIbernate;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.DataProviders
{
    public class SQLDataProvider<T> : IRepository<T>
        where T : class
    {
        SessionHelper sessionHelper = new SessionHelper();

        public void Create(T entity)
        {
            Execute(session =>
            {
                using (var transaction = session.BeginTransaction())
                {
                    session.SaveOrUpdate(entity);
                    transaction.Commit();
                }
            });
        }

        public void Update(T entity)
        {
            Execute(session =>
            {
                using (var transaction = session.BeginTransaction())
                {
                    session.Update(entity);
                    transaction.Commit();
                }
            });
        }

        public void Delete(string id)
        {
            Execute(session =>
            {
                using (var transaction = session.BeginTransaction())
                {
                    var valueToBeRemoved = session.Get<T>(id);
                    if (valueToBeRemoved != null)
                    {
                        session.Delete(valueToBeRemoved);
                        transaction.Commit();
                    }
                }
            });
        }

        public T Get(string id)
        {
            return Execute<T>(session =>
            {
                return session.Get<T>(id);
            });
        }

        protected T Execute<T>(Func<ISession, T> expression)
        {
            using (var session = sessionHelper.OpenSession())
            {
                return expression(session);
            }
        }

        protected bool Execute(Func<ISession, bool> expression)
        {
            using (var session = sessionHelper.OpenSession())
            {
                return expression(session);
            }
        }

        protected void Execute(Action<ISession> expression)
        {
            using (var session = sessionHelper.OpenSession())
            {
                expression(session);
            }
        }
    }
}
