﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CmsShoppingCart.Areas.Admin.Controllers
{
    public class DashbordController : Controller
    {
        // GET: Admin/Dashbord
        public ActionResult Index()
        {
            return View();
        }
    }
}