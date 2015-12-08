using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace DataTransfer
{
    [DataContract]
    public class PersonDTO : IdentifiableDTO
    {
        [DataMember(Name="Name")]
        public string Name { get; set; }
        [DataMember(Name = "Surname")]
        public string Surname { get; set; }
        [DataMember(Name = "BirthDate")]
        public DateTime BirthDate { get; set; }
        [DataMember(Name = "Email")]
        public string Email { get; set; }
        [DataMember(Name = "Role")]
        public PersonRoleDTO Role { get; set; }
        [DataMember(Name = "Subjects")]
        public IEnumerable<SubjectDTO> Subjects { get; set; }
        [DataMember(Name = "Class")]
        public ClassDTO Class { get; set; }

        public bool IsStudent()
        {
            return this.Role == PersonRoleDTO.Student;
        }
        public bool IsTeacher()
        {
            return this.Role == PersonRoleDTO.Teacher;
        }
    }
}
