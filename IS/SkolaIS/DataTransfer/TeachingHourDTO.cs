using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace DataTransfer
{
    [DataContract]
    public class TeachingHourDTO : IdentifiableDTO
    {
        [DataMember(Name="Day")]
        public int Day { get; set; }
        [DataMember(Name="Order")]
        public int Order { get; set; }
    }
}
