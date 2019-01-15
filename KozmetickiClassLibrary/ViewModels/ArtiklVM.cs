using System;
using System.Collections.Generic;
using System.Text;
using KozmetickiClassLibrary.Model;
namespace KozmetickiClassLibrary.ViewModels
{
    public class ArtiklVM
    {
        public ArtiklVM()
        {
            
        }

        public int IdArtikl { get; set; }
        public string Naziv { get; set; }
        public decimal Cijena { get; set; }
        public int IdDobavljac { get; set; }
        public int IdProizvodac { get; set; }
        public int IdKategorija { get; set; }
        public int Kolicina { get; set; }
        public decimal UkCijena { get; set; }
        public DobavljacVM Dobavljac { get; set; }
        public KategorijaVM Kategorija { get; set; }
        public ProizvodacVM Proizvodac { get; set; }
       
    }
}
