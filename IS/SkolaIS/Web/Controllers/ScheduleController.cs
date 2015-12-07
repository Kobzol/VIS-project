using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web.Controllers
{
    [Authorize]
    public class ScheduleController : BaseController
    {
        public ActionResult Index()
        {
            return View("Index", this.Person.Subjects);
        }

        [HttpPost]
        public ActionResult ShowSupplement(long subjectId, int day, int order)
        {
            return Json(new { teachers = new [] {
                new { name = "Wayne", id = 3},
                new { name = "Stříbný", id = 4}
            } });
        }

        [HttpPost]
        public ActionResult AddSupplement(long subjectId, int day, int order, long teacherId)
        {
            this.ServiceProxy.AddSupplement(subjectId, day, order, teacherId);
            return Json("");
        }
    }
}