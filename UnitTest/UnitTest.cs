using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using KozmetickiClassLibrary.Model;
using System.Collections.Generic;

namespace UnitTest
{
    [TestClass]
    public class UnitTest
    {
        [TestMethod]
        public void ZaposlenikDoesOverlapTrue()
        {
            TimeSpan start = new TimeSpan(5, 0, 0);
            TimeSpan end = new TimeSpan(6, 30, 0);
            TimeSpan start2 = new TimeSpan(5, 30, 0);
            TimeSpan end2 = new TimeSpan(7, 30, 0);

            bool zauzet=Zaposlenik.DoesOverlap(start, end, start2, end2);
            Assert.IsTrue(zauzet);
        }
        [TestMethod]
        public void ZaposlenikDoesOverlapFalse()
        {
            TimeSpan start = new TimeSpan(5, 0, 0);
            TimeSpan end = new TimeSpan(6, 30, 0);
            TimeSpan start2 = new TimeSpan(6, 40, 0);
            TimeSpan end2 = new TimeSpan(7, 30, 0);

            bool zauzet = Zaposlenik.DoesOverlap(start, end, start2, end2);
            Assert.IsFalse(zauzet);
        }
        [TestMethod]
        public void NabavaIzracunajUkupnuCijenu()
        {
            List<Tuple<decimal, int>> list = new List<Tuple<decimal, int>>();
            list.Add(Tuple.Create(Convert.ToDecimal(20.5), 2));
            list.Add(Tuple.Create(Convert.ToDecimal(5.5), 2));
            list.Add(Tuple.Create(Convert.ToDecimal(3.3), 2));
            Nabava nabava = new Nabava();
            nabava.IzracunajUkupnuCijenu(list);
            Assert.AreEqual(Convert.ToDecimal(58.6), nabava.UkupnaCijena);
        }

        [TestMethod]
        public void ZaposlenikProvjeriZauzetostZauzet()
        {
            List<Narudzba> narudzbe = new List<Narudzba>();
            Narudzba nar = new Narudzba();
            Usluga usluga = new Usluga();

            usluga.Trajanje = 60; // trajanje postojece usluge

            nar.Vrijeme = new DateTime(2019, 1, 1, 5, 30, 0); //vrijeme postojece narudzbe
            nar.Usluga = usluga;
            narudzbe.Add(nar);
            TimeSpan start = new TimeSpan(6, 0, 0); //vrijeme NOVE narudzbe
            int trajanjeNove = 30; //trajanje NOVE narudzbe

            bool zauzet= Zaposlenik.provjeriZauzetost(narudzbe, start, trajanjeNove);

            //metoda vraca TRUE ako je korisnik zeuzet u traženom terminu
            Assert.IsTrue(zauzet);
        }

        [TestMethod]
        public void ZaposlenikProvjeriZauzetostSlobodan()
        {
            List<Narudzba> narudzbe = new List<Narudzba>();
            Narudzba nar = new Narudzba();
            Usluga usluga = new Usluga();

            usluga.Trajanje = 60; //trajanje postojece usluge

            nar.Vrijeme = new DateTime(2019, 1, 1, 5, 30, 0); //vrijeme postojece narudzbe
            nar.Usluga = usluga;
            narudzbe.Add(nar);
            TimeSpan start = new TimeSpan(7, 0, 0); //vrijeme NOVE narudzbe
            int trajanjeNove = 30; //trajanje NOVE narudzbe

            //metoda vraca FALSE ako je korisnik slobodan u traženom terminu
            bool zauzet = Zaposlenik.provjeriZauzetost(narudzbe, start, trajanjeNove);

            Assert.IsFalse(zauzet);
        }

        [TestMethod]
        public void ZaposlenikgetZaposlenikProfitByMonth()
        {
            List<Narudzba> narudzbe = new List<Narudzba>();

            Narudzba nar = new Narudzba();
            Usluga usluga = new Usluga();
            nar.Vrijeme = new DateTime(2019, 2, 2, 17, 10, 0);
            usluga.Cijena = 100;
            nar.Usluga = usluga;

            Narudzba nar1 = new Narudzba();
            Usluga usluga1 = new Usluga();
            nar1.Vrijeme = new DateTime(2019, 2, 5, 18, 0, 0);
            usluga1.Cijena = 300;
            nar1.Usluga = usluga1;
            
            narudzbe.Add(nar);
            narudzbe.Add(nar1);
            decimal profit = Zaposlenik.getZaposlenikProfitByMonth(narudzbe, 2, 2019);
            Assert.AreEqual(400, profit);
        }

        [TestMethod]
        public void ZaposlenikgetZaposlenikProfitByMonth2()
        {
            List<Narudzba> narudzbe = new List<Narudzba>();

            Narudzba nar = new Narudzba();
            Usluga usluga = new Usluga();
            nar.Vrijeme = new DateTime(2019, 2, 2, 17, 10, 0);
            usluga.Cijena = 100;
            nar.Usluga = usluga;

            Narudzba nar1 = new Narudzba();
            Usluga usluga1 = new Usluga();
            nar1.Vrijeme = new DateTime(2019, 2, 5, 18, 0, 0);
            usluga1.Cijena = 300;
            nar1.Usluga = usluga1;

            Narudzba nar2 = new Narudzba();
            Usluga usluga2 = new Usluga();
            nar2.Vrijeme = new DateTime(2019, 5, 5, 18, 0, 0);
            usluga2.Cijena = 300;
            nar2.Usluga = usluga1;

            narudzbe.Add(nar);
            narudzbe.Add(nar1);
            narudzbe.Add(nar2);
            decimal profit = Zaposlenik.getZaposlenikProfitByMonth(narudzbe, 2, 2019);
            Assert.AreNotEqual(700, profit);
        }


    }
}
