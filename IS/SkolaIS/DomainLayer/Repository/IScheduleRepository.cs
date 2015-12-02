using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Repository
{
    public interface IScheduleRepository
    {
        ISchedule Find(long id);
    }
}
