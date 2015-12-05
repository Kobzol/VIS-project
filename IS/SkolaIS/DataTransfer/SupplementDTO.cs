using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTransfer
{
    [Serializable]
    public class Supplement : IdentifiableDTO
    {
        public bool IsHourCanceled { get; set; }
        public DateTime Date { get; set; }
        public TeachingHourDTO Hour { get; set; }
        public ScheduleDTO Schedule { get; set; }
    }
}
