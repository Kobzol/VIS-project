using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.Models.Login;

namespace Web.Controllers
{
    public class LoginController : BaseController
    {
        public ActionResult Index(LoginForm form)
        {
            if (ModelState.IsValid)
            {
                // TODO
            }

            return View(form);
        }
    }
}