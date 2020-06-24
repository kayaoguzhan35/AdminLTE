using AdminLTE.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AdminLTE.Controllers
{
    [RoutePrefix("Menu")]
    public class HomeController : Controller
    {
        
        Model1 context = new Model1();
        [Route("{id:int}")]
        public ActionResult Index(int id)
        {
            ViewBag.Title = id;
            Menu m = context.Menus.FirstOrDefault(x => x.id == id);
            var asd = context.Menus.ToList().Where(x => x.ustId == 0 && x.id != id);
            ViewBag.Liste = asd;
            return View(m);
        }
        
        [HttpPost]
        [ValidateInput(false)]
        [Route("{id:int}")]
        public ActionResult Index(Menu menu)
        {
            Menu guncel = context.Menus.FirstOrDefault(x => x.id == menu.id);
            guncel.ad = menu.ad;
            guncel.ustId = menu.ustId;
            guncel.sira = menu.sira;
            guncel.icerik = menu.icerik;
            guncel.dislink = menu.dislink;
            guncel.aktif = menu.aktif;
            context.SaveChanges();
            return RedirectToAction("Index", "Home");
        }

        public ActionResult SidebarGet()
        {
            return View(context.Menus.ToList());
        }
        
    }
}