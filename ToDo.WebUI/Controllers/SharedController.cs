using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ToDo.WebUI.Controllers
{
    public class SharedController : Controller
    {
        //
        // GET: /Shared/

        public ActionResult LoginHeader()
        {

            string viewName = User.Identity.IsAuthenticated ? "UserHeader" : "LoginHeader";
            ViewBag.Username = User.Identity.Name;
            return View(viewName);
        }

    }
}
