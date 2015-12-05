using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Repository
{
    public interface IGradeRepository : IRepository<IGrade>
    {
        Dictionary<long, IGrade> FindByTestId(long testId);
    }
}
