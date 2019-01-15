using System;
using System.Text;
using System.Collections.Generic;


namespace KozmetickiClassLibrary.Model {
    
    public class Proizvodac {
        public Proizvodac() { }
        public virtual int IdProizvodac { get; set; }
        public virtual string Nazivproizvodaca { get; set; }
        public virtual IList<Artikl> Artikl { get; set; }
    }
}
