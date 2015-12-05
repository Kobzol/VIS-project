using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer
{
    public class Test : IdentifiableObject, ITest
    {
        public string Name { get; private set; }
        public DateTime Date { get; private set; }
        public Dictionary<long, IGrade> Grades { get; private set; }

        public Test(string name, DateTime date, Dictionary<long, IGrade> grades)
        {
            this.Name = name;
            this.Date = date;
            this.Grades = grades;
        }
    }
}
