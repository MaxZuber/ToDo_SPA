using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;

namespace ToDo.WebUI.Controllers
{
    [Authorize]
    public class TaskBoardController : Controller
    {
        //
        // GET: /TaskBoard/

        public ActionResult Index()
        {
            ClaimsPrincipal user = HttpContext.User as ClaimsPrincipal;


            foreach(Claim c in user.Claims )
            {
                if(c.Type == ClaimTypes.NameIdentifier)
                {
                    ViewBag.UserID = c.Value;
                }

            }

            //ViewBag.UserID = user.Identity.Name;
            return View();
        }

    }
}
