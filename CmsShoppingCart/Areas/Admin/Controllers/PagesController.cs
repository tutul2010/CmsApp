using CmsShoppingCart.Models.Data;
using CmsShoppingCart.Models.ViewModels.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CmsShoppingCart.Areas.Admin.Controllers
{
    public class PagesController : Controller
    {
        // GET: Admin/Pages
        public ActionResult Index()
        {
            List<PageVM> pagesList;
            using (Db context = new Db())
            {
                pagesList = context.Pages.ToArray().OrderBy(x => x.Sorting)
                                .Select(x => new PageVM(x)).ToList();
            }
            if (pagesList == null)
                return HttpNotFound();

                return View(pagesList);
        }
    }
}