using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Repository
{
    public interface ITestRepository : IRepository<ITest>
    {
        IEnumerable<ITest> FindBySubjectId(long subjectId);
    }
}
