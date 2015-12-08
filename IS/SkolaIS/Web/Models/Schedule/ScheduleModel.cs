using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataTransfer;

namespace Web.Models.Schedule
{
    public class ScheduleModel
    {
        public SubjectDTO Subject { get; set; }
        public IEnumerable<SupplementDTO> Supplements { get; set; }
    }
}