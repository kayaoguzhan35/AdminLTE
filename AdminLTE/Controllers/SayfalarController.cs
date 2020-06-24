using AdminLTE.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AdminLTE.Controllers
{
    public class SayfalarController : Controller
    {
        // GET: Sayfalar
        Model1 context = new Model1();


        public ActionResult SayfaEkle()
        {
            var asd = context.Menus.ToList().Where(x => x.ustId == 0);
            ViewBag.Liste = asd;
            return View();
        }

        [HttpPost]
        public ActionResult SayfaEkle(Menu menu)
        {
            try {
                if (menu != null)
            {
                menu.aktif = true;
                context.Menus.Add(menu);
                context.SaveChanges();
            }
            return RedirectToAction("SayfaEkle", "Sayfalar");

        }

            catch
            {
                TempData["hata"] = "HATA";
                return RedirectToAction("SayfaEkle,Sayfalar");
            }
        }
       

        public ActionResult SayfaSil(int id)
        {
            context.Menus.Remove(context.Menus.FirstOrDefault(x => x.id == id));
            context.SaveChanges();
            return RedirectToAction("Dosyalar", "Dosyalar");

        }
    }
}