using System;
using System.Collections.Generic;
using System.Text;

namespace KozmetickiClassLibrary.ViewModels
{
    public class ProizvodacVM
    {
        public ProizvodacVM()
        {
            Artikli = new List<ArtiklVM>();
        }

        public int IdProizvodac { get; set; }
        public string Naziv { get; set; }

        public List<ArtiklVM> Artikli { get; set; }
    }
}
