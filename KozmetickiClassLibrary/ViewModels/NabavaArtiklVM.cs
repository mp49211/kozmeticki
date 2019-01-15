using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KozmetickiClassLibrary.Model;
namespace KozmetickiClassLibrary.ViewModels
{
   public class NabavaArtiklVM
    {
        public NabavaArtiklVM() { }
        public int IdANabavaArtikl { get; set; }
        public int Kolicina { get; set; }
        public int IdArtikl { get; set; }
        public int IdNabva { get; set; }

        public ArtiklVM Artikl { get; set; }
        public NabavaVM Nabava { get; set; }
    }
}
