﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer.Database;
using DomainLayer;

namespace DataLayer.DataMapper.SqlMapper
{
    class SqlPersonMapper : AbstractSqlMapper<Person>, IPersonMapper
    {
        protected override string TableName
        {
            get { return "Person"; }
        }
        protected override string FindByIdQuery
        {
            get { return String.Format("SELECT * FROM {0}", this.TableName); }
        }

        public SqlPersonMapper(DatabaseConnection connection) : base(connection)
        {

        }

        protected override Person LoadObject(SqlDataReader reader)
        {
            long id = reader.GetColumnValue<long>(this.ColumnId);

            return new Person(id);
        }
    }
}
