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
    public class artiklsController : Controller
    {
        ISession session = NHibertnateSession.OpenSession();
        // GET: artikls
        public ActionResult Index()
        {

            ArtikliVM artikli = new ArtikliVM();
            foreach (var x in session.Query<Artiklsalon>().ToList())
            {
                if (x.Salon.IdSalon == AktivniSalon.IdAktivniSalon)
                {
                    artikli.Arts.Add(x.Artikl);
                    artikli.Kols.Add(x.Kolicina);

                }
            }
            return View(artikli);
        }

        [HttpPost]
        public ActionResult Index(ArtikliVM artikli)
        {
            for (int i = 0; i < artikli.Arts.Count; i++)
            {
                int id = artikli.Arts[i].IdArtikl;
                Artiklsalon x = session.Query<Artiklsalon>().FirstOrDefault(f => (f.Salon.IdSalon == AktivniSalon.IdAktivniSalon && f.Artikl.IdArtikl == id));
                if (artikli.Kols[i] != x.Kolicina)
                {
                    if (artikli.Kols[i] > 0)
                    {
                        x.Kolicina = artikli.Kols[i];
                        using (ITransaction transaction = session.BeginTransaction())   //  Begin a transaction
                        {
                            session.Save(x); //  Save the book in session
                            transaction.Commit();   //  Commit the changes to the database
                        }
                    }
                    else
                    {
                        session.Delete(x);
                    }

                    using (ITransaction transaction = session.BeginTransaction())   //  Begin a transaction
                    {
                      
                        transaction.Commit();   //  Commit the changes to the database
                    }

                }
            }
            return RedirectToAction("Index");

        }

        // GET: artikls/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Artikl artikl = session.Get<Artikl>(id);
            if (artikl == null)
            {
                return HttpNotFound();
            }
            ArtiklVM artik = new ArtiklVM()
            {
                IdArtikl = artikl.IdArtikl,
                Naziv = artikl.Naziv,
                Cijena = artikl.Cijena,
                IdDobavljac = artikl.Dobavljac.Iddobavljac,
                IdKategorija = artikl.Kategorija.IdKategorija,
                IdProizvodac = artikl.Proizvodac.IdProizvodac,
                Kolicina = artikl.Artiklsalon.FirstOrDefault(k => k.Artikl.IdArtikl == artikl.IdArtikl && k.Salon.IdSalon == AktivniSalon.IdAktivniSalon).Kolicina,
                Dobavljac = new DobavljacVM()
                {
                    Iddobavljac = artikl.Dobavljac.Iddobavljac,
                    Naziv = artikl.Dobavljac.NazivDobavljaca,
                    Kontakt = artikl.Dobavljac.Kontakt,
                    Oib = artikl.Dobavljac.Oib,
                    Idadresa = artikl.Dobavljac.Adresa.IdAdresa,
                    Adresa =  new AdresaVM()
                    {
                        IdAdresa = artikl.Dobavljac.Adresa.IdAdresa,
                        Naziv = artikl.Dobavljac.Adresa.Nazivadrese,
                        IdGrad = artikl.Dobavljac.Adresa.Grad.IdGrad,
                        Grad = new GradVM()
                        {
                            IdGrad = artikl.Dobavljac.Adresa.Grad.IdGrad,
                            Naziv = artikl.Dobavljac.Adresa.Grad.NazivGrada,
                            Pbr = artikl.Dobavljac.Adresa.Grad.Pbr
                        }
                    }
                },
                Kategorija = new KategorijaVM()
                {
                    IdKategorija = artikl.Kategorija.IdKategorija,
                    Naziv = artikl.Kategorija.NazivKategorija
                },
                Proizvodac =  new ProizvodacVM()
                {
                    IdProizvodac = artikl.Proizvodac.IdProizvodac,
                    Naziv =artikl.Proizvodac.Nazivproizvodaca
                }

            };
            return View(artik);
        }

        // GET: artikls/Create
        public ActionResult Create()
        {
            ArtikliVM artikli = new ArtikliVM();
            bool postoji;
            foreach (var x in session.Query<Artikl>().ToList())
            {
                postoji = false;
                foreach (var y in session.Query<Artiklsalon>())
                {
                    if (y.Artikl.IdArtikl == x.IdArtikl && y.Salon.IdSalon== AktivniSalon.IdAktivniSalon)
                    {
                        postoji = true;
                    }
                }

                if (!postoji)
                {
                    artikli.Arts.Add(x);
                    artikli.Kols.Add(0);
                    artikli.Dict.Add(x.IdArtikl, 0);
                }
            }

            return View(artikli);
        }

        // POST: artikls/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ArtikliVM artikli)
        {

            if (ModelState.IsValid)
            {

                for (int i = 0; i < artikli.Arts.Count; i++)
                {
                    if (artikli.Kols[i] != 0)
                    {
                        Artiklsalon a = new Artiklsalon
                        {
                            Artikl = artikli.Arts[i],
                            Salon = session.Get<Salon>(AktivniSalon.IdAktivniSalon),
                            //artikl = artikli.Arts[i],
                            //salon = db.salon.Find(AktivniSalon.IdAktivniSalon),
                            Kolicina = artikli.Kols[i]

                        };
                        session.Save(a);
                        System.Diagnostics.Debug.WriteLine("dodan: " + a.Artikl.IdArtikl.ToString() + " " + a.Salon.IdSalon.ToString());
                    }
                }

                using (ITransaction transaction = session.BeginTransaction())   //  Begin a transaction
                {
                    
                    transaction.Commit();   //  Commit the changes to the database
                }
                return RedirectToAction("Index");
            }


            return View(artikli);
        }

        // GET: artikls/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Artikl artikl = session.Get<Artikl>(id);
            if (artikl == null)
            {
                return HttpNotFound();
            }
            var dobavljac = session.Query<Dobavljac>().AsEnumerable();
            var kategorija = session.Query<Kategorija>().AsEnumerable();
            var proizvodac= session.Query<Proizvodac>().AsEnumerable();
            ViewBag.idDobavljac = new SelectList(dobavljac, "iddobavljac", "naziv", artikl.Dobavljac.Iddobavljac);
            ViewBag.idKategorija = new SelectList(kategorija, "idKategorija", "nazivKategorija", artikl.Kategorija.IdKategorija);
            ViewBag.idProizvodac = new SelectList(proizvodac, "idProizvodac", "naziv", artikl.Proizvodac.IdProizvodac);
            return View(artikl);
        }

        // POST: artikls/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idArtikl,naziv,cijena,idDobavljac,idProizvodac,idKategorija")] Artikl artikl)
        {
            if (ModelState.IsValid)
            {
                using (ITransaction transaction = session.BeginTransaction())   //  Begin a transaction
                {
                    session.Save(artikl);
                    transaction.Commit();   //  Commit the changes to the database
                }
                return RedirectToAction("Index");
            }
            var dobavljac = session.Query<Dobavljac>().AsEnumerable();
            var kategorija = session.Query<Kategorija>().AsEnumerable();
            var proizvodac = session.Query<Proizvodac>().AsEnumerable();
            ViewBag.idDobavljac = new SelectList(dobavljac, "iddobavljac", "naziv", artikl.Dobavljac.Iddobavljac);
            ViewBag.idKategorija = new SelectList(kategorija, "idKategorija", "nazivKategorija", artikl.Kategorija.IdKategorija);
            ViewBag.idProizvodac = new SelectList(proizvodac, "idProizvodac", "naziv", artikl.Proizvodac.IdProizvodac);
            return View(artikl);
        }

        // GET: artikls/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Artikl artikl = session.Query<Artikl>().FirstOrDefault(a => a.IdArtikl == id);
            if (artikl == null)
            {
                return HttpNotFound();
            }
            return View(artikl);
        }

        // POST: artikls/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Artiklsalon a = session.Query<Artiklsalon>().FirstOrDefault(x => x.Artikl.IdArtikl == id && x.Salon.IdSalon == AktivniSalon.IdAktivniSalon);
            using (ITransaction transaction = session.BeginTransaction())   //  Begin a transaction
            {
                session.Delete(a); //  Save the book in session
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