using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace DataTransfer
{
    [DataContract]
    public enum AbsenceTypeDTO
    {
        [EnumMember]
        SchoolAction = 1,
        [EnumMember]
        Absence
    }
}
