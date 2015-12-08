using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace DataTransfer
{
    [DataContract]
    public class SubjectDTO : IdentifiableDTO
    {
        [DataMember(Name="Name")]
        public string Name { get; set; }
        [DataMember(Name = "Year")]
        public int Year { get; set; }
        [DataMember(Name = "Absences")]
        public IEnumerable<AbsenceDTO> Absences { get; set; }
        [DataMember(Name = "Schedule")]
        public ScheduleDTO Schedule { get; set; }
        [DataMember(Name = "Tests")]
        public IEnumerable<TestDTO> Tests { get; set; }
    }
}
