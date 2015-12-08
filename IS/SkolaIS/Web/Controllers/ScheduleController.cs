using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataTransfer;
using Web.Models.Schedule;

namespace Web.Controllers
{
    [Authorize]
    public class ScheduleController : BaseController
    {
        public ActionResult Index()
        {
            return View("Index",
                from subject
                in this.Person.Subjects
                select new ScheduleModel() { Subject = subject, Supplements = this.ServiceProxy.GetSupplements(subject.Id) }    
            );
        }

        [HttpPost]
        public ActionResult ShowSupplement(long subjectId, int day, int order)
        {
            List<PersonDTO> teachers = this.ServiceProxy.GetTeachers();
            List<PersonDTO> availableTeachers = new List<PersonDTO>();

            foreach (PersonDTO teacher in teachers)
            {
                bool valid = true;

                foreach (SubjectDTO subject in teacher.Subjects)
                {
                    IEnumerable<TeachingHourDTO> hours = from hour
                                                         in subject.Schedule.Hours
                                                         where hour.Day == day && hour.Order == order
                                                         select hour;

                    if (hours.Count() != 0)
                    {
                        valid = false;
                        break;
                    }
                }

                if (valid)
                {
                    availableTeachers.Add(teacher);
                }
            }

            return Json(new { teachers = availableTeachers });
        }

        [HttpPost]
        public ActionResult AddSupplement(long subjectId, int day, int order, long teacherId)
        {
            this.ServiceProxy.AddSupplement(subjectId, day, order, teacherId);
            return Json(true);
        }

        [HttpPost]
        public ActionResult SupplementCancel(long subjectId, int day, int order)
        {
            this.ServiceProxy.AddSupplementCancel(subjectId, day, order);
            return Json(true);
        }
    }
}