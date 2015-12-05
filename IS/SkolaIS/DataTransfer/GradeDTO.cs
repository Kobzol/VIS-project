using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTransfer
{
    [Serializable]
    public class GradeDTO : IdentifiableDTO
    {
        public double Weight { get; set; }
        public double Value { get; set; }
        public PersonDTO Student { get; set; }
        public TestDTO Test { get; set; }
    }
}
