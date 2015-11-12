using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer
{
    [Serializable]
    public class Person : IdentifiableObject
    {
        public Person(long id) : base(id)
        {

        }

        public long Test()
        {
            return this.Id;
        }
    }
}
