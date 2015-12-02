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
        public ISubject Subject { get; private set; }

        public Test(string name, DateTime date, ISubject subject)
        {
            this.Name = name;
            this.Date = date;
            this.Subject = subject;
        }
    }
}
