using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer
{
    public class Student : Person, IStudent
    {
        public IClass Class { get; private set; }
        public IEnumerable<ISubject> subjects { get; private set; }

        public Student(string name, string surname, DateTime birthDate, string email, IClass klass, IEnumerable<ISubject> subjects)
            : base(name, surname, birthDate, email)
        {
            this.Class = klass;
            this.subjects = subjects;
        }
    }
}
