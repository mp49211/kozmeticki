using System;
using System.Collections.Generic;
using System.Text;

namespace KozmetickiClassLibrary.ViewModels
{
    public class UslugaVM
    {
        public UslugaVM()
        {
            Saloni = new List<SalonVM>();
            Zaposlenici = new List<ZaposlenikVM>();
        }

        public int Idusluga { get; set; }
        public string Naziv { get; set; }
        public decimal Cijena { get; set; }
        public int Trajanje { get; set; }
        public string Opis { get; set; }
        public int IdKategorija { get; set; }
        public bool Odabrana { get; set; }

        public KategorijaVM Kategorija { get; set; }
        public List<SalonVM> Saloni { get; set; }
        public List<ZaposlenikVM> Zaposlenici { get; set; }
    }
}
