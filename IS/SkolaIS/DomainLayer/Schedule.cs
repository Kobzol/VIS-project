using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer
{
    public class Schedule : IdentifiableObject, ISchedule
    {
        public IEnumerable<ISupplement> Supplements { get; private set; }
        public IEnumerable<ITeachingHour> Hours { get; private set; }

        public Schedule(IEnumerable<ISupplement> supplements, IEnumerable<ITeachingHour> hours)
        {
            this.Supplements = supplements;
            this.Hours = hours;
        }
    }
}
