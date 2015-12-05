using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataTransfer;

namespace Web.Controllers
{
    [Authorize]
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            if (this.Person.Role == PersonRoleDTO.Student)
            {
                return RedirectToAction("Index", "Grade");
            }
            else return RedirectToAction("Index", "Schedule");
        }
    }
}