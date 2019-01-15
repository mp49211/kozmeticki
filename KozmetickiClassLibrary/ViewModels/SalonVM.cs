using System;
using System.Collections.Generic;
using System.Text;

namespace KozmetickiClassLibrary.ViewModels
{
    public class SalonVM
    {
        public SalonVM()
        {
            Artikli = new List<ArtiklVM>();
            Nabave = new List<NabavaVM>();
            Narudzbe = new List<NarudzbaVM>();
            Usluge = new List<UslugaVM>();
            Zaposlenici = new List<ZaposlenikVM>();
        }

        public int IdSalon { get; set; }
        public string Naziv { get; set; }
        public string Oib { get; set; }
        public int IdAdresa { get; set; }
        public string Username { get; set; }
        public string Pwd { get; set; }

        public AdresaVM Adresa { get; set; }
        public List<ArtiklVM> Artikli { get; set; }
        public List<NabavaVM> Nabave { get; set; }
        public List<NarudzbaVM> Narudzbe { get; set; }
        public List<UslugaVM> Usluge { get; set; }
        public List<ZaposlenikVM> Zaposlenici { get; set; }
    }
}
