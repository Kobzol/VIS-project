using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Newtonsoft.Json;
using Web.Models.Authentication;
using Web.Models.Login;

namespace Web.Controllers
{
    public class LoginController : BaseController
    {
        [HttpGet]
        public ActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }

            return View("Index", new LoginForm());
        }

        [HttpPost]
        public ActionResult Index(LoginForm form)
        {
            if (ModelState.IsValid)
            {
                if (this.IsLoginValid(form.Username, form.Password))
                {
                    UserPrincipalModel userModel = new UserPrincipalModel() { PersonId = form.Username == "student" ? 1 : 2 };
                    string json = JsonConvert.SerializeObject(userModel);

                    FormsAuthenticationTicket authTicket = new FormsAuthenticationTicket(
                             1,
                             form.Username,
                             DateTime.Now,
                             DateTime.Now.AddMonths(1),
                             true,
                             json);

                    string encTicket = FormsAuthentication.Encrypt(authTicket);
                    HttpCookie faCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encTicket);
                    Response.Cookies.Add(faCookie);

                    return RedirectToAction("Index");
                }

                form.Error = "Wrong username/password combination";
            }

            return View("Index", form);
        }

        private bool IsLoginValid(string username, string password)
        {
            return  (username == "student" && password == "student") ||
                    (username == "teacher" && password == "teacher");
        }
    }
}