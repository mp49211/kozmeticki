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
    public class PocetnaController : Controller
    {
        ISession session = NHibertnateSession.OpenSession();
        // GET: Pocetna
        public ActionResult Index()
        {
            PocetnaVM pocetna = new PocetnaVM
            {
                Datum = DateTime.Today,
                Zaposlenici = new List<ZaposlenikVM>()
            };

            List<Narudzba> narudzbe = session.Query<Narudzba>().Where(y => y.Salon.IdSalon == AktivniSalon.IdAktivniSalon).OrderBy(n => n.Vrijeme).ToList();
            narudzbe.RemoveAll(a => a.Vrijeme.Date.CompareTo(DateTime.Today) != 0);
            List<int> ids = new List<int>();
            foreach (Narudzba n in narudzbe)
            {
                if (!ids.Contains(n.Zaposlenik.IdZaposlenik))
                {
                    ids.Add(n.Zaposlenik.IdZaposlenik);
                }
            }

            foreach (int id in ids)
            {
                ZaposlenikVM zaposlenik = session.Query<Zaposlenik>().Select(z=> new ZaposlenikVM()
                {
                    IdZaposlenik = z.IdZaposlenik,
                    ImePrezime = z.Ime + " " + z.Prezime

                }).FirstOrDefault(z => z.IdZaposlenik == id);
         

                foreach (var n in narudzbe)
                {
                    if (n.Zaposlenik.IdZaposlenik == id)
                    {

                        zaposlenik.Narudzbe.Add(new NarudzbaVM()
                        {
                            IdNarudzba = n.IdNarudzba,
                            Vrijeme = n.Vrijeme,
                            IdUsluga = n.Usluga.Idusluga,
                            IdZaposlenik = n.Zaposlenik.IdZaposlenik,
                            Klijent = n.Klijent,
                            Kontakt = n.Kontakt,
                            Usluga = new UslugaVM()
                            {
                                Idusluga = n.Usluga.Idusluga,
                                Cijena = n.Usluga.Cijena,
                                Naziv = n.Usluga.Naziv,
                                Trajanje = n.Usluga.Trajanje
                            }
                        });
                    }
                }
                pocetna.Zaposlenici.Add(zaposlenik);
            }

            double x = pocetna.Zaposlenici.Count / 2.0;
            pocetna.Broj = (int)Math.Round(x, MidpointRounding.AwayFromZero);

            return View(pocetna);
        }

        // GET: Pocetna/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Narudzba narudzba = session.Get<Narudzba>(id);
            if (narudzba == null)
            {
                return HttpNotFound();
            }
            return View(narudzba);
        }

        // GET: Pocetna/Create
        public ActionResult Create()
        {
            var salon = session.Query<Salon>().AsEnumerable();
            var zaposlenik = session.Query<Zaposlenik>().AsEnumerable();
            var usluga = session.Query<Usluga>().AsEnumerable();
            ViewBag.idSalon = new SelectList(salon, "idSalon", "naziv");
            ViewBag.idZaposlenik = new SelectList(zaposlenik, "idZaposlenik", "ime");
            ViewBag.idUsluga = new SelectList(usluga, "idusluga", "naziv");
            return View();
        }

        // POST: Pocetna/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idNarudzba,klijent,kontakt,idZaposlenik,idSalon,vrijeme,idUsluga")] Narudzba narudzba)
        {
            if (ModelState.IsValid)
            {
                using (ITransaction transaction = session.BeginTransaction())   //  Begin a transaction
                {
                    session.Save(narudzba);
                    transaction.Commit();   //  Commit the changes to the database
                }
                return RedirectToAction("Index");
            }

            var salon = session.Query<Salon>().AsEnumerable();
            var zaposlenik = session.Query<Zaposlenik>().AsEnumerable();
            var usluga = session.Query<Usluga>().AsEnumerable();
            ViewBag.idSalon = new SelectList(salon, "idSalon", "naziv", narudzba.IdNarudzba);
            ViewBag.idZaposlenik = new SelectList(zaposlenik, "idZaposlenik", "ime", narudzba.Zaposlenik.IdZaposlenik);
            ViewBag.idUsluga = new SelectList(usluga, "idusluga", "naziv", narudzba.Usluga.Idusluga);
            return View(narudzba);
        }

        // GET: Pocetna/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Narudzba narudzba = session.Get<Narudzba>(id);
            if (narudzba == null)
            {
                return HttpNotFound();
            }
            var salon = session.Query<Salon>().AsEnumerable();
            var zaposlenik = session.Query<Zaposlenik>().AsEnumerable();
            var usluga = session.Query<Usluga>().AsEnumerable();
            ViewBag.idSalon = new SelectList(salon, "idSalon", "naziv", narudzba.IdNarudzba);
            ViewBag.idZaposlenik = new SelectList(zaposlenik, "idZaposlenik", "ime", narudzba.Zaposlenik.IdZaposlenik);
            ViewBag.idUsluga = new SelectList(usluga, "idusluga", "naziv", narudzba.Usluga.Idusluga);
            return View(narudzba);
        }

        // POST: Pocetna/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idNarudzba,klijent,kontakt,idZaposlenik,idSalon,vrijeme,idUsluga")] Narudzba narudzba)
        {
            if (ModelState.IsValid)
            {
                using (ITransaction transaction = session.BeginTransaction())   //  Begin a transaction
                {
                    session.Save(narudzba); //  Save the book in session
                    transaction.Commit();   //  Commit the changes to the database
                }
                return RedirectToAction("Index");
            }
            var salon = session.Query<Salon>().AsEnumerable();
            var zaposlenik = session.Query<Zaposlenik>().AsEnumerable();
            var usluga = session.Query<Usluga>().AsEnumerable();
            ViewBag.idSalon = new SelectList(salon, "idSalon", "naziv", narudzba.IdNarudzba);
            ViewBag.idZaposlenik = new SelectList(zaposlenik, "idZaposlenik", "ime", narudzba.Zaposlenik.IdZaposlenik);
            ViewBag.idUsluga = new SelectList(usluga, "idusluga", "naziv", narudzba.Usluga.Idusluga);
            return View(narudzba);
        }

        // GET: Pocetna/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Narudzba narudzba = session.Get<Narudzba>(id);
            if (narudzba == null)
            {
                return HttpNotFound();
            }
            return View(narudzba);
        }

        // POST: Pocetna/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Narudzba narudzba = session.Get<Narudzba>(id);
            using (ITransaction transaction = session.BeginTransaction())   //  Begin a transaction
            {
                session.Delete(narudzba); //  Save the book in session
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