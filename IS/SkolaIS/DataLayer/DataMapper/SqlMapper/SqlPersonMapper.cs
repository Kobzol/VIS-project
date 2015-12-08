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
    public class SqlPersonMapper : AbstractSqlMapper<IPerson>, IPersonRepository
    {
        protected override string TableName
        {
            get { return "Person"; }
        }
        protected override string UpdateByIdQuery
        {
            get { return "UPDATE {0} SET role=@role, name=@name, surname=@surname, birthDate=@birthDate, email=@email, classId=@classId WHERE id=@id".FormatWith(this.TableName); }
        }
        protected override string InsertQuery
        {
            get { return "INSERT INTO {0}(role, name, surname, birthDate, email, classId) VALUES(@role, @name, @surname, @birthDate, @email, @classId)".FormatWith(this.TableName); }
        }

        public ISubjectRepository SubjectRepository { get; set; }
        public IClassRepository ClassRepository { get; set; }

        public SqlPersonMapper(DatabaseConnection connection, ISubjectRepository subjectRepository = null, IClassRepository classRepository = null) : base(connection)
        {
            this.SubjectRepository = subjectRepository;
            this.ClassRepository = classRepository;
        }

        public IEnumerable<IPerson> FindBySubject(long subjectId)
        {
            SqlCommand command = this.Database.GetCommand(@"SELECT p.id, p.role, p.name, p.surname, p.birthDate, p.email, p.classId FROM {0} as p
            JOIN SubjectPerson as sp ON sp.personId = p.id AND sp.subjectId = @subjectId AND p.role = 2".FormatWith(this.TableName));
            command.Parameters.AddWithValue("subjectId", subjectId);

            return this.FindMultiple(command);
        }

        public IEnumerable<IPerson> FindByRole(PersonRole personRole)
        {
            SqlCommand command = this.Database.GetCommand(@"SELECT id, role, name, surname, birthDate, email, classId FROM {0} WHERE role = @role".FormatWith(this.TableName));
            command.Parameters.AddWithValue("role", (int) personRole);

            return this.FindMultiple(command);
        }

        protected override IPerson LoadObject(SqlDataReader reader)
        {
            long id = this.GetId(reader);
            PersonRole role = (PersonRole) Enum.Parse(typeof(PersonRole), reader.GetColumnValue<int>("role").ToString());
            long classId = reader.GetColumnValue<long>("classId");

            IEnumerable<ISubject> subjects = new List<ISubject>();

            if (role == PersonRole.Student)
            {
                subjects = this.SubjectRepository.FindByStudentId(id);
            }
            else if (role == PersonRole.Teacher)
            {
                subjects = this.SubjectRepository.FindByTeacherId(id);
            }

            IPerson person = new Person(
                    reader.GetColumnValue<string>("name"),
                    reader.GetColumnValue<string>("surname"),
                    reader.GetColumnValue<DateTime>("birthDate"),
                    reader.GetColumnValue<string>("email"),
                    role,
                    subjects,
                    this.ClassRepository.Find(classId)
            );

            this.AddId(person, id);

            return person;
        }

        protected override Dictionary<string, object> GetUpdateValues(IPerson t)
        {
            return new Dictionary<string, object>()
            {
                {"role", (int) t.Role},
                {"name", t.Name},
                {"surname", t.Surname},
                {"birthDate", t.BirthDate},
                {"email", t.Email},
                {"classId", t.Class.Id}
            };
        }
        protected override Dictionary<string, object> GetInsertValues(IPerson t)
        {
            return this.GetUpdateValues(t);
        }
    }
}
