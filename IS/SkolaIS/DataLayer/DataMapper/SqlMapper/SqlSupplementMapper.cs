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
    public class SqlSupplementMapper : AbstractSqlMapper<ISupplement>, ISupplementRepository
    {
        protected override string TableName
        {
            get { return "Supplement"; }
        }
        protected override string UpdateByIdQuery
        {
            get { return "UPDATE {0} SET canceled=@canceled, date=@date, scheduleId=@scheduleId, teachingHourId=@teachingHourId WHERE id=@id".FormatWith(this.TableName); }
        }
        protected override string InsertQuery
        {
            get { return "INSERT INTO {0}(canceled, date, scheduleId, teachingHourId) VALUES(@canceled, @date, @scheduleId, @teachingHourId)".FormatWith(this.TableName); }
        }

        private ITeachingHourRepository teachingHourRepository;
        private IScheduleRepository scheduleRepository;

        public SqlSupplementMapper(DatabaseConnection connection, ITeachingHourRepository teachingHourRepository, IScheduleRepository scheduleRepository) : base(connection)
        {
            this.teachingHourRepository = teachingHourRepository;
            this.scheduleRepository = scheduleRepository;
        }

        protected override ISupplement LoadObject(System.Data.SqlClient.SqlDataReader reader)
        {
            ITeachingHour hour = this.teachingHourRepository.Find(reader.GetColumnValue<long>("teachingHourId"));
            ISchedule schedule = this.scheduleRepository.Find(reader.GetColumnValue<long>("scheduleId"));

            ISupplement supplement = new Supplement(
                reader.GetColumnValue<byte>("canceled") == 1,
                reader.GetColumnValue<DateTime>("date"),
                hour,
                schedule
            );

            this.AddId(supplement, this.GetId(reader));

            return supplement;
        }

        protected override Dictionary<string, object> GetUpdateValues(ISupplement t)
        {
            return new Dictionary<string, object>()
            {
                {"canceled", (byte) (t.IsHourCanceled ? 1 : 0)},
                {"date", t.Date},
                {"teachingHourId", t.Hour.Id},
                {"scheduleId", t.Schedule.Id}
            };
        }
        protected override Dictionary<string, object> GetInsertValues(ISupplement t)
        {
            return this.GetUpdateValues(t);
        }
    }
}
