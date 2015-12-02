using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer
{
    public class Teacher : Person, ITeacher
    {
        public Teacher(string name, string surname, DateTime birthDate, string email) : base(name, surname, birthDate, email)
        {
            
        }
    }
}
