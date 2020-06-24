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
    [RoutePrefix("Urunler")]
    public class UrunlerController : Controller
    {
        // GET: Urunler
        Model1 context = new Model1();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult UrunEkle()
        {
            var urunListe = context.Urunlers.ToList();
            ViewBag.urunListe = urunListe;
            return View();
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult UrunEkle(Urunler urunler)
        {
            try
            {
                if (urunler != null)
                {
                    context.Urunlers.Add(urunler);
                    context.SaveChanges();
                    return RedirectToAction("UrunEkle", "Urunler");
                }
                TempData["hata"] = "Hata!";
                return RedirectToAction("UrunEkle", "Urunler");
            }
            catch
            {
                TempData["hata"] = "Hata! Lütfen gerekli yerleri doldurunuz!";
                return RedirectToAction("UrunEkle", "Urunler");
            }
        }
        [Route("{id:int}")]
        public ActionResult UrunDuzenle(int id)
        {
            var urunlerResim = context.GaleriResims.ToList().Where(x => x.urunlerId == id).ToList();
            ViewBag.urunlerResim = urunlerResim;


            Urunler urunler = context.Urunlers.FirstOrDefault(x => x.id == id);
            return View(urunler);
        }
        [HttpPost]
        [ValidateInput(false)]
        [Route("{id:int}")]
        public ActionResult UrunDuzenle(Urunler urunler)
        {
            try
            {
                Urunler guncel = context.Urunlers.FirstOrDefault(x => x.id == urunler.id);
                guncel.ad = urunler.ad;
                guncel.fiyat = urunler.fiyat;
                guncel.aktif = urunler.aktif;
                guncel.bilgi = urunler.bilgi;
                guncel.detaylıBilgi = urunler.detaylıBilgi;
                context.SaveChanges();
                return RedirectToAction("UrunDuzenle", "Urunler", new { id = urunler.id });
            }
            catch
            {

                return RedirectToAction("UrunDuzenle", "Urunler", new { id = urunler.id });
            }

        }
        public ActionResult UrunSil(int id)
        {
            context.Urunlers.Remove(context.Urunlers.FirstOrDefault(x => x.id == id));
            context.SaveChanges();
            return RedirectToAction("UrunEkle", "Urunler");
        }
        string ResimKaydet(HttpPostedFileBase file)
        {
            Image orj = Image.FromStream(file.InputStream);
            Bitmap kck = new Bitmap(orj, 250, 250);
            string dosyaadi = Path.GetFileNameWithoutExtension(file.FileName) + Guid.NewGuid() + Path.GetExtension(file.FileName);
            orj.Save(Server.MapPath("~/Content/images/" + dosyaadi));
            kck.Save(Server.MapPath("~/Content/images/small/" + dosyaadi));
            return dosyaadi;
        }
        [HttpPost]
        public ActionResult UruneResimEkle(GaleriResim gr, HttpPostedFileBase link)
        {
            try
            {
                string uzanti = Path.GetExtension(link.FileName).ToLower();
                string[] Uzantilar = new[] { ".jpg", ".png" };
                if (uzanti == ".jpg" || uzanti == ".png")
                {
                    string dosyaadi = ResimKaydet(link);
                    gr.link = "/Content/images/" + dosyaadi;
                    gr.kucukLink = "/Content/images/small/" + dosyaadi;
                }
                else
                {
                    return Json(false, JsonRequestBehavior.AllowGet);
                }
                var file = gr.link;
                if (file != null)
                {
                    GaleriResim img = new GaleriResim();
                    img.link = gr.link;
                    img.urunlerId = gr.urunlerId;
                    foreach (string u in Uzantilar)
                    {
                        if (uzanti == u)
                        {
                            if (ModelState.IsValid)
                            {
                                using (context)
                                {

                                    context.GaleriResims.Add(gr);
                                    context.SaveChanges();


                                    return RedirectToAction("UrunDuzenle", "Urunler", new { id = gr.urunlerId });
                                }
                            }
                        }
                    }
                }

                return Json(false, JsonRequestBehavior.AllowGet);

            }
            catch
            {

                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult ResimSil(int id)
        {
            var hey = context.GaleriResims.FirstOrDefault(x => x.id == id);
            int? qwe = hey.urunlerId;
            context.GaleriResims.Remove(context.GaleriResims.FirstOrDefault(x => x.id == id));
            context.SaveChanges();
            return RedirectToAction("UrunDuzenle", "Urunler", new { id = qwe });
        }
    }
}