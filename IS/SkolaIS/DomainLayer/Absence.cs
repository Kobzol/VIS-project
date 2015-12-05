using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer
{
    public class Absence : IdentifiableObject, IAbsence
    {
        public DateTime Date { get; private set; }
        public AbsenceType Type { get; private set; }
        public bool Excused { get; private set; }
        public IPerson Student { get; private set; }
        public ITeachingHour Hour { get; private set; }

        public Absence(DateTime date, AbsenceType type, bool excused, IPerson student, ITeachingHour hour)
        {
            this.Date = date;
            this.Type = type;
            this.Excused = excused;
            this.Student = student;
            this.Hour = hour;
        }
    }
}
