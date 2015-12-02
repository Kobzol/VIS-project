using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer
{
    public class Supplement : IdentifiableObject, ISupplement
    {
        public bool IsHourCanceled { get; private set; }
        public DateTime Date { get; private set; }
        public ITeachingHour Hour { get; private set; }

        public Supplement(bool isHourCanceled, DateTime date, ITeachingHour hour)
        {
            this.IsHourCanceled = isHourCanceled;
            this.Date = date;
            this.Hour = hour;
        }
    }
}
