using KozmetickiClassLibrary.Interfaces;
using KozmetickiClassLibrary.Model;
using KozmetickiClassLibrary.ViewModels;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace KozmetickiSalonWeb.Controllers
{
    public class HomeController : Controller
    {
        private IRepository homeRepository;

        public HomeController()
        {
            this.homeRepository = new Repository();
        }

        public ActionResult Index()
        {
            
                var salon = homeRepository.GetSalon().AsEnumerable();
                
                ViewBag.idSalon = new SelectList(salon, "idSalon", "naziv");
                return View();
            
        }

        [HttpPost]
        public ActionResult Index([Bind(Include = "idSalon, naziv")] Salon salon)
        {
            AktivniSalon.IdAktivniSalon = salon.IdSalon;
            AktivniSalon.Postoji = true;
            
                AktivniSalon.Naziv = homeRepository.GetSalon().Where(b => b.IdSalon == salon.IdSalon).FirstOrDefault().Naziv;
         

         

            return RedirectToAction("Index", "Pocetna");

        }

        public ActionResult About()
        {
            
                var nar = homeRepository.GetNarudzba().Where(a => a.Salon.IdSalon == AktivniSalon.IdAktivniSalon).Select(a => new NarudzbaVM()
                {

                    IdNarudzba = a.IdNarudzba,
                    IdUsluga = a.Usluga.Idusluga,
                    IdZaposlenik = a.Zaposlenik.IdZaposlenik,
                    Klijent = a.Klijent,
                    Kontakt = a.Kontakt,
                    Vrijeme = a.Vrijeme,
                    IdSalon = a.Salon.IdSalon,
                    Zaposlenik =  new ZaposlenikVM()
                    {
                        IdZaposlenik = a.Zaposlenik.IdZaposlenik,
                        Ime = a.Zaposlenik.Ime,
                        Prezime = a.Zaposlenik.Prezime

                    },






                }).ToList();
                return View(nar);

            
       
        }
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}