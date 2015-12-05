using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DomainLayer
{
    public interface IAbsence : IIdentifiable
    {
        DateTime Date { get; }
        AbsenceType Type { get; }
        bool Excused { get; }
        long Student { get; }
        ITeachingHour Hour { get; }
    }
}
