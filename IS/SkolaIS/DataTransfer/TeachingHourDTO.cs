using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTransfer
{
    [Serializable]
    public class TeachingHourDTO : IdentifiableDTO
    {
        public int Day { get; set; }
        public int Order { get; set; }
    }
}
