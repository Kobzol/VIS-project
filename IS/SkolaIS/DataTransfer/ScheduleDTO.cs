using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTransfer
{
    [Serializable]
    public class ScheduleDTO : IdentifiableDTO
    {
        public IEnumerable<TeachingHourDTO> Hours { get; set; }
    }
}
