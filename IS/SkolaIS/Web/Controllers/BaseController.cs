using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using DataTransfer;
using Web.Models.Authentication;
using Web.Models.Service;

namespace Web.Controllers
{
    public class BaseController : Controller
    {
        public UserPrincipal LoggedUser { get { return this.IsUserLoggedIn() ? (UserPrincipal) base.User : null; } }
        public ServiceProxy ServiceProxy { get; private set; }
        public PersonDTO Person { get; private set; }

        public string PageName { get; private set; }

        protected override void Initialize(RequestContext requestContext)
        {
            base.Initialize(requestContext);

            this.ServiceProxy = new ServiceProxy();
            
            if (this.IsUserLoggedIn())
            {
                this.Person = this.ServiceProxy.GetPerson(this.LoggedUser.PersonId);
            }

            this.PageName = requestContext.RouteData.Values["controller"].ToString().ToLower();
        }

        public bool IsUserLoggedIn()
        {
            return this.User.Identity.IsAuthenticated;
        }
    }
}