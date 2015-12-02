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
    public class SqlAbsenceMapper : AbstractSqlMapper<IAbsence>, IAbsenceRepository
    {
        protected override string TableName
        {
            get { return "Absence"; }
        }
        protected override string UpdateByIdQuery
        {
            get { return "UPDATE {0} SET date=@date, type=@type, excused=@excused, studentId=@studentId, teachingHourId=@teachingHourId, subjectId=@subjectId WHERE id=@id".FormatWith(this.TableName); }
        }
        protected override string InsertQuery
        {
            get { return "INSERT INTO {0}(date, type, excused, studentId, teachingHourId) VALUES(@date, @type, @excused, @studentId, @teachingHourId, @subjectId)".FormatWith(this.TableName); }
        }

        private IStudentRepository studentRepository;
        private ITeachingHourRepository teachingHourRepository;

        public SqlAbsenceMapper(DatabaseConnection connection, IStudentRepository studentRepository, ITeachingHourRepository teachingHourRepository) : base(connection)
        {
            this.studentRepository = studentRepository;
            this.teachingHourRepository = teachingHourRepository;
        }

        public IEnumerable<IAbsence> FindBySubjectId(long subjectId)
        {
            SqlCommand command = this.Database.GetCommand("SELECT date, type, studentId, teachingHourId, subjectId FROM {0} WHERE subjectId = @subjectId".FormatWith(this.TableName));
            command.Parameters.AddWithValue("subjectId", subjectId);

            return this.FindMultiple(command);
        }

        protected override IAbsence LoadObject(System.Data.SqlClient.SqlDataReader reader)
        {
            IStudent student = this.studentRepository.Find(reader.GetColumnValue<long>("studentId"));
            ITeachingHour hour = this.teachingHourRepository.Find(reader.GetColumnValue<long>("teachingHourId"));

            IAbsence absence = new Absence(
                reader.GetColumnValue<DateTime>("date"),
                (AbsenceType) Enum.Parse(typeof(AbsenceType), reader.GetColumnValue<int>("type").ToString()),
                reader.GetColumnValue<byte>("excused") == 1,
                student,
                hour
            );

            this.AddId(absence, this.GetId(reader));

            return absence;
        }

        protected override Dictionary<string, object> GetUpdateValues(IAbsence t)
        {
            return new Dictionary<string, object>()
            {
                {"date", t.Date},
                {"type", (uint) t.Type},
                {"excused", (byte) (t.Excused ? 1 : 0)},
                {"studentId", t.Student.Id},
                {"teachingHourId", t.Hour.Id}
            };
        }
        protected override Dictionary<string, object> GetInsertValues(IAbsence t)
        {
            return this.GetUpdateValues(t);
        }
    }
}
