using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer
{
    public class Teacher : Person, ITeacher
    {
        public IEnumerable<ISubject> Subjects { get; private set; }

        public Teacher(string name, string surname, DateTime birthDate, string email, IEnumerable<ISubject> subjects) : base(name, surname, birthDate, email)
        {
            this.Subjects = subjects;
        }
    }
}
