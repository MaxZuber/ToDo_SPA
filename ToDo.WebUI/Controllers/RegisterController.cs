using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ToDo.WebUI.Models;

namespace ToDo.WebUI.Controllers
{
    public class RegisterController : Controller
    {
        //
        // GET: /Register/

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(RegisterModel user)
        {

            if(ModelState.IsValid)
            {


                ViewBag.Username = user.Username;

                return View("SuccessfullRegistration");
            }
            return View();
        }

    }
}
