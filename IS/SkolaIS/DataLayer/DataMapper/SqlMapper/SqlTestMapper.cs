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
    public class SqlTestMapper : AbstractSqlMapper<ITest>, ITestRepository
    {
        protected override string TableName
        {
            get { return "Test"; }
        }
        protected override string UpdateByIdQuery
        {
            get { return "UPDATE {0} SET name=@name, date=@date, subjectId=@subjectId WHERE id=@id".FormatWith(this.TableName); }
        }
        protected override string InsertQuery
        {
            get { return "INSERT INTO {0}(name, date, subjectId) VALUES(@name, @date, @subjectId)".FormatWith(this.TableName); }
        }

        private ISubjectRepository subjectRepository;

        public SqlTestMapper(DatabaseConnection connection, ISubjectRepository subjectRepository) : base(connection)
        {
            this.subjectRepository = subjectRepository;
        }

        public IEnumerable<ITest> FindBySubjectId(long subjectId)
        {
            SqlCommand command = this.Database.GetCommand("SELECT name, date, subjectId FROM {0} WHERE subjectId = @subjectId".FormatWith(this.TableName));
            command.Parameters.AddWithValue("subjectId", subjectId);

            return this.FindMultiple(command);
        }

        protected override ITest LoadObject(System.Data.SqlClient.SqlDataReader reader)
        {
            ISubject subject = this.subjectRepository.Find(reader.GetColumnValue<long>("subjectId"));

            Test test = new Test(
                reader.GetColumnValue<string>("name"),
                reader.GetColumnValue<DateTime>("date"),
                subject
            );

            this.AddId(test, this.GetId(reader));

            return test;
        }

        protected override Dictionary<string, object> GetUpdateValues(ITest t)
        {
            return new Dictionary<string, object>()
            {
                {"name", t.Name},
                {"date", t.Date},
                {"subjectId", t.Subject.Id}
            };
        }

        protected override Dictionary<string, object> GetInsertValues(ITest t)
        {
            throw new NotImplementedException();
        }
    }
}
