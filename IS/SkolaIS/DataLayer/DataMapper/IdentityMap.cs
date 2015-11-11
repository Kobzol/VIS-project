using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainLayer;

namespace DataLayer.DataMapper
{
    public class IdentityMap<T> where T: IIdentifiable
    {
        private Dictionary<long, T> objects = new Dictionary<long, T>();

        public bool HasObject(long id)
        {
            return this.objects.ContainsKey(id);
        }
        public T GetObject(long id)
        {
            if (this.HasObject(id))
            {
                return this.objects[id];
            }
            else return default(T);
        }
        public void PutObject(T obj)
        {
            if (!this.HasObject(obj.Id))
            {
                this.objects[obj.Id] = obj;
            }
        }
    }
}
