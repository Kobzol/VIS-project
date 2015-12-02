using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer
{
    public class TeachingHour : IdentifiableObject, ITeachingHour
    {
        public int Day { get; private set; }
        public int Order { get; private set; }

        public TeachingHour(int day, int order)
        {
            this.Day = day;
            this.Order = order;
        }
    }
}
