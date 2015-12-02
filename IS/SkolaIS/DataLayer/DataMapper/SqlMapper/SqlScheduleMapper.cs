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
    public class SqlScheduleMapper : AbstractSqlMapper<ISchedule>, IScheduleRepository
    {
        public const string SCHEDULE_HOUR_ASSOCIATION_TABLE = "ScheduleTeachingHour";

        protected override string TableName
        {
            get { return "Schedule"; }
        }
        protected override string UpdateByIdQuery
        {
            get { throw new NotImplementedException(); }
        }
        protected override string InsertQuery
        {
            get { throw new NotImplementedException(); }
        }

        private ITeachingHourRepository teachingHourRepository;

        public SqlScheduleMapper(DatabaseConnection connection, ITeachingHourRepository teachingHourRepository) : base(connection)
        {
            this.teachingHourRepository = teachingHourRepository;
        }

        public override void Update(ISchedule t)
        {
            this.Delete(t);
            this.Create(t);
        }
        protected override void Create(ISchedule t)
        {
            StringBuilder insertSql = new StringBuilder("INSERT INTO {0}(scheduleId, teachingHourId)".FormatWith(this.TableName));

            int index = 0;
            foreach (ITeachingHour hour in t.Hours)
            {
                insertSql.Append(" VALUES(@scheduleId, @teachingHour{1}),".FormatWith(hour.Id));
            }

            if (index > 0)
            {
                insertSql.Remove(insertSql.Length - 1, 1);
            }

            index = 0;

            SqlCommand command = this.Database.GetCommand(insertSql.ToString());
            command.Parameters.AddWithValue("scheduleId", t.Id);

            foreach (ITeachingHour hour in t.Hours)
            {
                command.Parameters.AddWithValue("@teachingHour{0}".FormatWith(index), hour.Id);
            }

            command.ExecuteNonQuery();
        }
        public override void Delete(ISchedule t)
        {
            SqlCommand command = this.Database.GetCommand("DELETE FROM {0} WHERE scheduleId=@scheduleId".FormatWith(SqlScheduleMapper.SCHEDULE_HOUR_ASSOCIATION_TABLE));
            command.Parameters.AddWithValue("scheduleId", t.Id);
            command.ExecuteNonQuery();
        }
        
        protected override ISchedule LoadObject(SqlDataReader reader)
        {
            long id = this.GetId(reader);

            IEnumerable<ITeachingHour> hours = this.teachingHourRepository.FindByScheduleId(id);

            ISchedule schedule = new Schedule(
                hours
            );

            this.AddId(schedule, id);

            return schedule;
        }

        protected override Dictionary<string, object> GetUpdateValues(ISchedule t)
        {
            throw new NotImplementedException();
        }
        protected override Dictionary<string, object> GetInsertValues(ISchedule t)
        {
            throw new NotImplementedException();
        }
    }
}
