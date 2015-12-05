﻿using System;
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

        public PersonDTO GetPerson(long id)
        {
            return DTOConverter.Convert(SchoolService.repo.Person.Find(id));
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
            SqlSupplementMapper supplementMapper = new SqlSupplementMapper(connection, teachingHourMapper, scheduleMapper);

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


        public bool IsLoginValid(string username, string password)
        {
            throw new NotImplementedException();
        }
    }
}
