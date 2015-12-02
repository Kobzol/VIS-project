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
    public class SqlTeachingHourMapper : AbstractSqlMapper<ITeachingHour>, ITeachingHourRepository
    {
        protected override string TableName
        {
            get { return "TeachingHour"; }
        }
        protected override string UpdateByIdQuery
        {
            get { return "UPDATE {0} SET day=@day, order=@order WHERE id=@id".FormatWith(this.TableName); }
        }
        protected override string InsertQuery
        {
            get { return "INSERT INTO {0}(day, order) VALUES(@day, @order)".FormatWith(this.TableName); }
        }

        public SqlTeachingHourMapper(DatabaseConnection connection) : base(connection)
        {

        }

        public IEnumerable<ITeachingHour> FindByScheduleId(long scheduleId)
        {
            SqlCommand command = this.Database.GetCommand(@"SELECT day, order FROM {0} h JOIN {1} sth
                            ON sth.teachingHourId = h.id AND sth.scheduleId = @scheduleId".FormatWith(this.TableName, SqlScheduleMapper.SCHEDULE_HOUR_ASSOCIATION_TABLE));
            command.Parameters.AddWithValue("scheduleId", scheduleId);

            return this.FindMultiple(command);
        }

        protected override ITeachingHour LoadObject(SqlDataReader reader)
        {
            ITeachingHour hour = new TeachingHour(
                reader.GetColumnValue<int>("day"),
                reader.GetColumnValue<int>("order")
            );

            this.AddId(hour, this.GetId(reader));

            return hour;
        }

        protected override Dictionary<string, object> GetUpdateValues(ITeachingHour t)
        {
            return new Dictionary<string, object>()
            {
                {"day", t.Day},
                {"order", t.Order}
            };
        }
        protected override Dictionary<string, object> GetInsertValues(ITeachingHour t)
        {
            return this.GetUpdateValues(t);
        }
    }
}
