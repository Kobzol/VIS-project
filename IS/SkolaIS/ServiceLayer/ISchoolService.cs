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
        bool IsLoginValid(string username, string password);
    }
}
