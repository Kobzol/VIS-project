using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTransfer
{
    [Serializable]
    public class TestDTO : IdentifiableDTO
    {
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public SubjectDTO Subject { get; set; }
    }
}
