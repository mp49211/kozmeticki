using System;
using System.Text;
using System.Collections.Generic;


namespace KozmetickiClassLibrary.Model {
    
    public class Artiklsalon {
        public virtual int IdArtiklSalon { get; set; }
        public virtual Artikl Artikl { get; set; }
        public virtual Salon Salon { get; set; }
        public virtual int Kolicina { get; set; }
    }
}
