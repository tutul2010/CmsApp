using CmsShoppingCart.Models.Data;
using CmsShoppingCart.Models.ViewModels.Shop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace CmsShoppingCart.Areas.Admin.Controllers
{
    public class ShopController : Controller
    {
        // GET: Admin/Shop/Categories
        public ActionResult Categories()
        {
            //get all categories frm DB
            List<CategoryVM> CategoryVMlst;
            try
            {
                using (var db = new Db())
                {
                    CategoryVMlst = db.Categories.ToArray()
                                        .OrderBy(x => x.Sorting)
                                        .Select(x => new CategoryVM(x)).ToList();

                }
                if (CategoryVMlst == null)
                    return HttpNotFound();
            }
            catch (Exception ex)
            {

                throw;
            }

            return View(CategoryVMlst);
        }
    }
}