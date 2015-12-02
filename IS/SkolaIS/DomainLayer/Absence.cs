using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer
{
    public class Absence : IdentifiableObject
    {
        public DateTime Date { get; private set; }
        public AbsenceType Type { get; private set; }
        public bool Excused { get; private set; }

        public Absence(DateTime date, AbsenceType type, bool excused)
        {
            this.Date = date;
            this.Type = type;
            this.Excused = excused;
        }
    }
}
