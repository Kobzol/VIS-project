﻿using System;
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
        public IEnumerable<ITest> Tests { get; private set; }
        public IEnumerable<IAbsence> Absences { get; private set; }
        public IEnumerable<ISchedule> Schedule { get; private set; }

        public Subject(string name, int year, IEnumerable<ITest> tests, IEnumerable<IAbsence> absences, IEnumerable<ISchedule> schedule)
        {
            this.Name = name;
            this.Year = year;
            this.Tests = tests;
            this.Absences = absences;
            this.Schedule = schedule;
        }
    }
}
