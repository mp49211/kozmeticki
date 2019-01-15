using KozmetickiClassLibrary.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KozmetickiClassLibrary.Model;
namespace KozmetickiClassLibrary.ViewModels
{
    public class ArtiklSalonVM
    {
        public ArtiklSalonVM() { }
        public int IdArtiklSalon { get; set; }
        public int Kolicina { get; set; }
        public int IdArtikl { get; set; }
        public int IdSalon { get; set; }

        public ArtiklVM Artikl { get; set; }
        public SalonVM Salon { get; set; }
    }
}
