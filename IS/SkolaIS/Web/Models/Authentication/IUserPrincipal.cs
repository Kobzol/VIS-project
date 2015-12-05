using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Web.Models.Authentication
{
    public interface IUserPrincipal : IPrincipal
    {
        long PersonId { get; }
    }
}
