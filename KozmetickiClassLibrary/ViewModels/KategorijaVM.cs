using System;
using System.Collections.Generic;
using System.Text;
using KozmetickiClassLibrary.Model;
namespace KozmetickiClassLibrary.ViewModels
{
    public class KategorijaVM
    {
        public KategorijaVM()
        {
            Artikli = new List<ArtiklVM>();
            Usluge = new List<UslugaVM>();
        }

        public int IdKategorija { get; set; }
        public string Naziv { get; set; }

        public List<ArtiklVM> Artikli { get; set; }
        public List<UslugaVM> Usluge { get; set; }
    }
}
