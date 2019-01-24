using KozmetickiClassLibrary.Interfaces;
using KozmetickiClassLibrary.Model;
using KozmetickiClassLibrary.ViewModels;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace KozmetickiSalonWeb.Controllers
{
    public class NarudzbasController : Controller
    {
        private IRepository narudzbaRepository;

        public NarudzbasController()
        {
            this.narudzbaRepository = new Repository();
        }
        public ActionResult Index()
        {
            NarudzbeVM nar = new NarudzbeVM();
            nar.datum = DateTime.Today;
            var narudzba = narudzbaRepository.GetNarudzba().Where(x => x.Salon.IdSalon == AktivniSalon.IdAktivniSalon);
            List<NarudzbaVM> narudzbe = narudzba.Select(x => new NarudzbaVM()
            {
                IdNarudzba = x.IdNarudzba,
                Klijent = x.Klijent,
                IdUsluga = x.Usluga.Idusluga,
                IdZaposlenik = x.Zaposlenik.IdZaposlenik,
                Kontakt = x.Kontakt,
                Vrijeme = x.Vrijeme,
                IdSalon = x.Salon.IdSalon
            }).OrderBy(x => x.Vrijeme).ToList();

            narudzbe.RemoveAll(x => x.Vrijeme.Date.CompareTo(nar.datum.Date) != 0);

            foreach (var n in narudzbe)
            {
                n.Zaposlenik = narudzbaRepository.GetZaposleniksQuery().Select(x => new ZaposlenikVM()
                {
                    IdZaposlenik = x.IdZaposlenik,
                    ImePrezime = x.Ime + " " + x.Prezime

                }).FirstOrDefault(x => x.IdZaposlenik == n.IdZaposlenik);

                n.Usluga = narudzbaRepository.GetUsluga().Select(x => new UslugaVM()
                {
                    Idusluga = x.Idusluga,
                    Naziv = x.Naziv,
                    Cijena = x.Cijena,
                    Trajanje = x.Trajanje

                }).FirstOrDefault(x => x.Idusluga == n.IdUsluga);
            }
            nar.narudzbe = narudzbe;
            return View(nar);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(NarudzbeVM nar)
        {
            if (ModelState.IsValid)
            {
                List<NarudzbaVM> narudzbe = narudzbaRepository.GetNarudzba()
                    .Where(n => n.Salon.IdSalon == AktivniSalon.IdAktivniSalon)
                    .Select(n => new NarudzbaVM()
                    {
                        IdNarudzba = n.IdNarudzba,
                        Klijent = n.Klijent,
                        IdUsluga = n.Usluga.Idusluga,
                        IdZaposlenik = n.Zaposlenik.IdZaposlenik,
                        Kontakt = n.Kontakt,
                        IdSalon = n.Salon.IdSalon,
                        Vrijeme = n.Vrijeme
                    }).ToList();

                narudzbe.RemoveAll(x => x.Vrijeme.Date.CompareTo(nar.datum.Date) != 0);

                foreach (var n in narudzbe)
                {
                    n.Zaposlenik = narudzbaRepository.GetZaposleniksQuery().Select(x => new ZaposlenikVM()
                    {
                        IdZaposlenik = x.IdZaposlenik,
                        ImePrezime = x.Ime + " " + x.Prezime

                    }).FirstOrDefault(x => x.IdZaposlenik == n.IdZaposlenik);

                    n.Usluga = narudzbaRepository.GetUsluga().Select(x => new UslugaVM()
                    {
                        Idusluga = x.Idusluga,
                        Naziv = x.Naziv,
                        Cijena = x.Cijena,
                        Trajanje = x.Trajanje

                    }).FirstOrDefault(x => x.Idusluga == n.IdUsluga);
                }
                nar.narudzbe = narudzbe;
                return View(nar);
            }
            return View(nar);

        }

        // GET: Narudzbas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Narudzba narudzba = narudzbaRepository.GetNarudzbaByID(id);
            if (narudzba == null)
            {
                return HttpNotFound();
            }
            return View(narudzba);
        }

        // GET: Narudzbas/Create
        public ActionResult Create()
        {
            var narudzba = new NarudzbaVM
            {
                IdSalon = AktivniSalon.IdAktivniSalon,
                Datum = DateTime.Today

            };

            var usluge = narudzbaRepository.GetSalonUsluga().Where(x => x.Salon.IdSalon == AktivniSalon.IdAktivniSalon)
                .Select(x => new UslugaVM()
                {
                    Idusluga = x.Usluga.Idusluga,
                    Naziv = x.Usluga.Naziv
                }).ToList();
            ViewBag.idUsluga = new SelectList(usluge, "Idusluga", "Naziv");
            ViewBag.poruka = "";
            return View(narudzba);
        }

        // POST: Narudzbas/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(NarudzbaVM narudzba)
        {
            if (ModelState.IsValid)
            {

                narudzba.Usluga = narudzbaRepository.GetUsluga().Select(x => new UslugaVM()
                {
                    Idusluga = x.Idusluga,
                    Naziv = x.Naziv,
                    Trajanje = x.Trajanje,
                    Cijena = x.Cijena
                }).FirstOrDefault(x => x.Idusluga == narudzba.IdUsluga);

                //...

                List<ZaposlenikVM> zaposlenici = new List<ZaposlenikVM>();
                foreach (var z in narudzbaRepository.GetZaposleniksQuery())
                {
                    if (z.Salon.IdSalon == AktivniSalon.IdAktivniSalon)
                    {
                        foreach (var u in z.Zaposlenikusluga)
                        {
                            if (u.Usluga.Idusluga == narudzba.IdUsluga)
                            {
                                zaposlenici.Add(new ZaposlenikVM()
                                {
                                    IdZaposlenik = z.IdZaposlenik,
                                    ImePrezime = z.Ime + ' ' + z.Prezime,
                                    IdSmjena = z.Smjena.IdSmjena
                                });
                            }
                        }
                    }
                }

                List<Narudzba> narudzbe = narudzbaRepository.GetNarudzba().Where(x => x.Salon.IdSalon == AktivniSalon.IdAktivniSalon).ToList();

                narudzbe.RemoveAll(x => x.Vrijeme.Date.CompareTo(narudzba.Datum.Date) != 0);
                Dictionary<int, List<DateTime>> vremena = new Dictionary<int, List<DateTime>>();


                bool moguce;
                TimeSpan ts;
                DateTime start;
                DateTime end;

                foreach (var z in zaposlenici)
                {
                    vremena[z.IdZaposlenik] = new List<DateTime>();
                    if (z.IdSmjena == 1)
                    {
                        ts = new TimeSpan(8, 0, 0);
                        start = narudzba.Datum.Date + ts;
                        end = start.AddMinutes(6 * 60 - narudzba.Usluga.Trajanje);
                    }
                    else
                    {
                        ts = new TimeSpan(14, 0, 0);
                        start = narudzba.Datum.Date + ts;
                        end = start.AddMinutes(6 * 60 - narudzba.Usluga.Trajanje);
                    }


                    while (start <= end)
                    {
                        DateTime poc = start;
                        DateTime kraj = start.AddMinutes(narudzba.Usluga.Trajanje);

                        moguce = true;

                        foreach (var n in narudzbe)
                        {
                            if (n.Zaposlenik.IdZaposlenik == z.IdZaposlenik)
                            {
                                DateTime s = n.Vrijeme;
                                DateTime e = n.Vrijeme.AddMinutes(n.Usluga.Trajanje);

                                if (!(DateTime.Compare(poc, s) < 0 && DateTime.Compare(kraj, s) <= 0 || DateTime.Compare(poc, e) >= 0 && DateTime.Compare(kraj, e) > 0))
                                {
                                    moguce = false;
                                }

                            }

                        }

                        if (moguce == true)
                        {
                            vremena[z.IdZaposlenik].Add(poc);
                        }


                        start = start.AddMinutes(30);
                    }


                }
                narudzba.Vremena = new List<ZaposlenikVrijemeVM>();

                int i = 1;
                foreach (var x in vremena.Keys)
                {
                    Zaposlenik zapo = narudzbaRepository.GetZaposleniksQuery().FirstOrDefault(z => z.IdZaposlenik == x);
                    ZaposlenikVM zap = new ZaposlenikVM()
                    {
                        IdZaposlenik = zapo.IdZaposlenik,
                        ImePrezime = zapo.Ime + " " + zapo.Prezime

                    };

                    foreach (var y in vremena[x])
                    {

                        narudzba.Vremena.Add(new ZaposlenikVrijemeVM()
                        {
                            Id = i,
                            Vrijeme = y,
                            IdZaposlenik = zap.IdZaposlenik,
                            Prikaz = y.ToShortTimeString() + " (" + zap.ImePrezime + ")"
                        });
                        i++;
                    }
                }

                narudzba.Vremena = narudzba.Vremena.OrderBy(x => x.Vrijeme).ToList();
                TempData["nar"] = narudzba;
                if(narudzba.Vremena.Count > 0)
                {
                    return RedirectToAction("Create2");
                }
                else
                {
                    ViewBag.poruka = "ZA ODABRANI DATUM NEMA SLOBODNIH TERMINA. ODABERI DRUGI!";
                }
                


            }

            var usluge = narudzbaRepository.GetSalonUsluga().Where(x => x.Salon.IdSalon == AktivniSalon.IdAktivniSalon)
                 .Select(x => new UslugaVM()
                 {
                     Idusluga = x.Usluga.Idusluga,
                     Naziv = x.Usluga.Naziv
                 }).ToList();
            ViewBag.idUsluga = new SelectList(usluge, "Idusluga", "Naziv");
            
            return View(narudzba);
        }

        [HttpGet]
        public ActionResult Create2()
        {
            NarudzbaVM narudzba = (NarudzbaVM)TempData["nar"];
            ViewBag.idVrijeme = new SelectList(narudzba.Vremena, "Id", "Prikaz");
            return View(narudzba);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Create2")]
        public ActionResult Create2(NarudzbaVM narudzba)
        {
            ZaposlenikVrijemeVM zv = narudzba.Vremena.FirstOrDefault(x => x.Id == narudzba.IdVrijeme);
            var salon= narudzbaRepository.GetSalonByID(AktivniSalon.IdAktivniSalon);
            var usluga = narudzbaRepository.GetUslugaByID(narudzba.IdUsluga);
            var zaposlenik = narudzbaRepository.GetZaposlenikByID(zv.IdZaposlenik);

            Narudzba nar = new Narudzba()
            {
                Salon = salon,
                Usluga= usluga,
                Zaposlenik = zaposlenik,
                Klijent = narudzba.Klijent,
                Kontakt = narudzba.Kontakt,
                Vrijeme = zv.Vrijeme

            };

            narudzbaRepository.InsertNarudzba(nar);
        
            return RedirectToAction("Index", "Pocetna");
        }

        // GET: Narudzbas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Narudzba narudzba = narudzbaRepository.GetNarudzbaByID(id);
            if (narudzba == null)
            {
                return HttpNotFound();
            }
            var salon = narudzbaRepository.GetSalon().AsEnumerable();
            var zaposlenik = narudzbaRepository.GetZaposleniksQuery().AsEnumerable();
            var usluga = narudzbaRepository.GetUsluga().AsEnumerable();
            ViewBag.idSalon = new SelectList(salon, "idSalon", "naziv", narudzba.IdNarudzba);
            ViewBag.idZaposlenik = new SelectList(zaposlenik, "idZaposlenik", "ime", narudzba.Zaposlenik.IdZaposlenik);
            ViewBag.idUsluga = new SelectList(usluga, "idusluga", "naziv", narudzba.Usluga.Idusluga);
            return View(narudzba);
        }

        // POST: Narudzbas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idNarudzba,klijent,kontakt,idZaposlenik,idSalon,vrijeme,idUsluga")] Narudzba narudzba)
        {
            if (ModelState.IsValid)
            {
                narudzbaRepository.InsertNarudzba(narudzba);
              
                return RedirectToAction("Index");
            }
            var salon = narudzbaRepository.GetSalon().AsEnumerable();
            var zaposlenik = narudzbaRepository.GetZaposleniksQuery().AsEnumerable();
            var usluga = narudzbaRepository.GetUsluga().AsEnumerable();
            ViewBag.idSalon = new SelectList(salon, "idSalon", "naziv", narudzba.IdNarudzba);
            ViewBag.idZaposlenik = new SelectList(zaposlenik, "idZaposlenik", "ime", narudzba.Zaposlenik.IdZaposlenik);
            ViewBag.idUsluga = new SelectList(usluga, "idusluga", "naziv", narudzba.Usluga.Idusluga);
            return View(narudzba);
        }

        // GET: Narudzbas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Narudzba narudzba = narudzbaRepository.GetNarudzbaByID(id);
            if (narudzba == null)
            {
                return HttpNotFound();
            }
            return View(narudzba);
        }

        // POST: Narudzbas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Narudzba narudzba = narudzbaRepository.GetNarudzbaByID(id);
            narudzbaRepository.DeleteNarudzba(narudzba);
         
            return RedirectToAction("Index");
        }

       
    }
}