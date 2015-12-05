using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.Models.Authentication;
using Web.Models.Service;

namespace Web.Controllers
{
    public class BaseController : Controller
    {
        public new UserPrincipal User { get { return (UserPrincipal) base.User; } }
        public ServiceProxy ServiceProxy { get; private set; }

        protected override void Initialize(System.Web.Routing.RequestContext requestContext)
        {
            base.Initialize(requestContext);

            this.ServiceProxy = new ServiceProxy();
        }
    }
}