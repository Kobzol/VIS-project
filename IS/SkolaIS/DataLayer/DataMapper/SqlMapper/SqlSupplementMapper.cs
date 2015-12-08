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
    public class SqlSupplementMapper : AbstractSqlMapper<ISupplement>, ISupplementRepository
    {
        protected override string TableName
        {
            get { return "Supplement"; }
        }
        protected override string UpdateByIdQuery
        {
            get { return "UPDATE {0} SET canceled=@canceled, date=@date, scheduleId=@scheduleId, teachingHourId=@teachingHourId, teacherId=@teacherId WHERE id=@id".FormatWith(this.TableName); }
        }
        protected override string InsertQuery
        {
            get { return "INSERT INTO {0}(canceled, date, scheduleId, teachingHourId, teacherId) VALUES(@canceled, @date, @scheduleId, @teachingHourId, @teacherId)".FormatWith(this.TableName); }
        }

        private ITeachingHourRepository teachingHourRepository;
        private IScheduleRepository scheduleRepository;
        private IPersonRepository personRepository;

        public SqlSupplementMapper(DatabaseConnection connection, ITeachingHourRepository teachingHourRepository,
            IScheduleRepository scheduleRepository, IPersonRepository personRepository) : base(connection)
        {
            this.teachingHourRepository = teachingHourRepository;
            this.scheduleRepository = scheduleRepository;
            this.personRepository = personRepository;
        }

        public IEnumerable<ISupplement> FindBySubjectId(long subjectId)
        {
            SqlCommand command = this.Database.GetCommand(@"SELECT sup.id, sup.canceled, sup.date, sup.scheduleId, sup.teachingHourId, sup.teacherId FROM {0} as sup
            JOIN Subject as subj ON subj.scheduleId = sup.scheduleId AND subj.id=@subjectId".FormatWith(this.TableName));

            command.Parameters.AddWithValue("subjectId", subjectId);

            return this.FindMultiple(command);
        }

        protected override ISupplement LoadObject(System.Data.SqlClient.SqlDataReader reader)
        {
            ITeachingHour hour = this.teachingHourRepository.Find(reader.GetColumnValue<long>("teachingHourId"));
            ISchedule schedule = this.scheduleRepository.Find(reader.GetColumnValue<long>("scheduleId"));

            long? teacherId = reader.GetColumnValue<long>("teacherId");
            IPerson teacher = null;

            if (teacherId.HasValue)
            {
                teacher = this.personRepository.Find(teacherId.Value);
            }

            ISupplement supplement = new Supplement(
                reader.GetColumnValue<byte>("canceled") == 1,
                reader.GetColumnValue<DateTime>("date"),
                hour,
                schedule,
                teacher
            );

            this.AddId(supplement, this.GetId(reader));

            return supplement;
        }

        protected override Dictionary<string, object> GetUpdateValues(ISupplement t)
        {
            object teacherId = DBNull.Value;
            
            if (t.Teacher != null)
            {
                teacherId = t.Teacher.Id;
            }

            return new Dictionary<string, object>()
            {
                {"canceled", (byte) (t.IsHourCanceled ? 1 : 0)},
                {"date", t.Date},
                {"teachingHourId", t.Hour.Id},
                {"scheduleId", t.Schedule.Id},
                {"teacherId", teacherId}
            };
        }
        protected override Dictionary<string, object> GetInsertValues(ISupplement t)
        {
            return this.GetUpdateValues(t);
        }
    }
}
