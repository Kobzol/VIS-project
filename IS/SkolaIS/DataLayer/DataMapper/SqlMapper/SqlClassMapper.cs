using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer.Database;
using DataLayer.Helper;
using DomainLayer;
using DomainLayer.Repository;

namespace DataLayer.DataMapper.SqlMapper
{
    public class SqlClassMapper : AbstractSqlMapper<IClass>, IClassRepository
    {
        protected override string TableName
        {
            get { return "Class"; }
        }
        protected override string UpdateByIdQuery
        {
            get { return "UPDATE {0} SET firstYear=@firstYear, finalYear=@finalYear, room=@room, teacherID=@teacherID WHERE id=@id".FormatWith(this.TableName); }
        }
        protected override string InsertQuery
        {
            get { return "INSERT INTO {0}(firstYear, finalYear, room, teacherId) VALUES(@firstYear, @finalYear, @room, @teacherId)".FormatWith(this.TableName); }
        }

        private IPersonRepository teacherRepository;

        public SqlClassMapper(DatabaseConnection connection, IPersonRepository teacherRepository)
            : base(connection)
        {
            this.teacherRepository = teacherRepository;
        }

        protected override IClass LoadObject(SqlDataReader reader)
        {
            IClass klass = new Class(
                reader.GetColumnValue<int>("firstYear"),
                reader.GetColumnValue<int>("finalYear"),
                (Room) Enum.Parse(typeof(Room), reader.GetColumnValue<int>("room").ToString())
            );

            this.AddId(klass, this.GetId(reader));

            return klass;
        }

        protected override Dictionary<string, object> GetUpdateValues(IClass t)
        {
            return new Dictionary<string, object>()
            {
                {"firstYear", t.FirstYear},
                {"finalYear", t.FinalYear},
                {"room", (uint) t.Room}
            };
        }
        protected override Dictionary<string, object> GetInsertValues(IClass t)
        {
            return this.GetUpdateValues(t);
        }
    }
}
