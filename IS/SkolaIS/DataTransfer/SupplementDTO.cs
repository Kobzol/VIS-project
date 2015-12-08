using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace DataTransfer
{
    [DataContract]
    public class SupplementDTO : IdentifiableDTO
    {
        [DataMember(Name="IsHourCanceled")]
        public bool IsHourCanceled { get; set; }
        [DataMember(Name = "Date")]
        public DateTime Date { get; set; }
        [DataMember(Name = "Hour")]
        public TeachingHourDTO Hour { get; set; }
        [DataMember(Name = "Schedule")]
        public ScheduleDTO Schedule { get; set; }
        [DataMember(Name = "Teacher")]
        public PersonDTO Teacher { get; set; }
    }
}
