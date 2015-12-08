using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace DataTransfer
{
    [DataContract]
    public enum RoomDTO : uint
    {
        [EnumMember]
        A201 = 1,
        [EnumMember]
        A202,
        [EnumMember]
        B305
    }
}
