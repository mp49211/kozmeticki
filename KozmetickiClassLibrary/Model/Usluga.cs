using System;
using System.Text;
using System.Collections.Generic;


namespace KozmetickiClassLibrary.Model {
    
    public class Usluga {
        public Usluga() { }
        public virtual int Idusluga { get; set; }
        public virtual Kategorija Kategorija { get; set; }
        public virtual string Naziv { get; set; }
        public virtual decimal Cijena { get; set; }
        public virtual int Trajanje { get; set; }
        public virtual string Opis { get; set; }

        public virtual IList<Salonusluga> Salonusluga { get; set; }
        public virtual IList<Zaposlenikusluga> Zaposlenikusluga { get; set; }
        public virtual IList<Narudzba> Narudzba { get; set; }
    }
}
