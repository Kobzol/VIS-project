using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace DataTransfer
{
    [DataContract]
    public enum PersonRoleDTO
    {
        [EnumMember]
        Student = 1,
        [EnumMember]
        Teacher,
        [EnumMember]
        Administrator
    }
}
