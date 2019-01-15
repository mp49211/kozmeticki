using System;
using System.Collections.Generic;
using System.Text;
using KozmetickiClassLibrary.Model;
namespace KozmetickiClassLibrary.ViewModels
{
    public class DobavljacVM
    {
        public DobavljacVM()
        {
            Artikli = new List<ArtiklVM>();
        }

        public int Iddobavljac { get; set; }
        public string Naziv { get; set; }
        public string Oib { get; set; }
        public string Kontakt { get; set; }
        public int Idadresa { get; set; }

        public AdresaVM Adresa { get; set; }
        public List<ArtiklVM> Artikli { get; set; }
    }
}
