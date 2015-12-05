using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer
{
    public class Subject : IdentifiableObject, ISubject
    {
        public string Name { get; private set; }
        public int Year { get; private set; }
        public IEnumerable<IAbsence> Absences { get; private set; }
        public ISchedule Schedule { get; private set; }
        public IEnumerable<ITest> Tests { get; private set; }

        public Subject(string name, int year, IEnumerable<IAbsence> absences, ISchedule schedule, IEnumerable<ITest> tests)
        {
            this.Name = name;
            this.Year = year;
            this.Absences = absences;
            this.Schedule = schedule;
            this.Tests = tests;
        }
    }
}
