using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using DataTransfer;

namespace ServiceLayer
{
    [ServiceContract]
    public interface ISchoolService
    {
        [OperationContract]
        PersonDTO GetPerson(long id);

        [OperationContract]
        IEnumerable<PersonDTO> GetTeachers();

        [OperationContract]
        bool IsLoginValid(string username, string password);

        [OperationContract]
        bool AddSupplement(long subjectId, int day, int order, long teacherId);

        [OperationContract]
        bool AddSupplementCancel(long subjectId, int day, int order);

        [OperationContract]
        IEnumerable<SupplementDTO> GetSupplements(long subjectId);

        [OperationContract]
        IEnumerable<GradeDTO> TestGetGrades();
    }
}
