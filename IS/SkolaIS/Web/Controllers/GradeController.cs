using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataTransfer;
using Web.Models.Grade;
using Web.Models.Subject;

namespace Web.Controllers
{
    [Authorize]
    public class GradeController : BaseController
    {
        public ActionResult Index()
        {
            dynamic obj = this.ViewData["pendingGrade"];

            if (obj != null)
            {
                ViewBag.TestGrade = obj;
            }

            return View("Index", this.Person.Subjects);
        }

        [HttpPost]
        public ActionResult AddTestGrade(long subject, double value, double weight)
        {
            this.ViewData["pendingGrade"] = new TestGrade() { SubjectId = subject, Value = value, Weight = weight };

            return Index();
        }
    }
}