using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using KozmetickiClassLibrary.Model;
using KozmetickiClassLibrary.ViewModels;
using NHibernate;

namespace KozmetickiSalonWeb.Controllers
{
    public class UslugasController : Controller
    {
        ISession session = NHibertnateSession.OpenSession();
        public ActionResult Index()
        {
            List<UslugaVM> usluge = session.Query<Salonusluga>()
                .Where(u => u.Salon.IdSalon == AktivniSalon.IdAktivniSalon).Select(u => new UslugaVM()
                {
                    Idusluga = u.Usluga.Idusluga,
                    Naziv = u.Usluga.Naziv,
                    Cijena = u.Usluga.Cijena,
                    Opis = u.Usluga.Opis,
                    Trajanje = u.Usluga.Trajanje,
                    IdKategorija = u.Usluga.Kategorija.IdKategorija

                }).ToList();

            var employees = session.Query<Zaposlenik>().Where(z => z.Salon.IdSalon == AktivniSalon.IdAktivniSalon).Select(z => new ZaposlenikVM()
            {
                IdZaposlenik = z.IdZaposlenik,
                IdSalon = z.Salon.IdSalon,
                ImePrezime = z.Ime + " " + z.Prezime
            }).ToList();
            List<Zaposlenikusluga> zaposlenici = session.Query<Zaposlenikusluga>().ToList();
            foreach (var u in usluge)
            {
                u.Kategorija = session.Query<Kategorija>().Select(k => new KategorijaVM()
                {
                    IdKategorija = k.IdKategorija,
                    Naziv = k.NazivKategorija
                }).FirstOrDefault(k => k.IdKategorija == u.IdKategorija);
                
                foreach (Zaposlenikusluga z in zaposlenici)
                {
                    if (z.Zaposlenik.IdZaposlenik == 0) continue;
                    if (z.Usluga.Idusluga == u.Idusluga && z.Zaposlenik.Salon.IdSalon == AktivniSalon.IdAktivniSalon)
                    {
                        u.Zaposlenici.Add(new ZaposlenikVM()
                        {
                            IdZaposlenik = z.Zaposlenik.IdZaposlenik,
                            ImePrezime = z.Zaposlenik.Ime + " " + z.Zaposlenik.Prezime
                        });
                    }
                }
            }
            return View(usluge.ToList());
        }

        // GET: Uslugas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Usluga usluga = session.Get<Usluga>(id);
            {
                return HttpNotFound();
            }
            return View(usluga);
        }

        // GET: Uslugas/Create
        public ActionResult Create()
        {
            ZaposlenikAdresaVM usluge = new ZaposlenikAdresaVM
            {
                usluge = new List<UslugaVM>()
            };
            usluge.zaposlenik = session.Query<Zaposlenik>().First();
            bool postoji;
            foreach (var u in session.Query<Usluga>().ToList())
            {
                postoji = false;
                foreach (var s in session.Query<Salonusluga>().ToList())
                {
                    if (s.Salon.IdSalon== AktivniSalon.IdAktivniSalon && s.Usluga.Idusluga == u.Idusluga)
                    {
                        postoji = true;
                    }
                }
                if (postoji)
                {
                    usluge.usluge.Add(new UslugaVM()
                    {
                        Idusluga = u.Idusluga,
                        Naziv = u.Naziv,
                        Cijena = u.Cijena,
                        IdKategorija = u.Kategorija.IdKategorija,
                        Odabrana = true,
                        Opis = u.Opis,
                        Trajanje = u.Trajanje,
                        Kategorija =  new KategorijaVM()
                        {
                            IdKategorija = u.Kategorija.IdKategorija,
                            Naziv = u.Kategorija.NazivKategorija
                        }
                    });
                }
                else
                {
                    usluge.usluge.Add(new UslugaVM()
                    {
                        Idusluga = u.Idusluga,
                        Naziv = u.Naziv,
                        Cijena = u.Cijena,
                        IdKategorija = u.Kategorija.IdKategorija,
                        Odabrana = false,
                        Opis = u.Opis,
                        Trajanje = u.Trajanje,
                        Kategorija = new KategorijaVM()
                        {
                            IdKategorija = u.Kategorija.IdKategorija,
                            Naziv = u.Kategorija.NazivKategorija
                        }
                    });
                }
            }
            System.Diagnostics.Debug.WriteLine(usluge.usluge.Count);
            return View(usluge);
        }

        // POST: Uslugas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ZaposlenikAdresaVM za)
        {
            List<UslugaVM> usl = za.usluge;
            if (ModelState.IsValid)
            {
                System.Diagnostics.Debug.WriteLine(usl.Count);
                foreach (var u in usl)
                {
                    System.Diagnostics.Debug.WriteLine(u.Idusluga);
                    System.Diagnostics.Debug.WriteLine(u.Naziv);
                    System.Diagnostics.Debug.WriteLine(u.Odabrana);
                    if (u.Odabrana && !session.Query<Salonusluga>().Any(s => s.Salon.IdSalon == AktivniSalon.IdAktivniSalon && s.Usluga.Idusluga == u.Idusluga))
                    {
                        Salonusluga su = new Salonusluga()
                        {
                            Usluga = session.Get<Usluga>(u.Idusluga),
                            Salon = session.Get<Salon>(AktivniSalon.IdAktivniSalon)
                    };
                        session.Save(su);
                    }

                    if (!u.Odabrana && session.Query<Salonusluga>().Any(s => s.Salon.IdSalon == AktivniSalon.IdAktivniSalon && s.Usluga.Idusluga == u.Idusluga))
                    {
                        Salonusluga su = session.Query<Salonusluga>()
                            .FirstOrDefault(s => s.Salon.IdSalon == AktivniSalon.IdAktivniSalon && s.Usluga.Idusluga == u.Idusluga);
                        session.Delete(su);
                    }
                }
                //db.usluga.Add(usluga);
                using (ITransaction transaction = session.BeginTransaction())   //  Begin a transaction
                {
                    transaction.Commit();   //  Commit the changes to the database
                }

                return RedirectToAction("Index");
            }

            return View(za);
        }

        // GET: Uslugas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Usluga usluga = session.Get<Usluga>(id);
            if (usluga == null)
            {
                return HttpNotFound();
            }
            var kategorija = session.Query<Kategorija>().AsEnumerable();
            ViewBag.idKategorija = new SelectList(kategorija, "idKategorija", "nazivKategorija", usluga.Kategorija.IdKategorija);
            return View(usluga);
        }

        // POST: Uslugas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idusluga,naziv,cijena,trajanje,opis,idKategorija")] Usluga usluga)
        {
            if (ModelState.IsValid)
            {
                using (ITransaction transaction = session.BeginTransaction())   //  Begin a transaction
                {
                    session.Delete(usluga); //  Save the book in session
                    transaction.Commit();   //  Commit the changes to the database
                }
                return RedirectToAction("Index");
            }
            var kategorija = session.Query<Kategorija>().AsEnumerable();
            ViewBag.idKategorija = new SelectList(kategorija, "idKategorija", "nazivKategorija", usluga.Kategorija.IdKategorija);
            return View(usluga);
        }

        // GET: Uslugas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Usluga usluga = session.Get<Usluga>(id);
            if (usluga == null)
            {
                return HttpNotFound();
            }
            return View(usluga);
        }

        // POST: Uslugas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Usluga usluga = session.Get<Usluga>(id);
            session.Delete(usluga);
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