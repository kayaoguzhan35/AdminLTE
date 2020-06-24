
using AdminLTE.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AdminLTE.Controllers
{
    public class DosyalarController : Controller
    {
        // GET: Dosyalar
        Model1 context = new Model1();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Dosyalar()
        {
            return View(context.Dosyas.ToList());
        }

        string ResimKaydet(HttpPostedFileBase file)
        {
            Image orj = Image.FromStream(file.InputStream);
            string dosyaadi = Path.GetFileNameWithoutExtension
            (file.FileName) + Guid.NewGuid() + Path.GetExtension(file.FileName);
            orj.Save(Server.MapPath("~/Content/images/" + dosyaadi));
            return dosyaadi;
        }
        public JsonResult ResimEkle(Dosya dosya, HttpPostedFileBase link)
        {
            try
            {
                string uzanti = Path.GetExtension(link.FileName).ToLower();
                string[] Uzantilar = new[] { ".jpg", ".png", ".docx", ".xlsx", ".pdf" };

                if(uzanti == ".jpg" || uzanti == ".png")
                {
                    string dosyaadi = ResimKaydet(link);
                    dosya.link = "/Content/images/" + dosyaadi;
                }
                else if(uzanti == ".docx" || uzanti == ".xlsx" || uzanti == ".pdf")
                {
                    string wep = Path.GetFileNameWithoutExtension(link.FileName) + Guid.NewGuid() + Path.GetExtension(link.FileName);
                    string dosyaadi = wep;
                    dosya.link = "/Content/images/" + dosyaadi;
                }
                else
                {
                    return Json(false, JsonRequestBehavior.AllowGet);
                }
                int imgId = 0;
                var file = dosya.link;
                
                if (file != null)
                {
                    BinaryReader reader = new BinaryReader(link.InputStream);
                    Dosya img = new Dosya();
                    img.link = dosya.link;
                    foreach (string u in Uzantilar)
                    {
                        if(uzanti == u)
                        {
                            if(ModelState.IsValid)
                            {
                                using (context)
                                {
                                    context.Dosyas.Add(img);
                                    context.SaveChanges();
                                    imgId = img.id;
                                }
                            }
                        }
                    }
                }
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }

        }

        public ActionResult ResimSil(int id)
        {
            context.Dosyas.Remove(context.Dosyas.FirstOrDefault(x => x.id == id));
            context.SaveChanges();
            return RedirectToAction("Dosyalar", "Dosyalar");
        }

        [Route("MenuTipi")]
        public ActionResult MenuNavbar(int id = 1)
        {
            var menuTipi = context.MenuTipis.ToList();
            ViewBag.menuTipi = menuTipi;


            Menuler menuler = context.Menulers.FirstOrDefault(x => x.id == id);
            return View(menuler);
        }

        [HttpPost]
        [Route("MenuTipi")]
        public ActionResult MenuNavbar(Menuler menuler)
        {

            Menuler guncel = context.Menulers.FirstOrDefault(x => x.id == menuler.id);

            guncel.ad = menuler.ad;
            guncel.renk = menuler.renk;

            context.SaveChanges();
            return RedirectToAction("MenuNavbar", "Dosyalar", new { id = menuler.id });
        }
        public ActionResult Menu()
        {
            var menuTip = context.Menulers.FirstOrDefault(x => x.id == 1);
            ViewBag.menuTipii = menuTip.ad;
            return View(context.Menus.ToList());
        }
    }
}