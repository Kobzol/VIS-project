using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web.Controllers
{
    [Authorize]
    public class AbsenceController : BaseController
    {
        public ActionResult Index()
        {
            return View("Index", this.Person.Subjects);
        }
    }
}