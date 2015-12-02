using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer
{
    public class Class : IdentifiableObject, IClass
    {
        public int FirstYear { get; private set; }
        public int FinalYear { get; private set; }
        public Room Room { get; private set; }
        public ITeacher Teacher { get; private set; }
        public IEnumerable<ISubject> Subjects { get; private set; }

        public Class(long id, int firstYear, int finalYear, Room room, ITeacher teacher, IEnumerable<ISubject> subjects) : base(id)
        {
            this.FirstYear = firstYear;
            this.FinalYear = finalYear;
            this.Room = room;
            this.Teacher = teacher;
            this.Subjects = subjects;
        }
    }
}
