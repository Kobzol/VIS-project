using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace DataTransfer
{
    [DataContract]
    public class IdentifiableDTO
    {
        [DataMember(Name="Id")]
        public long Id { get; set; }
    }
}
