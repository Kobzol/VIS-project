using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer
{
    [Serializable]
    public class Person : IdentifiableObject, IPerson
    {
        public string Name { get; set; }
        public string Surname { get; private set; }
        public DateTime BirthDate { get; private set; }
        public string Email { get; private set; }

        public Person(string name, string surname, DateTime birthDate, string email)
        {
            this.Name = name;
            this.Surname = surname;
            this.BirthDate = birthDate;
            this.Email = email;
        }
    }
}
