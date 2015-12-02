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
    public class SqlStudentMapper : AbstractSqlMapper<IStudent>, IStudentRepository
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

        public IClassRepository ClassRepository { get; set; }
        public ISubjectRepository SubjectRepository { get; set; }

        public SqlStudentMapper(DatabaseConnection connection) : base(connection)
        {
            
        }

        protected override IStudent LoadObject(SqlDataReader reader)
        {
            long id = this.GetId(reader);

            IClass klass = this.ClassRepository.Find(reader.GetColumnValue<long>("classId"));
            IEnumerable<ISubject> subjects = this.SubjectRepository.FindByStudentId(id);

            IStudent student = new Student(
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

        protected override Dictionary<string, object> GetUpdateValues(IStudent t)
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
        protected override Dictionary<string, object> GetInsertValues(IStudent t)
        {
            return this.GetUpdateValues(t);
        }
    }
}
