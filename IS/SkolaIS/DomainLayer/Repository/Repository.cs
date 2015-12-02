using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Repository
{
    public class Repository
    {
        public static Repository Instance { get { return Repository.instance; } }
        private static Repository instance = new Repository();

        public IStudentRepository Student { get; set; }
        public ITeacherRepository Teacher { get; set; }
        public IClassRepository Class { get; set; }
        public ISubjectRepository Subject { get; set; }
        public IScheduleRepository Schedule { get; set; }
        public ITestRepository Test { get; set; }
        public IAbsenceRepository Absence { get; set; }
        public IGradeRepository Grade { get; set; }
        public ITeachingHourRepository TeachingHour { get; set; }
        public ISupplementRepository Supplement { get; set; }
    }
}
