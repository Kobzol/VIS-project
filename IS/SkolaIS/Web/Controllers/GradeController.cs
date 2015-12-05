using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataTransfer;

namespace Web.Controllers
{
    [Authorize]
    public class GradeController : BaseController
    {
        public ActionResult Index()
        {
            PersonDTO person = this.ServiceProxy.GetPerson(this.User.PersonId);

            return View("Index", person.Subjects);
        }
    }
}