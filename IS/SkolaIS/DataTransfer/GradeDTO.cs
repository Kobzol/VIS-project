using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace DataTransfer
{
    [DataContract]
    public class GradeDTO : IdentifiableDTO
    {
        [DataMember(Name="Weight")]
        public double Weight { get; set; }
        [DataMember(Name="Value")]
        public double Value { get; set; }
    }
}
