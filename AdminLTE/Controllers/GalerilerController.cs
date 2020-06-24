using AdminLTE.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AdminLTE.Controllers
{
    [RoutePrefix("Galeriler")]
    public class GalerilerController : Controller
    {
        // GET: Galeriler
        Model1 context = new Model1();
        public ActionResult Index()
        {
            return View();
        }

        [Route("Galeriler")]
        public ActionResult Galeriler()
        {
            var tip = context.GaleriTipis.ToList().Where(x => x.ustId == 0);
            ViewBag.Liste = tip;
            var gals = context.Galerilers.ToList();
            ViewBag.galeriListe = gals;
            var resim = context.GaleriResims.ToList();
            ViewBag.resimListe = resim;
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        [Route("Galeriler")]
        public ActionResult Galeriler(Galeriler galeriler)
        {
            try
            {
                if (galeriler != null)
                {
                    galeriler.sema = "Şema 1";
                    context.Galerilers.Add(galeriler);
                    context.SaveChanges();
                    return RedirectToAction("Galeriler", "Galeriler");
                }
                TempData["hata"] = "Hata";
                return RedirectToAction("Galeriler", "Galeriler");
            }
            catch
            {
                TempData["hata"] = "Hata";
                return RedirectToAction("Galeriler", "Galeriler");
            }
        }
        public ActionResult GaleriSil(int id)
        {
            context.Galerilers.Remove(context.Galerilers.FirstOrDefault(x => x.id == id));
            context.SaveChanges();
            return RedirectToAction("Galeriler", "Galeriler");
        }

        [Route("{id:int}")]
        public ActionResult GalerileriDuzenle(int id,int? page)
        {
            Galeriler galeriler = context.Galerilers.FirstOrDefault(x => x.id == id);
            ViewBag.Title = id;
            if(galeriler.tip == "Ürün Slider")
            {
                var sliderResim = context.GaleriResims.ToList().Where(x => x.galerilerId == id).ToList().ToPagedList(page ?? 1, 8);
                ViewBag.galeriResim = sliderResim;
            }
            else
            {
                var galeriResim = context.GaleriResims.ToList().Where(x => x.galerilerId == id);
                ViewBag.galeriResim = galeriResim;
            }

            var sayi = context.GaleriResims.ToList().Where(x => x.galerilerId == id).Count();
            ViewBag.sayi = sayi;

            var galerii = context.GaleriTipis.ToList().Where(x => x.ustId == 0); ;
            ViewBag.galeriListe = galerii;


            var bar = context.Galerilers.FirstOrDefault(x => x.id == id);
            var heyy = context.GaleriTipis.FirstOrDefault(x => x.ad == bar.tip);
            var galeriii = context.GaleriTipis.ToList().Where(x => x.ustId == heyy.id);
            ViewBag.galeriListee = galeriii;

            Galeriler galeri = context.Galerilers.FirstOrDefault(x => x.id == id);
            return View(galeri);

        }

        [HttpPost]
        [ValidateInput(false)]
        [Route("{id:int}")]
        public ActionResult GalerileriDuzenle(Galeriler galeriler)
        {
            try
            {

                Galeriler guncel = context.Galerilers.FirstOrDefault(x => x.id == galeriler.id);
                guncel.ad = galeriler.ad;
                guncel.tip = galeriler.tip;
                guncel.sema = galeriler.sema;
                guncel.genislik = galeriler.genislik;
                guncel.yukseklik = galeriler.yukseklik;
                guncel.aktif = galeriler.aktif;

                context.SaveChanges();
                return RedirectToAction("GalerileriDuzenle", "Galeriler", new { id = galeriler.id });
            }
            catch
            {
                return RedirectToAction("Galeriler", "Galeriler");
            }
        }
        string ResimKaydet(HttpPostedFileBase file)
        {
            Image orj = Image.FromStream(file.InputStream);
            Bitmap kucuk = new Bitmap(orj, 250, 250);
            string dosyaadi = Path.GetFileNameWithoutExtension(file.FileName) + Guid.NewGuid() + Path.GetExtension(file.FileName);
            orj.Save(Server.MapPath("~/Content/images/" + dosyaadi));
            kucuk.Save(Server.MapPath("~/Content/images/small" + dosyaadi));
            return dosyaadi;
        }

        [HttpPost]
        public ActionResult GaleriResimEkle(GaleriResim galeriresim, HttpPostedFileBase Link)
        {
            try
            {
                string uzanti = Path.GetExtension(Link.FileName).ToLower();
                string[] Uzantilar = new[] { ".jpg", ".png" };
                if (uzanti == ".jpg" || uzanti == ".png")
                {
                    string dosyaadi = ResimKaydet(Link);
                    galeriresim.link = "/Content/images/" + dosyaadi;
                    galeriresim.kucukLink = "/Content/images/small/" + dosyaadi;
                }
                else
                {
                    return Json(false, JsonRequestBehavior.AllowGet);
                }
                var file = galeriresim.link;
                if (file != null)
                {
                    GaleriResim img = new GaleriResim();
                    img.link = galeriresim.link;
                    foreach (string u in Uzantilar)
                    {
                        if (uzanti == u)
                        {
                            if (ModelState.IsValid)
                            {
                                using (context)
                                {

                                    context.GaleriResims.Add(galeriresim);
                                    context.SaveChanges();


                                    return RedirectToAction("GalerileriDuzenle", "Galeriler", new { id = galeriresim.galerilerId });
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
    }
}