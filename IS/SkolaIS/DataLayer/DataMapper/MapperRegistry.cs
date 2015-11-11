using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainLayer;

namespace DataLayer.DataMapper
{
    public class MapperRegistry
    {
        private Dictionary<Type, object> mappers = new Dictionary<Type, object>();

        public void RegisterMapper<T>(IMapper<T> mapper) where T : IIdentifiable
        {
            if (mapper == null)
            {
                throw new ArgumentNullException("Mapper cannot be null");
            }

            Type key = typeof(T);

            if (this.mappers.ContainsKey(key))
            {
                throw new ArgumentException("Type " + key.ToString() + " already has a mapper registered.");
            }

            this.mappers[typeof(T)] = mapper;
        }
        public IMapper<T> GetMapper<T>() where T : IIdentifiable
        {
            Type key = typeof(T);

            if (!this.mappers.ContainsKey(key))
            {
                throw new ArgumentException("Type " + key.ToString() + " has no mapper registered.");
            }

            return (IMapper<T>) this.mappers[typeof(T)];
        }
    }
}
