using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace DataTransfer
{
    [DataContract]
    public class AbsenceDTO : IdentifiableDTO
    {
        [DataMember(Name="Date")]
        public DateTime Date { get; set; }
        [DataMember(Name = "Type")]
        public AbsenceTypeDTO Type { get; set; }
        [DataMember(Name = "Excused")]
        public bool Excused { get; set; }
        [DataMember(Name = "Student")]
        public long Student { get; set; }
        [DataMember(Name = "Hour")]
        public TeachingHourDTO Hour { get; set; }
    }
}
