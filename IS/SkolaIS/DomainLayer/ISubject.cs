using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DomainLayer
{
    public interface ISubject : IIdentifiable
    {
        string Name { get; }
        int Year { get; }
        IEnumerable<IAbsence> Absences { get; }
        ISchedule Schedule { get; }
    }
}
