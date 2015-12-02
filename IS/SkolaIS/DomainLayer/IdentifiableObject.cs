using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer
{
    [Serializable]
    public class IdentifiableObject : IIdentifiable
    {
        private const long ID_NOT_INITIALIZED = -1;

        public long Id { get; set; }
        public bool IsPersisted { get { return this.Id != IdentifiableObject.ID_NOT_INITIALIZED; } }

        public IdentifiableObject()
        {
            this.Id = IdentifiableObject.ID_NOT_INITIALIZED;
        }
        public IdentifiableObject(long id)
        {
            this.Id = id;
        }
    }
}
