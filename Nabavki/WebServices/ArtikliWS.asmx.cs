using Nabavki.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace Nabavki.WebServices
{
    /// <summary>
    /// Summary description for ArtikliWS
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class ArtikliWS : System.Web.Services.WebService
    {
        private NabavkiDbContext _context;

        public ArtikliWS()
        {
            _context = new NabavkiDbContext();
        }

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }

        [WebMethod]
        public int ReturnArtiklId(string naziv)
        {
            if (String.IsNullOrEmpty(naziv))
            {
                return 0;
            }

            var artikl = _context.Artikli.Where(a => a.Naziv == naziv).FirstOrDefault();
            if (artikl == null)
                return 0;

            return artikl.IdArtikli;
        }
    }
}
