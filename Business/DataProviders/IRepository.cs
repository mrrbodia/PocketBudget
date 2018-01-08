using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.DataProviders
{
    public interface IRepository<T>
        where T : class
    {
        void Create(T entity);
        void Update(T entity);
        void Delete(string id);
        T Get(string id);
    }
}
