using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Repository
{
    public interface IRepository<T> where T : IIdentifiable
    {
        T Find(long id);
        void Update(T t);
        void Delete(T t);
    }
}
