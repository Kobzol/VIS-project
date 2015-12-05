using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.Controllers;

namespace Web.Models.View
{
    public abstract class BaseViewPage : WebViewPage
    {
        public BaseController BasePage { get { return this.ViewContext.Controller as BaseController; } }
    }
}