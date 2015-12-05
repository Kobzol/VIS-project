using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataTransfer;
using DomainLayer;

namespace ServiceLayer
{
    public static class DTOConverter
    {
        public static ClassDTO Convert(IClass klass)
        {
            return new ClassDTO()
            {
                Id = klass.Id,
                FirstYear = klass.FinalYear,
                FinalYear = klass.FinalYear,
                Room = Convert(klass.Room)
            };
        }
        public static SubjectDTO Convert(ISubject subject)
        {
            return new SubjectDTO()
            {
                Id = subject.Id,
                Name = subject.Name,
                Year = subject.Year,
                Absences = ConvertAll(subject.Absences),
                Schedule = Convert(subject.Schedule),
                Tests = ConvertAll(subject.Tests)
            };
        }
        public static RoomDTO Convert(Room room)
        {
            return (RoomDTO)room;
        }
        public static PersonDTO Convert(IPerson teacher)
        {
            return new PersonDTO()
            {
                Id = teacher.Id,
                BirthDate = teacher.BirthDate,
                Name = teacher.Name,
                Email = teacher.Email,
                Role = Convert(teacher.Role),
                Surname = teacher.Surname,
                Subjects = ConvertAll(teacher.Subjects)
            };
        }
        public static PersonRoleDTO Convert(PersonRole role)
        {
            return (PersonRoleDTO)role;
        }
        public static AbsenceDTO Convert(IAbsence absence)
        {
            return new AbsenceDTO()
            {
                Id = absence.Id,
                Date = absence.Date,
                Excused = absence.Excused,
                Type = Convert(absence.Type),
                Hour = Convert(absence.Hour),
                Student = Convert(absence.Student)
            };
        }
        public static AbsenceTypeDTO Convert(AbsenceType absenceType)
        {
            return (AbsenceTypeDTO)absenceType;
        }
        public static TeachingHourDTO Convert(ITeachingHour hour)
        {
            return new TeachingHourDTO()
            {
                Id = hour.Id,
                Day = hour.Day,
                Order = hour.Order
            };
        }
        public static ScheduleDTO Convert(ISchedule schedule)
        {
            return new ScheduleDTO()
            {
                Id = schedule.Id,
                Hours = ConvertAll(schedule.Hours)
            };
        }
        public static GradeDTO Convert(IGrade grade)
        {
            return new GradeDTO()
            {
                Id = grade.Id,
                Value = grade.Value,
                Weight = grade.Weight
            };
        }
        public static TestDTO Convert(ITest test)
        {
            return new TestDTO()
            {
                Id = test.Id,
                Name = test.Name,
                Date = test.Date,
                Grades = ConvertAll(test.Grades)
            };
        }

        public static IEnumerable<AbsenceDTO> ConvertAll(IEnumerable<IAbsence> absences)
        {
            List<AbsenceDTO> converted = new List<AbsenceDTO>();

            foreach (IAbsence absence in absences)
            {
                converted.Add(Convert(absence));
            }

            return converted;
        }
        public static IEnumerable<SubjectDTO> ConvertAll(IEnumerable<ISubject> subjects)
        {
            List<SubjectDTO> converted = new List<SubjectDTO>();

            foreach (ISubject subject in subjects)
            {
                converted.Add(Convert(subject));
            }

            return converted;
        }
        public static IEnumerable<TeachingHourDTO> ConvertAll(IEnumerable<ITeachingHour> hours)
        {
            List<TeachingHourDTO> converted = new List<TeachingHourDTO>();

            foreach (ITeachingHour hour in hours)
            {
                converted.Add(Convert(hour));
            }

            return converted;
        }
        public static Dictionary<long, GradeDTO> ConvertAll(Dictionary<long, IGrade> grades)
        {
            Dictionary<long, GradeDTO> converted = new Dictionary<long, GradeDTO>();

            foreach (long id in grades.Keys)
            {
                converted.Add(id, Convert(grades[id]));
            }

            return converted;
        }
        public static IEnumerable<TestDTO> ConvertAll(IEnumerable<ITest> tests)
        {
            List<TestDTO> converted = new List<TestDTO>();

            foreach (ITest test in tests)
            {
                converted.Add(Convert(test));
            }

            return converted;
        }
    }
}