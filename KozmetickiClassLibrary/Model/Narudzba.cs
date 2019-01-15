using System;
using System.Text;
using System.Collections.Generic;


namespace KozmetickiClassLibrary.Model {
    
    public class Narudzba {
        public virtual int IdNarudzba { get; set; }
        public virtual Zaposlenik Zaposlenik { get; set; }
        public virtual Salon Salon { get; set; }
        public virtual Usluga Usluga { get; set; }
        public virtual string Klijent { get; set; }
        public virtual string Kontakt { get; set; }
        public virtual DateTime Vrijeme { get; set; }

    }
}
