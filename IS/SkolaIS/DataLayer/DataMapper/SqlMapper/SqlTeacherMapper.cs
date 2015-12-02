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
    public class SqlTeacherMapper : AbstractSqlMapper<ITeacher>, ITeacherRepository
    {
        protected override string TableName
        {
            get { return "Person"; }
        }
        protected override string UpdateByIdQuery
        {
            get { return "UPDATE {0} SET name=@name, surname=@surname, birthDate=@birthDate, email=@email WHERE id=@id".FormatWith(this.TableName); }
        }
        protected override string InsertQuery
        {
            get { return "INSERT INTO {0}(name, surname, birthDate, email) VALUES(@name, @surname, @birthDate, @email)".FormatWith(this.TableName); }
        }

        private ISubjectRepository subjectRepository;

        public SqlTeacherMapper(DatabaseConnection connection, ISubjectRepository subjectRepository) : base(connection)
        {
            this.subjectRepository = subjectRepository;
        }

        protected override ITeacher LoadObject(SqlDataReader reader)
        {
            long id = this.GetId(reader);

            IEnumerable<ISubject> subjects = this.subjectRepository.FindByTeacherId(id);

            ITeacher teacher = new Teacher(
                reader.GetColumnValue<string>("name"),
                reader.GetColumnValue<string>("surname"),
                reader.GetColumnValue<DateTime>("birthDate"),
                reader.GetColumnValue<string>("email"),
                subjects
            );

            this.AddId(teacher, id);

            return teacher;
        }

        protected override Dictionary<string, object> GetUpdateValues(ITeacher t)
        {
            return new Dictionary<string, object>()
            {
                {"name", t.Name},
                {"surname", t.Surname},
                {"birthDate", t.BirthDate},
                {"email", t.Email}
            };
        }
        protected override Dictionary<string, object> GetInsertValues(ITeacher t)
        {
            return this.GetUpdateValues(t);
        }
    }
}
