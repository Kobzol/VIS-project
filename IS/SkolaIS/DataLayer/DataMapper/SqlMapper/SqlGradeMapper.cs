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
    public class SqlGradeMapper : AbstractSqlMapper<IGrade>, IGradeRepository
    {
        protected override string TableName
        {
            get { return "Grade"; }
        }
        protected override string UpdateByIdQuery
        {
            get { return "UPDATE {0} SET weight=@weight, value=@value, studentId=@studentId, testId=@testId WHERE id=@id".FormatWith(this.TableName); }
        }
        protected override string InsertQuery
        {
            get { return "INSERT INTO {0}(weight, value, studentId, testId) VALUES(@weight, @value, @studentId, @testId)".FormatWith(this.TableName); }
        }

        private IPersonRepository studentRepository;

        public SqlGradeMapper(DatabaseConnection connection, IPersonRepository studentRepository) : base(connection)
        {
            this.studentRepository = studentRepository;
        }

        public Dictionary<long, IGrade> FindByTestId(long testId)
        {
            SqlCommand command = this.Database.GetCommand("SELECT id, weight, value, studentId FROM {0} WHERE testId = @testId".FormatWith(this.TableName));
            command.Parameters.AddWithValue("testId", testId);

            Dictionary<long, IGrade> grades = new Dictionary<long, IGrade>();

            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    IGrade grade = this.LoadObjectFromCache(reader);
                    grades.Add(reader.GetColumnValue<long>("studentId"), grade);
                }
            }

            return grades;
        }

        protected override IGrade LoadObject(SqlDataReader reader)
        {
            IGrade grade = new Grade(
                reader.GetColumnValue<double>("weight"),
                reader.GetColumnValue<double>("value")
            );

            this.AddId(grade, this.GetId(reader));

            return grade;
        }

        protected override Dictionary<string, object> GetUpdateValues(IGrade t)
        {
            return new Dictionary<string, object>()
            {
                {"weight", t.Weight},
                {"value", t.Value}
            };
        }
        protected override Dictionary<string, object> GetInsertValues(IGrade t)
        {
            return this.GetUpdateValues(t);
        }
    }
}
