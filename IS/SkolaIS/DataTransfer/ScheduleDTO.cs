using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace DataTransfer
{
    [DataContract]
    public class ScheduleDTO : IdentifiableDTO
    {
        [DataMember(Name="Hours")]
        public IEnumerable<TeachingHourDTO> Hours { get; set; }
    }
}
