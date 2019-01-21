using System;
using System.Web.Mvc;
using NHibernate;
using NHibernate.Linq;
using System.Linq;

using KozmetickiClassLibrary.Model;
using KozmetickiClassLibrary.ViewModels;
using KozmetickiClassLibrary.Interfaces;
using System.Net;
using System.Collections.Generic;

namespace KozmetickiSalonWeb.Controllers
{
    public class ZaposleniksController : Controller
    {
      
        private IRepository zaposlenikRepository;

        public ZaposleniksController() {
            this.zaposlenikRepository = new Repository();
        }

        // GET: Zaposleniks
        public ActionResult Index()
        {
           
                var employees = zaposlenikRepository.GetZaposleniksQuery().Where(z => z.Salon.IdSalon == AktivniSalon.IdAktivniSalon).Select(z => new ZaposlenikVM()
                {
                    IdZaposlenik = z.IdZaposlenik,
                    Ime = z.Ime,
                    Prezime = z.Prezime,
                    ImePrezime = z.Ime + " " + z.Prezime,
                    Oib = z.Oib,
                    IdAdresa = z.Adresa.IdAdresa,
                    Adresa =  new AdresaVM()
                    {
                        IdAdresa = z.Adresa.IdAdresa,
                        Naziv = z.Adresa.Nazivadrese,
                        IdGrad = z.Adresa.Grad.IdGrad,
                        Grad = new GradVM()
                        {
                            IdGrad = z.Adresa.Grad.IdGrad,
                            Naziv = z.Adresa.Grad.NazivGrada,
                            Pbr = z.Adresa.Grad.Pbr
                        }
                    },
                    IdSalon = z.Salon.IdSalon,
                    IdSmjena = z.Smjena.IdSmjena,
                    Smjena = new SmjenaVM()
                    {
                        IdSmjena = z.Smjena.IdSmjena,
                        Naziv = z.Smjena.SmjenaVal,
                        TimeEnd = z.Smjena.TimeEnd,
                        TimeStart = z.Smjena.TimeStart
                    }

                }).ToList();


                var usluge = zaposlenikRepository.GetZaposlenikUsluga();
            foreach (var z in employees)
            {
                foreach (var x in usluge)
                {
                    if (x.Zaposlenik.IdZaposlenik == z.IdZaposlenik)
                    {
                        z.Usluge.Add(new UslugaVM()
                        {
                            Idusluga = x.Usluga.Idusluga,
                            Naziv = x.Usluga.Naziv,
                            Opis = x.Usluga.Opis,
                            Cijena = x.Usluga.Cijena,
                            IdKategorija = x.Usluga.Kategorija.IdKategorija,
                            Trajanje = x.Usluga.Trajanje,
                            Kategorija = new KategorijaVM()
                            {
                                IdKategorija = x.Usluga.Kategorija.IdKategorija,
                                Naziv = x.Usluga.Kategorija.NazivKategorija
                            }
                        });
                    }
                }
            }
                
                return View(employees.ToList());
            


        }
        public ActionResult Profit(int? id) {
            Zaposlenik z = zaposlenikRepository.GetZaposlenikByID(id);
            List<decimal> profits = new List<decimal>();
            for (int i = 1; i < 13; ++i) {
                decimal profit = Zaposlenik.GetZaposlenikProfitByMonth(z.Narudzba, i,2019);
                profits.Add(profit);
            }
            profits.Add(Convert.ToDecimal(id));
            return View(profits);
            
        }
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
           
                Zaposlenik z = zaposlenikRepository.GetZaposlenikByID(id);

            ZaposlenikVM zaposlenik = new ZaposlenikVM()
                {
                    IdZaposlenik = z.IdZaposlenik,
                    Ime = z.Ime,
                    Prezime = z.Prezime,
                    ImePrezime = z.Ime + " " + z.Prezime,
                    Oib = z.Oib,
                    IdAdresa = z.Adresa.IdAdresa,
                    IdSalon = z.Salon.IdSalon,
                    IdSmjena = z.Smjena.IdSmjena,
                    Adresa = new AdresaVM()
                    {
                        IdAdresa = z.Adresa.IdAdresa,
                        Naziv = z.Adresa.Nazivadrese,
                        IdGrad = z.Adresa.Grad.IdGrad,
                        Grad = new GradVM()
                        {
                            IdGrad = z.Adresa.Grad.IdGrad,
                            Naziv = z.Adresa.Grad.NazivGrada,
                            Pbr = z.Adresa.Grad.Pbr
                        }
                    },

                    Smjena = new SmjenaVM()
                    {
                        IdSmjena = z.Smjena.IdSmjena,
                        Naziv = z.Smjena.SmjenaVal,
                        TimeEnd = z.Smjena.TimeEnd,
                        TimeStart = z.Smjena.TimeStart
                    }
                };



                var usluge = zaposlenikRepository.GetZaposlenikUsluga();
                foreach (var x in usluge)
                {
                   
                        if (x.Zaposlenik.IdZaposlenik == z.IdZaposlenik)
                        {
                            zaposlenik.Usluge.Add(new UslugaVM()
                            {
                                Idusluga = x.Usluga.Idusluga,
                                Naziv = x.Usluga.Naziv,
                                Opis = x.Usluga.Opis,
                                Cijena = x.Usluga.Cijena,
                                IdKategorija = x.Usluga.Kategorija.IdKategorija,
                                Trajanje = x.Usluga.Trajanje,
                                Kategorija = new KategorijaVM()
                                {
                                    IdKategorija = x.Usluga.Kategorija.IdKategorija,
                                    Naziv = x.Usluga.Kategorija.NazivKategorija
                                }
                            });
                        }
                    
                }
                

                if (zaposlenik == null)
                {
                    return HttpNotFound();
                }
                return View(zaposlenik);
            
        }
       // GET: Zaposleniks/Create
        public ActionResult Create()
        {
           
                var grad = zaposlenikRepository.GetGrad().AsEnumerable();
                var smjena = zaposlenikRepository.GetSmjena().AsEnumerable();
                ViewBag.idGrad = new SelectList(grad, "idGrad", "nazivGrada");
                ViewBag.idSmjena = new SelectList(smjena, "idSmjena", "smjenaVal");
                ZaposlenikAdresaVM za = new ZaposlenikAdresaVM();
                za.usluge = zaposlenikRepository.GetUsluga().Select(u => new UslugaVM()
                {
                    Idusluga = u.Idusluga,
                    Naziv = u.Naziv,
                    Odabrana = false
                }).ToList();
                return View(za);
            
        }

        // POST: Zaposleniks/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ZaposlenikAdresaVM za)
        {

            
                if (ModelState.IsValid)
            {
               
                    Adresa ad = new Adresa();
                    int id;
                    ad.Grad = za.adresa.Grad;
                    ad.Nazivadrese = za.adresa.Nazivadrese;
                    id = zaposlenikRepository.InsertAdresa(ad);
                    Zaposlenik zaposlenik = za.zaposlenik;
                    ad = zaposlenikRepository.GetAdresa().Where(b => b.IdAdresa == id).FirstOrDefault();
                    zaposlenik.Adresa = ad;
                   // zaposlenik.Adresa.IdAdresa = ad.IdAdresa;
                     zaposlenik.Salon = zaposlenikRepository.GetSalon().Where(s => s.IdSalon == AktivniSalon.IdAktivniSalon).FirstOrDefault();
                //zaposlenik.Salon.IdSalon = AktivniSalon.IdAktivniSalon;

                zaposlenikRepository.InsertZaposlenik(zaposlenik);
                  
                    foreach (var u in za.usluge)
                    {
                        if (u.Odabrana)
                        {
                          
                           
                            Zaposlenikusluga zu = new Zaposlenikusluga()
                            {
                                Usluga = zaposlenikRepository.GetUslugaByID(u.Idusluga),
                                 Zaposlenik = zaposlenikRepository.GetZaposlenikByID(zaposlenik.IdZaposlenik)

                            };
                        zaposlenikRepository.InsertZaposlenikusluga(zu);
                        }
                    }
                   
                    return RedirectToAction("Index");
                   
                }
                var grad = zaposlenikRepository.GetGrad().AsEnumerable();
                var smjena = zaposlenikRepository.GetSmjena().AsEnumerable();
                ViewBag.idGrad = new SelectList(grad, "idGrad", "nazivGrada", za.adresa.Grad.IdGrad);
                ViewBag.idSmjena = new SelectList(smjena, "idSmjena", "smjenaVal", za.zaposlenik.Smjena.IdSmjena);
            
            
            return View(za);
        }

        // GET: Zaposleniks/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
           

                Zaposlenik zaposlenik = zaposlenikRepository.GetZaposlenikByID(id);
                if (zaposlenik == null)
                {
                    return HttpNotFound();
                }
                ZaposlenikAdresaVM za = new ZaposlenikAdresaVM()
                {
                    zaposlenik = zaposlenik,
                    adresa = zaposlenik.Adresa,
                    usluge = new List<UslugaVM>()
                };

                foreach (var u in zaposlenikRepository.GetUsluga().ToList())
                {

                    if (zaposlenikRepository.GetZaposlenikUsluga().Any(x => x.Zaposlenik.IdZaposlenik == id && x.Usluga.Idusluga == u.Idusluga))
                    {
                        za.usluge.Add(new UslugaVM()
                        {
                            Idusluga = u.Idusluga,
                            Naziv = u.Naziv,
                            Odabrana = true
                        });
                    }
                    else
                    {
                        za.usluge.Add(new UslugaVM()
                        {
                            Idusluga = u.Idusluga,
                            Naziv = u.Naziv,
                            Odabrana = false
                        });
                    }
                }
                var grad = zaposlenikRepository.GetGrad().AsEnumerable();
                var smjena = zaposlenikRepository.GetSmjena().AsEnumerable();
                ViewBag.idGrad = new SelectList(grad, "idGrad", "nazivGrada", za.adresa.Grad.IdGrad);
                ViewBag.idSmjena = new SelectList(smjena, "idSmjena", "smjenaVal", za.zaposlenik.Smjena.IdSmjena);
                return View(za);
            

        }

        // POST: Zaposleniks/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ZaposlenikAdresaVM za)
        {

                Zaposlenik zap= zaposlenikRepository.GetZaposlenikByID(za.zaposlenik.IdZaposlenik);

                if (ModelState.IsValid)
                {
                    var ad = za.adresa;
                    var gr = zaposlenikRepository.GetGradByID(za.adresa.Grad.IdGrad);
                    if (!((ad.Nazivadrese.Equals(zap.Adresa.Nazivadrese) || ad.Nazivadrese == zap.Adresa.Nazivadrese)
                        && ad.Grad.IdGrad == zap.Adresa.Grad.IdGrad))
                    {
                        ad.Grad = gr;
                        zaposlenikRepository.InsertAdresa(ad);
                      
                        zap.Adresa = ad;
                    }

                    zap.Ime = za.zaposlenik.Ime;
                    zap.Prezime = za.zaposlenik.Prezime;
                    zap.Oib = za.zaposlenik.Oib;
                    zap.Smjena = zaposlenikRepository.GetSmjenaByID(za.zaposlenik.Smjena.IdSmjena);

                zaposlenikRepository.InsertZaposlenik(zap);
                    foreach (var u in za.usluge)
                    {
                        if (u.Odabrana && !zaposlenikRepository.GetZaposlenikUsluga().Any(x => x.Usluga.Idusluga == u.Idusluga && x.Zaposlenik.IdZaposlenik == zap.IdZaposlenik))
                        {
                            Zaposlenikusluga zu = new Zaposlenikusluga()
                            {
                                Zaposlenik = zaposlenikRepository.GetZaposlenikByID(zap.IdZaposlenik),
                             
                                Usluga = zaposlenikRepository.GetUslugaByID(u.Idusluga) 
                            };
                        zaposlenikRepository.InsertZaposlenikusluga(zu);
                          
                        }

                        if (!u.Odabrana && zaposlenikRepository.GetZaposlenikUsluga().Any(x => x.Usluga.Idusluga == u.Idusluga && x.Zaposlenik.IdZaposlenik == zap.IdZaposlenik))
                        {
                            Zaposlenikusluga zu = zaposlenikRepository.GetZaposlenikUsluga().Where(x => x.Usluga.Idusluga == u.Idusluga && x.Zaposlenik.IdZaposlenik == zap.IdZaposlenik).FirstOrDefault();
                           zaposlenikRepository.DeleteZaposlenikusluga(zu);
                       
                        }
                    }

                zaposlenikRepository.Commit();
                    return RedirectToAction("Index");
                }
                var grad = zaposlenikRepository.GetGrad().AsEnumerable();
                var smjena = zaposlenikRepository.GetSmjena().AsEnumerable();
                ViewBag.idGrad = new SelectList(grad, "idGrad", "nazivGrada", za.adresa.Grad.IdGrad);
                ViewBag.idSmjena = new SelectList(smjena, "idSmjena", "smjenaVal", za.zaposlenik.Smjena.IdSmjena);
                return View(zap);
            
        }

        // GET: Zaposleniks/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
           
                Zaposlenik zaposlenik = zaposlenikRepository.GetZaposlenikByID(id);

                if (zaposlenik == null)
                {
                    return HttpNotFound();
                }
                return View(zaposlenik);
            
        }

        // POST: Zaposleniks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
           
                Zaposlenik zaposlenik = zaposlenikRepository.GetZaposlenikByID(id);
            foreach (var i in zaposlenik.Zaposlenikusluga) {
                Zaposlenikusluga zaposlenikusl = zaposlenikRepository.GetZaposlenikUslugaByID(i.IdZaposlenikUsluga);
                zaposlenikRepository.DeleteZaposlenikusluga(zaposlenikusl);
            }
           
           
            zaposlenikRepository.DeleteZaposlenik(zaposlenik);
           
                return RedirectToAction("Index");
            
        }

       
    }
}
