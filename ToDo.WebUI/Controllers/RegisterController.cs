using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ToDo.Dal.Abstract;
using ToDo.Entities;
using ToDo.WebUI.Models;

namespace ToDo.WebUI.Controllers
{
    public class RegisterController : Controller
    {
        //
        // GET: /Register/

        private readonly IUserRepository _userRepository;

        public RegisterController(IUserRepository userRepository)
        {
            this._userRepository = userRepository;
        }

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

                tblUsers newUser = new tblUsers()
                {
                    Mail = user.Mail,
                    Password = user.Password,
                    Username = user.Username
                };

                newUser = _userRepository.Register(newUser);


                if (newUser == null)
                {
                    ModelState.AddModelError("Username", "Username already exists");
                }
                else
                {
                    return View("SuccessfullRegistration");
                }

                

            }

            return View();
        }


        
    }
}
