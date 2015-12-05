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
    public class SqlSubjectMapper : AbstractSqlMapper<ISubject>, ISubjectRepository
    {
        protected override string TableName
        {
            get { return "Subject"; }
        }
        protected override string UpdateByIdQuery
        {
            get { return "UPDATE {0} SET name=@name, year=@year, scheduleId=@scheduleId WHERE id=@id".FormatWith(this.TableName); }
        }
        protected override string InsertQuery
        {
            get { return "INSERT INTO {0}(name, year, scheduleId) VALUES(@name, @year, @scheduleId)".FormatWith(this.TableName); }
        }

        private IAbsenceRepository absenceRepository;
        private IScheduleRepository scheduleRepository;

        public SqlSubjectMapper(DatabaseConnection connection, IAbsenceRepository absenceRepository, IScheduleRepository scheduleRepository) : base(connection)
        {
            this.absenceRepository = absenceRepository;
            this.scheduleRepository = scheduleRepository;
        }

        public IEnumerable<ISubject> FindByStudentId(long studentId)
        {
            SqlCommand command = this.Database.GetCommand(@"SELECT sub.id, sub.name, sub.year, scheduleId FROM {0} as sub
                JOIN SubjectPerson as subPer ON sub.id = subPer.subjectId AND subPer.personId = @studentId".FormatWith(this.TableName));
            command.Parameters.AddWithValue("studentId", studentId);

            return this.FindMultiple(command);
        }
        public IEnumerable<ISubject> FindByTeacherId(long teacherId)
        {
            return this.FindByStudentId(teacherId);
        }

        protected override ISubject LoadObject(SqlDataReader reader)
        {
            long id = this.GetId(reader);

            IEnumerable<IAbsence> absences = this.absenceRepository.FindBySubjectId(id);
            ISchedule schedule = this.scheduleRepository.Find(reader.GetColumnValue<long>("scheduleId"));

            ISubject subject = new Subject(
                reader.GetColumnValue<string>("name"),
                reader.GetColumnValue<int>("year"),
                absences,
                schedule
            );

            this.AddId(subject, id);

            return subject;
        }

        protected override Dictionary<string, object> GetUpdateValues(ISubject t)
        {
            return new Dictionary<string, object>()
            {
                {"name", t.Name},
                {"year", t.Year},
                {"scheduleId", t.Schedule.Id}
            };
        }
        protected override Dictionary<string, object> GetInsertValues(ISubject t)
        {
            return this.GetUpdateValues(t);
        }
    }
}
