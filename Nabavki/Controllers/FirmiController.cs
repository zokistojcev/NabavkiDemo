using Nabavki.Models;
using Nabavki.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Nabavki.Controllers
{
    public class FirmiController : Controller
    {
        private NabavkiDbContext _context;

        public FirmiController()
        {
            _context = new NabavkiDbContext();
        }

        // GET: Firma
        public ActionResult Index()
        {
            var firmi = _context.Firmi.ToList();

            var model = new List<FirmaViewModel>();
            foreach (var firma in firmi)
            {
                var f = new FirmaViewModel
                {
                    IdFirma = firma.IdFirma,
                    Naziv = firma.Naziv
                };
                model.Add(f);
            }

            return View(model);
        }

        public ActionResult New()
        {
            var model = new FirmaViewModel();

            return View(model);
        }

        [HttpPost]
        public ActionResult New(FirmaViewModel firma)
        {
            if (!ModelState.IsValid)
                return View("New", firma);

            Firma f = new Firma
            {
                Naziv = firma.Naziv
            };

            _context.Firmi.Add(f);
            _context.SaveChanges();

            return RedirectToAction("Details", new { id = f.IdFirma });
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Greska");
            
            var firma = _context.Firmi.Find(id);
            if (firma == null)
                return HttpNotFound();

            FirmaViewModel fdvm = new FirmaViewModel
            {
                Naziv = firma.Naziv,
                IdFirma = firma.IdFirma
            };

            foreach (var partner in firma.Partners)
            {
                var part = new PartnerViewModel
                {
                    IdPartner = partner.IdPartner,
                    Naziv = partner.Naziv,
                    IdFirma = partner.IdFirma
                };
                fdvm.Partneri.Add(part);
            }

            return View(fdvm);
        }

        [HttpPost]
        public ActionResult AddPartner(PartnerViewModel partner)
        {
            if (!ModelState.IsValid)
                return View("Details", new { id = partner.IdPartner });
            
            Partner part = new Partner
            {
                Naziv = partner.Naziv,
                IdFirma = partner.IdFirma
            };

            _context.Partneri.Add(part);
            _context.SaveChanges();

            var partneri = _context.Partneri.Where(x => x.IdFirma == partner.IdFirma).ToList();

            List<PartnerViewModel> lpvm = new List<PartnerViewModel>();

            foreach (var p in partneri)
            {
                var pvm = new PartnerViewModel
                {
                    IdFirma = p.IdFirma,
                    Naziv = p.Naziv,
                    IdPartner = p.IdPartner
                };
                lpvm.Add(pvm);
            }

            return RedirectToAction("GetPartners", new { id = partner.IdFirma });
        }

        public ActionResult GetPartners(int id)
        {
            var partneri = _context.Partneri.Where(x => x.IdFirma == id).ToList();

            List<PartnerViewModel> lpvm = new List<PartnerViewModel>();

            foreach (var p in partneri)
            {
                var pvm = new PartnerViewModel
                {
                    IdFirma = p.IdFirma,
                    Naziv = p.Naziv,
                    IdPartner = p.IdPartner
                };
                lpvm.Add(pvm);
            }
            return Json(lpvm, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Greska");

            var firma = _context.Firmi.Find(id);
            if (firma == null)
                return HttpNotFound();

            FirmaViewModel fvm = new FirmaViewModel
            {
                IdFirma = firma.IdFirma,
                Naziv = firma.Naziv
            };

            List<PartnerViewModel> lpvm = new List<PartnerViewModel>();

            foreach (var partner in fvm.Partneri)
            {
                PartnerViewModel pvm = new PartnerViewModel
                {
                    IdFirma = partner.IdFirma,
                    Naziv = partner.Naziv,
                    IdPartner = partner.IdPartner
                };
                lpvm.Add(pvm);
            }

            fvm.Partneri = lpvm;
            
            return View(fvm);
        }

        [HttpPost]
        public ActionResult UpdateFirma(FirmaViewModel firma)
        {
            if (!ModelState.IsValid)
                return RedirectToAction("Edit", new { id = firma.IdFirma});
            
            var firmaDb = _context.Firmi.Find(firma.IdFirma);

            if (firmaDb == null)
                return HttpNotFound();

            firmaDb.Naziv = firma.Naziv;
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        
        public ActionResult Delete(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Greska");

            var firma = _context.Firmi.Find(id);
            if (firma == null)
                return HttpNotFound();

            var partneri = _context.Partneri.Where(x => x.IdFirma == firma.IdFirma);

            foreach (var item in partneri)
            {
                var artikli = _context.Artikli.Where(x => x.IdPartner == item.IdPartner);
     
                _context.Artikli.RemoveRange(artikli);
            }

            _context.Partneri.RemoveRange(partneri);

            _context.Firmi.Remove(firma);

            _context.SaveChanges();

            return RedirectToAction("Index");
        }

    }
}