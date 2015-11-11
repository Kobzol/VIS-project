using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer
{
    public class IdentifiableObject : IIdentifiable
    {
        public long Id { get; private set; }

        public IdentifiableObject(long id)
        {
            this.Id = id;
        }
    }
}
