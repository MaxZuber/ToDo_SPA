﻿using System;
using System.Collections.Generic;
using System.Linq;
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
            return View();
        }

    }
}
