using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTransfer
{
    [Serializable]
    public class SubjectDTO : IdentifiableDTO
    {
        public string Name { get; set; }
        public int Year { get; set; }
        public IEnumerable<AbsenceDTO> Absences { get; set; }
        public ScheduleDTO Schedule { get; set; }
    }
}
