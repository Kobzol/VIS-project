using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataTransfer;
using Web.WebService;

namespace Web.Models.Service
{
    public class ServiceProxy
    {
        private SchoolServiceClient client;

        public ServiceProxy()
        {
            this.client = new SchoolServiceClient();
        }

        public PersonDTO GetPerson(long id)
        {
            return this.client.GetPerson(id);
        }

        public bool IsLoginValid(string username, string password)
        {
            return this.client.IsLoginValid(username, password);
        }

        public bool AddSupplement(long subjectId, int day, int order, long teacherId)
        {
            return this.client.AddSupplement(subjectId, day, order, teacherId);
        }
    }
}