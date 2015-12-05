using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTransfer
{
    [Serializable]
    public class PersonDTO : IdentifiableDTO
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime BirthDate { get; set; }
        public string Email { get; set; }
        public PersonRoleDTO Role { get; set; }
        public IEnumerable<SubjectDTO> Subjects { get; set; }
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
