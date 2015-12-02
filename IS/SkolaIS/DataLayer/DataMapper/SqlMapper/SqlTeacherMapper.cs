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
    public class SqlTeacherMapper : AbstractSqlMapper<Teacher>
    {
        protected override string TableName
        {
            get { return "Person"; }
        }
        protected override string UpdateByIdQuery
        {
            get { throw new NotImplementedException(); }
        }
        protected override string InsertQuery
        {
            get { throw new NotImplementedException(); }
        }

        public SqlTeacherMapper(DatabaseConnection connection) : base(connection)
        {

        }

        protected override Teacher LoadObject(SqlDataReader reader)
        {
            return null;
        }

        protected override Dictionary<string, object> GetUpdateValues(Teacher t)
        {
            throw new NotImplementedException();
        }

        protected override Dictionary<string, object> GetInsertValues(Teacher t)
        {
            throw new NotImplementedException();
        }
    }
}
