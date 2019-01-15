using KozmetickiClassLibrary.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace KozmetickiClassLibrary.ViewModels
{
    public class ZaposlenikVM
    {
        public ZaposlenikVM()
        {
            Narudzbe = new List<NarudzbaVM>();
            Usluge = new List<UslugaVM>();
        }

        public int IdZaposlenik { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public string ImePrezime { get; set; }
        public string Oib { get; set; }
        public int IdAdresa { get; set; }
        public int IdSalon { get; set; }
        public int IdSmjena { get; set; }

        public SmjenaVM Smjena { get; set; }
        public AdresaVM Adresa { get; set; }
        public SalonVM Salon { get; set; }
        public List<NarudzbaVM> Narudzbe { get; set; }
        public List<UslugaVM> Usluge { get; set; }
    }
}
