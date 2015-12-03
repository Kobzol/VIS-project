using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Repository
{
    public interface IAbsenceRepository : IRepository<IAbsence>
    {
        IEnumerable<IAbsence> FindBySubjectId(long subjectId);
    }
}
