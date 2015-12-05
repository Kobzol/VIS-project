using System;
using System.Collections.Generic;
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

        protected override IPerson LoadObject(System.Data.SqlClient.SqlDataReader reader)
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
            throw new NotImplementedException();
        }
        protected override Dictionary<string, object> GetInsertValues(IPerson t)
        {
            throw new NotImplementedException();
        }
    }
}
