using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;

namespace Web.Models.Authentication
{
    public class UserPrincipal : IUserPrincipal
    {
        public long PersonId { get; private set; }
        public IIdentity Identity { get; private set; }

        public UserPrincipal(string name, long personId)
        {
            this.Identity = new GenericIdentity(name);
            this.PersonId = personId;
        }

        public bool IsInRole(string role)
        {
            return false;
        }
    }
}