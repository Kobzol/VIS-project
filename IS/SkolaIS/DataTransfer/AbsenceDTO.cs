using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTransfer
{
    [Serializable]
    public class AbsenceDTO : IdentifiableDTO
    {
        public DateTime Date { get; set; }
        public AbsenceTypeDTO Type { get; set; }
        public bool Excused { get; set; }
        public long Student { get; set; }
        public TeachingHourDTO Hour { get; set; }
    }
}
