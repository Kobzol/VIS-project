using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Web.Hosting;
using DataLayer.Database;
using DataLayer.DataMapper.SqlMapper;
using DataLayer.DataMapper.XmlMapper;
using DataTransfer;
using DomainLayer;
using DomainLayer.Repository;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace ServiceLayer
{
    public class SchoolService : ISchoolService
    {
        private static DatabaseConnection connection;
        private static RepoContainer repo;

        static SchoolService()
        {
            SchoolService.connection = new DatabaseConnection("Server=(localdb)\\v11.0; Integrated Security=true; Database=VIS; MultipleActiveResultSets=true;");
            SchoolService.repo = RepoContainer.Instance;
            SchoolService.InitMappers(SchoolService.connection, SchoolService.repo);
        }
        
        public SchoolService()
        {
            SchoolService.connection.Connect();
        }

        [WebGet(UriTemplate="/GetPerson?id={id}")]
        public PersonDTO GetPerson(long id)
        {
            return DTOConverter.Convert(SchoolService.repo.Person.Find(id));
        }

        [WebGet(UriTemplate = "/IsLoginValid?username={username}&password={password}")]
        public bool IsLoginValid(string username, string password)
        {
            return  (username == "student" && password == "student") ||
                    (username == "teacher" && password == "teacher");
        }

        [WebGet(UriTemplate = "/AddSupplement?subjectId={subjectId}&day={day}&order={order}&teacherId={teacherId}")]
        public bool AddSupplement(long subjectId, int day, int order, long teacherId)
        {
            ISubject subject = SchoolService.repo.Subject.Find(subjectId);
            ITeachingHour hour = SchoolService.repo.TeachingHour.FindByDayOrder(day, order);
            IPerson teacher = SchoolService.repo.Person.Find(teacherId);
            ISupplement supplement = new Supplement(false, DateTime.Now, hour, subject.Schedule, teacher);

            SchoolService.repo.Supplement.Update(supplement);

            return true;
        }

        [WebGet(UriTemplate = "/AddSupplementCancel?subjectId={subjectId}&day={day}&order={order}")]
        public bool AddSupplementCancel(long subjectId, int day, int order)
        {
            ISubject subject = SchoolService.repo.Subject.Find(subjectId);
            ITeachingHour hour = SchoolService.repo.TeachingHour.FindByDayOrder(day, order);
            ISupplement supplement = new Supplement(true, DateTime.Now, hour, subject.Schedule);

            SchoolService.repo.Supplement.Update(supplement);

            return true;
        }

        public IEnumerable<PersonDTO> GetTeachers()
        {
            return DTOConverter.ConvertAll(SchoolService.repo.Person.FindByRole(PersonRole.Teacher));
        }

        public IEnumerable<SupplementDTO> GetSupplements(long subjectId)
        {
            return DTOConverter.ConvertAll(SchoolService.repo.Supplement.FindBySubjectId(subjectId));
        }
        
        [WebGet(UriTemplate = "/TestGetGrades")]
        public IEnumerable<GradeDTO> TestGetGrades()
        {
            return new List<GradeDTO>()
            {
                new GradeDTO() {Id = 1, Value = 2, Weight = 5},
                new GradeDTO() {Id = 2, Value = 3, Weight = 4},
                new GradeDTO() {Id = 3, Value = 1, Weight = 8}
            };
        }

        private static void InitMappers(DatabaseConnection connection, RepoContainer repository)
        {
            string gradeXml = HostingEnvironment.MapPath("~/xml");

            SqlPersonMapper personMapper = new SqlPersonMapper(connection);
            SqlTeachingHourMapper teachingHourMapper = new SqlTeachingHourMapper(connection);
            SqlScheduleMapper scheduleMapper = new SqlScheduleMapper(connection, teachingHourMapper);
            SqlAbsenceMapper absenceMapper = new SqlAbsenceMapper(connection, personMapper, teachingHourMapper);
            SqlTestMapper testMapper = new SqlTestMapper(connection);
            SqlSubjectMapper subjectMapper = new SqlSubjectMapper(connection, absenceMapper, scheduleMapper, testMapper);
            SqlClassMapper classMapper = new SqlClassMapper(connection, personMapper);
            SqlGradeMapper gradeMapper = new SqlGradeMapper(connection);
            SqlSupplementMapper supplementMapper = new SqlSupplementMapper(connection, teachingHourMapper, scheduleMapper, personMapper);

            personMapper.SubjectRepository = subjectMapper; // break object cycle
            personMapper.ClassRepository = classMapper;
            testMapper.GradeRepository = gradeMapper;

            repository.TeachingHour = teachingHourMapper;
            repository.Schedule = scheduleMapper;
            repository.Person = personMapper;
            repository.Absence = absenceMapper;
            repository.Subject = subjectMapper;
            repository.Class = classMapper;
            repository.Test = testMapper;
            repository.Grade = gradeMapper;
            repository.Supplement = supplementMapper;
        }
    }
}
