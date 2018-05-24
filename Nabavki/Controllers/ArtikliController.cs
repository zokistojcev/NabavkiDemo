using CrystalDecisions.CrystalReports.Engine;
using Nabavki.Models;
using Nabavki.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace Nabavki.Controllers
{
    public class ArtikliController : Controller
    {
        private NabavkiDbContext _context;

        public ArtikliController()
        {
            _context = new NabavkiDbContext();
        }

        public ActionResult ArtickleByNaziv()
        {
            return View();
        }

        public ActionResult Export_artikli()
        {
            ReportDocument rd = new ReportDocument();
            rd.Load(Path.Combine(Server.MapPath("~/Report"), "ArtikliReport.rpt"));
            rd.SetDataSource(_context.Artikli.ToList());
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();

            try
            {
                Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                stream.Seek(0, SeekOrigin.Begin);
                return File(stream, "aplication/pdf", "Artikli_list.pdf");
            }
            catch (Exception)
            { 
                throw;
            }   
        }

        // GET: Artikl
        public ActionResult Index()
        {
            var artikli = _context.Artikli.ToList();

            List<ArtiklViewModel> model = new List<ArtiklViewModel>();

            foreach (var a in artikli)
            {
                var artikl = new ArtiklViewModel
                {
                    IdArtikli = a.IdArtikli,
                    Naziv = a.Naziv,
                    IdPartner = a.IdPartner,
                    Sifra = a.Sifra
                };
                model.Add(artikl);
            }
            return View(model);
        }

        [HttpGet]
        [Route("GetArtiklByNaziv/{query}")]
        public ActionResult GetByNaziv(string query)
        {
            ArtikliWebService.ArtikliWSSoapClient client = new ArtikliWebService.ArtikliWSSoapClient();
            var artid = client.ReturnArtiklId(query);        

            return Json(artid, JsonRequestBehavior.AllowGet);
        }

        public ActionResult New()
        {
            var firmi = _context.Firmi.ToList();

            List<FirmaViewModel> lfdvm = new List<FirmaViewModel>();

            foreach (var item in firmi)
            {
                FirmaViewModel fdvm = new FirmaViewModel
                {
                    Naziv = item.Naziv,
                    IdFirma = item.IdFirma
                };
                lfdvm.Add(fdvm);
            }

            AddArtiklViewModel model = new AddArtiklViewModel
            {
                Firmi = lfdvm
            };

            return View(model);
        }

        public ActionResult GetPartneri(int? id)
        {
            if (id==null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Greska");

            var firma = _context.Firmi.Find(id);
            if (firma == null)
                return HttpNotFound();

            List<PartnerViewModel> lpvm = new List<PartnerViewModel>();

            foreach (var item in firma.Partners)
            {
                var pvm = new PartnerViewModel
                {
                    Naziv = item.Naziv,
                    IdPartner = item.IdPartner,
                    IdFirma = item.IdFirma
                };
                lpvm.Add(pvm);
            }

            return Json(lpvm, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult SaveArtikl(AddArtiklViewModel artikl)
        {
            if (!ModelState.IsValid)
                return View("Edit", new { id = artikl.IdArtikli });

            Artikli art = new Artikli
            {
                IdPartner = artikl.IdPartner,
                IdArtikli = artikl.IdArtikli,
                Naziv = artikl.ArtiklNaziv,
                Sifra = artikl.ArtiklSifra
            };

            _context.Artikli.Add(art);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id ==null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Greska");

            var artikl = _context.Artikli.Find(id);
            if (artikl == null)
                return HttpNotFound();

            ArtiklViewModel avm = new ArtiklViewModel
            {
                Sifra = artikl.Sifra,
                Naziv = artikl.Naziv,
                IdPartner = artikl.IdPartner,
                IdArtikli = artikl.IdArtikli
            };

            return View(avm);
        }

        [HttpPost]
        public ActionResult UpdateArtikl(ArtiklViewModel artikl)
        {
            if (!ModelState.IsValid)
                return View("Edit", new { id = artikl.IdArtikli });
            
            var art =_context.Artikli.Find(artikl.IdArtikli);
            if (art == null)
                return HttpNotFound();

            art.IdArtikli = artikl.IdArtikli;
            art.IdPartner = artikl.IdPartner;
            art.Naziv = artikl.Naziv;
            art.Sifra = artikl.Sifra;

            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Greska");

            var artikl = _context.Artikli.Find(id);
            if (artikl == null)
                return HttpNotFound();

            _context.Artikli.Remove(artikl);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Greska");

            var artikl = _context.Artikli.Find(id);
            if (artikl == null)
                return HttpNotFound();

            var partner = _context.Partneri.Find(artikl.IdPartner);
            if (partner == null)
                return HttpNotFound();

            var firma = _context.Firmi.Find(partner.IdFirma);
            if (firma == null)
                return HttpNotFound();

            ArtiklDetailsViewModel avm = new ArtiklDetailsViewModel
            {
                ArtiklSifra = artikl.Sifra,
                ArtiklName = artikl.Naziv,
                FirmaName = firma.Naziv,
                PartnerName = partner.Naziv
            };

            return View(avm);
        }
    }
}



