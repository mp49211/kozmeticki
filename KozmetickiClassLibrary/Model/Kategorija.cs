using System;
using System.Text;
using System.Collections.Generic;


namespace KozmetickiClassLibrary.Model {
    
    public class Kategorija {
        public Kategorija() { }
        public virtual int IdKategorija { get; set; }
        public virtual string NazivKategorija { get; set; }
        public virtual IList<Artikl> Artikl { get; set; }
        public virtual IList<Usluga> Usluga { get; set; }
    }
}
