using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DomainLayer
{
    public interface IPerson
    {
        string Name { get; }
        string Surname { get; }
        DateTime BirthDate { get; }
        string Email { get; }
    }
}
