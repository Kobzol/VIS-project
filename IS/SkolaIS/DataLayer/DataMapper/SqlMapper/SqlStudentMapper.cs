using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer.Database;
using DataLayer.Helper;
using DomainLayer;

namespace DataLayer.DataMapper.SqlMapper
{
    public class SqlStudentMapper : AbstractSqlMapper<Student>
    {
        protected override string TableName
        {
            get { return "Person"; }
        }
        protected override string UpdateByIdQuery
        {
            get { return "UPDATE {0} SET name=@name, surname=@surname, birthDate=@birthDate, email=@email, classId=@classId WHERE id=@id".FormatWith(this.TableName); }
        }
        protected override string InsertQuery
        {
            get { return "INSERT INTO {0}(name, surname, birthDate, email, classId) VALUES(@name, @surname, @birthDate, @email, @classId)".FormatWith(this.TableName); }
        }

        public SqlStudentMapper(DatabaseConnection connection) : base(connection)
        {

        }

        protected override Student LoadObject(SqlDataReader reader)
        {
            long id = reader.GetColumnValue<long>(this.ColumnId);

            if (this.HasStoredObject(id))
            {
                return this.GetStoredObject(id);
            }

            IClass klass = null;    // TODO
            IEnumerable<ISubject> subjects = null;

            Student student = new Student(
                reader.GetColumnValue<string>("name"),
                reader.GetColumnValue<string>("surname"),
                reader.GetColumnValue<DateTime>("birthDate"),
                reader.GetColumnValue<string>("email"),
                klass,
                subjects
            );

            this.AddId(student, id);

            return student;
        }

        protected override Dictionary<string, object> GetUpdateValues(Student t)
        {
            return new Dictionary<string, object>()
            {
                {"name", t.Name},
                {"surname", t.Surname},
                {"birthDate", t.BirthDate},
                {"email", t.Email},
                {"classId", t.Class == null ? null : (long?) t.Class.Id}
            };
        }

        protected override Dictionary<string, object> GetInsertValues(Student t)
        {
            return this.GetUpdateValues(t);
        }
    }
}
