using System;
using System.Text;
using System.Collections.Generic;


namespace KozmetickiClassLibrary.Model {
    
    public class Artikl {
        public Artikl() { }
        public virtual int IdArtikl { get; set; }
        public virtual Dobavljac Dobavljac { get; set; }
        public virtual Proizvodac Proizvodac { get; set; }
        public virtual Kategorija Kategorija { get; set; }
        public virtual string Naziv { get; set; }
        public virtual decimal Cijena { get; set; }
        public virtual IList<Artiklsalon> Artiklsalon { get; set; }
        public virtual IList<Nabavaartikl> Nabavaartikl { get; set; }
    }
}
