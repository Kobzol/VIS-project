using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer
{
    public class Schedule : IdentifiableObject, ISchedule
    {
        public IEnumerable<ITeachingHour> Hours { get; private set; }

        public Schedule(IEnumerable<ITeachingHour> hours)
        {
            this.Hours = hours;
        }
    }
}
