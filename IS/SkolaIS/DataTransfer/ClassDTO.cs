using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTransfer
{
    [Serializable]
    public class ClassDTO : IdentifiableDTO
    {
        public int FirstYear { get; set; }
        public int FinalYear { get; set; }
        public RoomDTO Room { get; set; }
        public PersonDTO Teacher { get; set; }
    }
}
