using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainLayer;

namespace DataLayer.DataMapper
{
    public interface IMapper<T> where T : IIdentifiable
    {
        T Find(long id);
    }
}
