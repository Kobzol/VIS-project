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
    public class SqlPersonMapper : AbstractSqlMapper<Person>, IPersonMapper
    {
        protected override string TableName
        {
            get { return "Person"; }
        }
        protected override string FindByIdQuery
        {
            get { return String.Format("SELECT * FROM {0} WHERE {1} = @id", this.TableName, this.ColumnId); }
        }

        public SqlPersonMapper(DatabaseConnection connection) : base(connection)
        {

        }

        protected override Person LoadObject(SqlDataReader reader)
        {
            long id = reader.GetColumnValue<long>(this.ColumnId);

            if (this.HasStoredObject(id))
            {
                return this.GetStoredObject(id);
            }

            return new Person(id);
        }
    }
}
