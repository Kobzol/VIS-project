using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer
{
    public class Person : IdentifiableObject, IPerson
    {
        public string Name { get; private set; }
        public string Surname { get; private set; }
        public DateTime BirthDate { get; private set; }
        public string Email { get; private set; }
        public PersonRole Role { get; private set; }
        public IEnumerable<ISubject> Subjects { get; private set; }
        public IClass Class { get; private set; }

        public Person(string name, string surname, DateTime birthDate, string email, PersonRole role, IEnumerable<ISubject> subjects, IClass klass)
        {
            this.Name = name;
            this.Surname = surname;
            this.BirthDate = birthDate;
            this.Email = email;
            this.Role = role;
            this.Subjects = subjects;
            this.Class = klass;
        }

        public bool IsStudent()
        {
            return this.Role == PersonRole.Student;
        }
        public bool IsTeacher()
        {
            return this.Role == PersonRole.Teacher;
        }
    }
}
