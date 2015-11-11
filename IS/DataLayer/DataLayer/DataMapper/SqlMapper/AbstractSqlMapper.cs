using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer.Database;

namespace DataLayer.DataMapper.SqlMapper
{
    abstract class AbstractSqlMapper<T> : IMapper<T>
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

        public AbstractSqlMapper(DatabaseConnection connection)
        {
            this.Database = connection;
        }

        public T Find(long id)
        {
            SqlCommand command = this.Database.GetCommand(this.FindByIdQuery);
            command.Parameters.AddWithValue(this.ColumnId, id);

            return this.LoadObject(command.ExecuteReader());
        }

        protected abstract T LoadObject(SqlDataReader reader);
    }
}
