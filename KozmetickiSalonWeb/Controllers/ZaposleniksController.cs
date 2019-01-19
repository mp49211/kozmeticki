using System;
using System.Web.Mvc;
using NHibernate;
using NHibernate.Linq;
using System.Linq;

using KozmetickiClassLibrary.Model;
using KozmetickiClassLibrary.ViewModels;
using System.Net;
using System.Collections.Generic;

namespace KozmetickiSalonWeb.Controllers
{
    public class ZaposleniksController : Controller
    {
        ISession session = NHibertnateSession.OpenSession();
        // GET: Zaposleniks
        public ActionResult Index()
        {
           
                var employees = session.Query<Zaposlenik>().Where(z => z.Salon.IdSalon == AktivniSalon.IdAktivniSalon).Select(z => new ZaposlenikVM()
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


                var usluge = session.Query<Zaposlenikusluga>().ToList();
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
            Zaposlenik z = session.Get<Zaposlenik>(id);
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
           
                Zaposlenik z = session.Get<Zaposlenik>(id);
            
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



                var usluge = session.Query<Zaposlenikusluga>().ToList();
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
           
                var grad = session.Query<Grad>().AsEnumerable();
                var smjena = session.Query<Smjena>().AsEnumerable();
                ViewBag.idGrad = new SelectList(grad, "idGrad", "nazivGrada");
                ViewBag.idSmjena = new SelectList(smjena, "idSmjena", "smjenaVal");
                ZaposlenikAdresaVM za = new ZaposlenikAdresaVM();
                za.usluge = session.Query < Usluga > ().Select(u => new UslugaVM()
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
                    using (ITransaction transaction = session.BeginTransaction())   //  Begin a transaction
                    {
                     id = (int)session.Save(ad); //  Save the book in session
                        transaction.Commit();   //  Commit the changes to the database
                    }
                    Zaposlenik zaposlenik = za.zaposlenik;
                    ad = session.Query<Adresa>().Where(b => b.IdAdresa == id).FirstOrDefault();
                    zaposlenik.Adresa = ad;
                   // zaposlenik.Adresa.IdAdresa = ad.IdAdresa;
                     zaposlenik.Salon = session.Query<Salon>().Where(s => s.IdSalon == AktivniSalon.IdAktivniSalon).FirstOrDefault();
                      //zaposlenik.Salon.IdSalon = AktivniSalon.IdAktivniSalon;
                   
                    using (ITransaction transaction = session.BeginTransaction())   //  Begin a transaction
                    {
                        session.Save(zaposlenik); //  Save the book in session
                        transaction.Commit();   //  Commit the changes to the database
                    }
                  
                    foreach (var u in za.usluge)
                    {
                        if (u.Odabrana)
                        {
                          
                            var usl=session.Get<Usluga>(u.Idusluga);
                            Zaposlenikusluga zu = new Zaposlenikusluga()
                            {
                                Usluga = session.Get<Usluga>(u.Idusluga),
                                Zaposlenik = session.Get<Zaposlenik>(zaposlenik.IdZaposlenik)

                            };
                            using (ITransaction transaction = session.BeginTransaction())   //  Begin a transaction
                            {
                                session.Save(zu); //  Save the book in session
                                transaction.Commit();   //  Commit the changes to the database
                            }
                        }
                    }
                   
                    return RedirectToAction("Index");
                   
                }
                var grad = session.Query<Grad>().AsEnumerable();
                var smjena = session.Query<Smjena>().AsEnumerable();
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
           

                Zaposlenik zaposlenik = session.Get<Zaposlenik>(id);
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

                foreach (var u in session.Query<Usluga>().ToList())
                {

                    if (session.Query<Zaposlenikusluga>().Any(x => x.Zaposlenik.IdZaposlenik == id && x.Usluga.Idusluga == u.Idusluga))
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
                var grad = session.Query<Grad>().AsEnumerable();
                var smjena = session.Query<Smjena>().AsEnumerable();
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

                Zaposlenik zap= session.Get<Zaposlenik>(za.zaposlenik.IdZaposlenik);

                if (ModelState.IsValid)
                {
                    var ad = za.adresa;
                    var gr = session.Get<Grad>(za.adresa.Grad.IdGrad);
                    if (!((ad.Nazivadrese.Equals(zap.Adresa.Nazivadrese) || ad.Nazivadrese == zap.Adresa.Nazivadrese)
                        && ad.Grad.IdGrad == zap.Adresa.Grad.IdGrad))
                    {
                        ad.Grad = gr;
                        using (ITransaction transaction = session.BeginTransaction())   //  Begin a transaction
                        {
                            session.Save(ad); //  Save the book in session
                            transaction.Commit();   //  Commit the changes to the database
                        }
                      
                        zap.Adresa = ad;
                    }

                    zap.Ime = za.zaposlenik.Ime;
                    zap.Prezime = za.zaposlenik.Prezime;
                    zap.Oib = za.zaposlenik.Oib;
                    zap.Smjena = session.Get<Smjena>(za.zaposlenik.Smjena.IdSmjena);

                    using (ITransaction transaction = session.BeginTransaction())   //  Begin a transaction
                    {
                        session.Merge(zap); //  Save the book in session
                        transaction.Commit();   //  Commit the changes to the database
                    }
                    foreach (var u in za.usluge)
                    {
                        if (u.Odabrana && !session.Query<Zaposlenikusluga>().Any(x => x.Usluga.Idusluga == u.Idusluga && x.Zaposlenik.IdZaposlenik == zap.IdZaposlenik))
                        {
                            Zaposlenikusluga zu = new Zaposlenikusluga()
                            {
                                Zaposlenik = session.Get<Zaposlenik>(zap.IdZaposlenik),
                             
                                Usluga = session.Get<Usluga>(u.Idusluga) 
                            };
                            session.Save(zu);
                        }

                        if (!u.Odabrana && session.Query<Zaposlenikusluga>().Any(x => x.Usluga.Idusluga == u.Idusluga && x.Zaposlenik.IdZaposlenik == zap.IdZaposlenik))
                        {
                            Zaposlenikusluga zu = session.Query<Zaposlenikusluga>().Where(x => x.Usluga.Idusluga == u.Idusluga && x.Zaposlenik.IdZaposlenik == zap.IdZaposlenik).FirstOrDefault();
                            session.Delete(zu);
                        }
                    }
                    using (ITransaction transaction = session.BeginTransaction())   //  Begin a transaction
                    {
                     
                        transaction.Commit();   //  Commit the changes to the database
                    }
                    return RedirectToAction("Index");
                }
                var grad = session.Query<Grad>().AsEnumerable();
                var smjena = session.Query<Smjena>().AsEnumerable();
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
           
                Zaposlenik zaposlenik = session.Get<Zaposlenik>(id);

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
           
                Zaposlenik zaposlenik = session.Get<Zaposlenik>(id);
            foreach (var i in zaposlenik.Zaposlenikusluga) {
                Zaposlenikusluga zaposlenikusl = session.Get<Zaposlenikusluga>(i.IdZaposlenikUsluga);
                session.Delete(zaposlenikusl);
            }
           
           
            session.Delete(zaposlenik);
                using (ITransaction transaction = session.BeginTransaction())   //  Begin a transaction
                {
                
                    transaction.Commit();   //  Commit the changes to the database
                }
                return RedirectToAction("Index");
            
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
               
                    session.Dispose();
                
            }
            base.Dispose(disposing);
        }
    }
}
