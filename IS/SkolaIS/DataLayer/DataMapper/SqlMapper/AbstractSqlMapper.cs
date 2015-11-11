using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer.Database;
using DomainLayer;

namespace DataLayer.DataMapper.SqlMapper
{
    public abstract class AbstractSqlMapper<T> : IMapper<T> where T : IIdentifiable
    {
        public DatabaseConnection Database { get; private set; }

        protected abstract string TableName { get; }
        protected abstract string FindByIdQuery { get; }
        protected virtual string ColumnId
        {
            get
            {
                return "id";
            }
        }

        protected IdentityMap<T> identityMap = new IdentityMap<T>();

        public AbstractSqlMapper(DatabaseConnection connection)
        {
            this.Database = connection;
        }

        public T Find(long id)
        {
            if (this.HasStoredObject(id))
            {
                return this.GetStoredObject(id);
            }

            SqlCommand command = this.Database.GetCommand(this.FindByIdQuery);
            command.Parameters.AddWithValue(this.ColumnId, id);

            SqlDataReader reader = command.ExecuteReader();

            if (!reader.HasRows)
            {
                throw new DatabaseException(String.Format("Object with id {0} was not found", id));
            }
            
            reader.Read();

            return this.LoadObject(reader);
        }

        protected virtual bool HasStoredObject(long id)
        {
            return this.identityMap.HasObject(id);
        }
        protected virtual T GetStoredObject(long id)
        {
            return this.identityMap.GetObject(id);
        }

        protected abstract T LoadObject(SqlDataReader reader);
    }
}
