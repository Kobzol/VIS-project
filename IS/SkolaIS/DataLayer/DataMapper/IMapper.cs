using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.DataMapper
{
    public interface IMapper<T>
    {
        T Find(long id);
    }
}
