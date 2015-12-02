using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Repository
{
    public interface ISubjectRepository
    {
        ISubject Find(long id);
        IEnumerable<ISubject> FindByStudentId(long studentId);
        IEnumerable<ISubject> FindByTeacherId(long teacherId);
    }
}
