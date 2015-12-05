using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataTransfer;

namespace Web.Models.Subject
{
    public class SubjectModel
    {
        public SubjectDTO Subject { get; set; }
        public IEnumerable<TestDTO> Tests { get; set; }
    }
}