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
        // GET: Admin/Pages/addpage
        public ActionResult AddPage()
        {
            return View();
        }
        // POST: Admin/Pages/addpage
        [HttpPost,ValidateAntiForgeryToken]
        public ActionResult AddPage(PageVM model)
        {
            if(!ModelState.IsValid)
            {
                return View(model);
            }
            using (var db = new Db())
            {
                string slug;
                var dto = new pageDTO();
                dto.Title = model.Title;
                if (string.IsNullOrWhiteSpace(model.Slug))
                    slug = model.Title.Replace(" ", "-").ToLower();
                else
                    slug = model.Slug.Replace(" ", "-").ToLower();
                //chk sure title and slug is unique
                if(db.Pages.Any(x=>x.Title== model.Title ) || db.Pages.Any(x => x.Slug == model.Slug))
                {
                    ModelState.AddModelError("", "that title or slug is already exist");
                    return View(model);
                }
                //set dto
                dto.Slug = slug;
                dto.Body = model.Body;
                dto.HasSidebar = model.HasSidebar;
                dto.Sorting = 100;
                //save
                db.Pages.Add(dto);
                db.SaveChanges();                    

            }
            //set msg
            TempData["SM"] = "You have added a new page !";
            //redirect
            return RedirectToAction("AddPage");
        }

        // GET: Admin/Pages/editpage/id
        public ActionResult EditPage(int id)
        {
            PageVM model;
            using (var Db = new Db())
            {
                pageDTO dto = Db.Pages.Find(id); //get data to dto frm DB
                if (dto == null)
                    return HttpNotFound();

                model = new PageVM(dto); //load dto data to pageVM

            }
                return View(model);
        }
        // POST: Admin/Pages/editpage/id
        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult EditPage(PageVM model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            int id = model.Id;
            string slug="home";
            using (var db = new Db())
            {

                //find the data
                pageDTO dto = db.Pages.Find(id);
               
                 dto.Title = model.Title;
                if (model.Slug != "home")
                {
                    if (string.IsNullOrWhiteSpace(model.Slug))
                        slug = model.Title.Replace(" ", "-").ToLower();
                    else
                        slug = model.Slug.Replace(" ", "-").ToLower();
                }

                //chk sure title and slug is unique
                if (db.Pages.Where(x => x.Id != id).Any(x =>x.Title==model.Title) || 
                            db.Pages.Where(x => x.Id != id).Any(x => x.Slug == slug))
                {
                    ModelState.AddModelError("", "that title or slug is already exist !");
                    return View(model);
                }
                //set dto
                dto.Slug = slug;
                dto.Body = model.Body;
                dto.HasSidebar = model.HasSidebar;
               
                //update
                db.SaveChanges();

            }
            //set msg
            TempData["SM"] = "You have edited a  page !";
            //redirect
            return RedirectToAction("AddPage");
        }

        // GET: Admin/Pages/Details/id
        public ActionResult Details(int id)
        {
            PageVM model;
            using (var Db = new Db())
            {
                pageDTO dto = Db.Pages.Find(id); //get data to dto frm DB
                if (dto == null)
                    return HttpNotFound();

                model = new PageVM(dto); //load dto data to pageVM

            }
            return View(model);
        }
        // GET: Admin/Pages/Delete/id       
        public ActionResult Delete(int id)
        {
          
            using (var db = new Db())
            {
             
                //find the data
                pageDTO dto = db.Pages.Find(id);
                if (dto == null)
                    return HttpNotFound();
                //remove
                db.Pages.Remove(dto);
                db.SaveChanges();

            }        
            //redirect
            return RedirectToAction("Index");
        }
        // POST: Admin/Pages/RecorderPages    
        [HttpPost] 
        public void RecorderPages(int[] id)
        {
            using (var db = new Db())
            {
                int cnt = 1;
                pageDTO dto;
                //set soring for each pages
                foreach(var pageId in id)
                {
                    dto = db.Pages.Find(pageId);
                    dto.Sorting = cnt;
                    db.SaveChanges();
                    cnt++;
                }
            }
        }

        // GET: Admin/Pages/EditSidebar/
        public ActionResult EditSidebar()
        {
            SidebarVM model;
            using (var Db = new Db())
            {
                SidebarDTO dto = Db.Sidebar.Find(1); //get data to dto frm DB
                if (dto == null)
                    return HttpNotFound();

                model = new SidebarVM(dto); //load dto data to pageVM

            }
            return View(model);
        }
        // POST: Admin/Pages/EditSidebar 
        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult EditSidebar(SidebarVM model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
           
            using (var db = new Db())
            {
                //get data
                SidebarDTO dto = db.Sidebar.Find(1);

                //set dto
                dto.Body = model.Body;
                //update
                db.SaveChanges();

            }
            //set msg
            TempData["SM"] = "You have edited a  sidebar !";
            //redirect
            return RedirectToAction("EditSidebar");
        }
    }
}