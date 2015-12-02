using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Repository
{
    public interface IGradeRepository
    {
        IGrade Find(long id);
        IEnumerable<IGrade> FindByTestId(long testId);
    }
}
