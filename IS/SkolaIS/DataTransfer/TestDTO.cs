using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace DataTransfer
{
    [DataContract]
    public class TestDTO : IdentifiableDTO
    {
        [DataMember(Name="Name")]
        public string Name { get; set; }
         [DataMember(Name = "Date")]
        public DateTime Date { get; set; }
         [DataMember(Name = "Grades")]
        public Dictionary<long, GradeDTO> Grades { get; set; }
    }
}
