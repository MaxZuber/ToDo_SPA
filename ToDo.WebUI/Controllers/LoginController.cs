using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ToDo.Dal.Abstract;
using ToDo.Entities;
using ToDo.WebUI.Models;
using ToDo.WebUI.Services;

namespace ToDo.WebUI.Controllers
{
    public class LoginController : Controller
    {
        //
        // GET: /Login/


        private readonly ISecurityService _securityService;
        private readonly IUserRepository _userRepository;
        public LoginController(IUserRepository userRepository, ISecurityService securityService)
        {
            this._userRepository = userRepository;
            this._securityService = securityService;
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(LoginModel user)
        {

            if(ModelState.IsValid)
            {



                bool IsSuccessful = _securityService.Login(user.Username, user.Username, user.IsPersistent);

                if (IsSuccessful)
                {
                    return Redirect("~/TaskBoard");
                }
                else
                {
                    ModelState.AddModelError("Username", "Login or password is incorrect");
                }
            }

            return View();
        }

        public ActionResult Logout()
        {

            _securityService.Logout();
            return Redirect("~/TaskBoard");
        }

    }
}
