using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Repository
{
    public interface ISupplementRepository : IRepository<ISupplement>
    {
        IEnumerable<ISupplement> FindBySubjectId(long subjectId);
    }
}
