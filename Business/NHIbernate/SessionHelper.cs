using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.NHIbernate
{
    public class SessionHelper
    {
        /// <summary>
        /// NHibernate Helper
        /// </summary>
        private NHibernateHelper _nHibernateHelper = null;

        public SessionHelper()
        {
            _nHibernateHelper = new NHibernateHelper();
        }

        /// <summary>
        /// Retrieve the current ISession.
        /// </summary>
        public ISession Current
        {
            get
            {
                return _nHibernateHelper.GetCurrentSession();
            }
        }

        /// <summary>
        /// Create an ISession.
        /// </summary>
        public void CreateSession()
        {
            _nHibernateHelper.CreateSession();
        }

        /// <summary>
        /// Clear an ISession.
        /// </summary>
        public void ClearSession()
        {
            Current.Clear();
        }

        /// <summary>
        /// Open an ISession.
        /// </summary>
        public ISession OpenSession()
        {
            return _nHibernateHelper.OpenSession();
        }

        /// <summary>
        /// Close an ISession.
        /// </summary>
        public void CloseSession()
        {
            _nHibernateHelper.CloseSession();
        }
    }
}
