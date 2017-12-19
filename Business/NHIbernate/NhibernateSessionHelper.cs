using NHibernate;
using NHibernate.Cfg;
using NHibernate.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.NHIbernate
{
    public class NhibernateSessionHelper
    {
        private static readonly ISessionFactory sessionFactory;

        static NhibernateSessionHelper()
        {
            var configuration = new Configuration().Configure();
            sessionFactory = configuration.BuildSessionFactory();
        }

        public static ISession OpenSession()
        {
            if (CurrentSessionContext.HasBind(sessionFactory))
            {
                var result = sessionFactory.GetCurrentSession();
                if (result.IsOpen)
                    return result;

                Unbind();
            }

            var session = sessionFactory.OpenSession();
            CurrentSessionContext.Bind(session);
            return session;
        }

        public static void Unbind()
        {
            CurrentSessionContext.Unbind(sessionFactory);
        }
    }
}
