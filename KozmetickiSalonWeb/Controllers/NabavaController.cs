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
    public class NabavaController : Controller
    {
        ISession session = NHibertnateSession.OpenSession();
        // GET: Nabava
        public ActionResult Index()
        {

            var nabava = session.Query<Nabava>().Where(n => n.Salon.IdSalon == AktivniSalon.IdAktivniSalon);
            return View(nabava.ToList());
        }

        // GET: Nabava/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var nabava = session.Query<Nabavaartikl>().Where(na => na.Nabava.Idnabava == id).Select(na => new NabavaArtiklVM()
            {

                IdANabavaArtikl = na.IdNabavaArtikl,
                IdArtikl = na.Artikl.IdArtikl,
                IdNabva = na.Nabava.Idnabava,
                Kolicina = na.Kolicina,
                Artikl = new ArtiklVM()
                {
                    Cijena = na.Artikl.Cijena,
                    IdArtikl = na.Artikl.IdArtikl,
                    IdKategorija = na.Artikl.Kategorija.IdKategorija,
                    IdDobavljac = na.Artikl.Dobavljac.Iddobavljac,
                    IdProizvodac = na.Artikl.Proizvodac.IdProizvodac,
                    Naziv = na.Artikl.Naziv,
                    //UkCijena=a.cijena*na.kolicina,
                    Proizvodac = new ProizvodacVM()
                    {
                        IdProizvodac = na.Artikl.Proizvodac.IdProizvodac,
                        Naziv = na.Artikl.Proizvodac.Nazivproizvodaca

                    },

                    Dobavljac = new DobavljacVM()
                    {
                        Iddobavljac = na.Artikl.Dobavljac.Iddobavljac,
                        Naziv = na.Artikl.Dobavljac.NazivDobavljaca
                    },

                    Kategorija =  new KategorijaVM()
                    {

                        IdKategorija = na.Artikl.Kategorija.IdKategorija,
                        Naziv = na.Artikl.Kategorija.NazivKategorija

                    }



                },

                Nabava = new NabavaVM()
                {

                    Idnabava = na.Nabava.Idnabava,
                    Datum = na.Nabava.Datum,
                    UkupnaCijena = na.Nabava.UkupnaCijena

                }



            }).ToList();

            if (nabava == null)
            {
                return HttpNotFound();
            }
            return View(nabava.ToList());
        }

        // GET: Nabava/Create
        public ActionResult Create()
        {
            ArtikliVM artikli = new ArtikliVM();

            foreach (var x in session.Query<Artikl>().ToList())
            {

                artikli.Arts.Add(x);
                artikli.Kols.Add(0);
                artikli.Dict.Add(x.IdArtikl, 0);

            }

            return View(artikli);

        }

        // POST: Nabava/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ArtikliVM artikli)
        {
            if (ModelState.IsValid)
            {
                DateTime dateTime = DateTime.UtcNow.Date;
                var dob = session.Query<Dobavljac>().Select(a => a.Iddobavljac).Distinct().ToList();

                foreach (var d in dob)
                {
                    var map = new Dictionary<int, int>();
                    var novaNabava = new Nabava();
                    novaNabava.Salon= session.Get<Salon>(AktivniSalon.IdAktivniSalon); 
                    decimal sum = 0;
                    int id = 0;
                    for (int i = 0; i < artikli.Arts.Count; ++i)
                    {


                        if (artikli.Arts[i].Dobavljac.Iddobavljac == d)
                        {
                            if (artikli.Kols[i] != 0)
                            {
                                
                                novaNabava.Dobavljac = artikli.Arts[i].Dobavljac;
                                map.Add(artikli.Arts[i].IdArtikl, artikli.Kols[i]);
                                sum += artikli.Arts[i].Cijena * artikli.Kols[i];
                            }
                        }


                    }
                    if (id != 0)
                    {
                        novaNabava.Datum = dateTime;
                        novaNabava.UkupnaCijena = sum;
                        using (ITransaction transaction = session.BeginTransaction())   //  Begin a transaction
                        {
                            session.Save(novaNabava); //  Save the book in session
                            transaction.Commit();   //  Commit the changes to the database
                        }
                        var idnab = novaNabava;

                        foreach (var pair in map)
                        {
                            int key = pair.Key;
                            int value = pair.Value;

                            var artnab = new Nabavaartikl();
                            artnab.Nabava = idnab;
                            Artikl artikl = session.Get<Artikl>(key);
                            artnab.Artikl = artikl;
                            artnab.Kolicina = value;
                            using (ITransaction transaction = session.BeginTransaction())   //  Begin a transaction
                            {
                                session.Save(artnab); //  Save the book in session
                                transaction.Commit();   //  Commit the changes to the database
                            }

                        }
                    }



                }
                return RedirectToAction("Index");
            }
            return View(artikli);

        }

        // GET: Nabava/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Nabava nabava= session.Get<Nabava>(id);
            if (nabava == null)
            {
                return HttpNotFound();
            }
            var salon = session.Query<Salon>().AsEnumerable();
            var dobavljac= session.Query<Dobavljac>().AsEnumerable();
            ViewBag.idsalon = new SelectList(salon, "idSalon", "naziv", nabava.Salon.IdSalon);
            ViewBag.iddobavljac = new SelectList(dobavljac, "iddobavljac", "nazivDobavljaca", nabava.Dobavljac.Iddobavljac);
            return View(nabava);
        }

        // POST: Nabava/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idnabava,idsalon,iddobavljac,datum,ukupnaCijena")] Nabava nabava)
        {
            if (ModelState.IsValid)
            {
                using (ITransaction transaction = session.BeginTransaction())   //  Begin a transaction
                {
                    session.Save(nabava); //  Save the book in session
                    transaction.Commit();   //  Commit the changes to the database
                }

                return RedirectToAction("Index");
            }
            var salon = session.Query<Salon>().AsEnumerable();
            var dobavljac = session.Query<Dobavljac>().AsEnumerable();
            ViewBag.idsalon = new SelectList(salon, "idSalon", "naziv", nabava.Salon.IdSalon);
            ViewBag.iddobavljac = new SelectList(dobavljac, "iddobavljac", "nazivDobavljaca", nabava.Dobavljac.Iddobavljac);
            return View(nabava);
        }

        // GET: Nabava/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Nabava nabava = session.Get<Nabava>(id);
            if (nabava == null)
            {
                return HttpNotFound();
            }
            return View(nabava);
        }

        // POST: Nabava/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Nabava nabava = session.Get<Nabava>(id);
            using (ITransaction transaction = session.BeginTransaction())   //  Begin a transaction
            {
                session.Delete(nabava); //  Save the book in session
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