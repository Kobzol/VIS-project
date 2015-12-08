using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace DataTransfer
{
    [DataContract]
    public class ClassDTO : IdentifiableDTO
    {
        [DataMember(Name="FirstYear")]
        public int FirstYear { get; set; }
        [DataMember(Name="FinalYear")]
        public int FinalYear { get; set; }
        [DataMember(Name = "Room")]
        public RoomDTO Room { get; set; }
        [DataMember(Name = "Teacher")]
        public PersonDTO Teacher { get; set; }
    }
}
