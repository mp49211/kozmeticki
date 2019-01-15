using System;
using System.Text;
using System.Collections.Generic;


namespace KozmetickiClassLibrary.Model {
    
    public class Dobavljac {
        public Dobavljac() { }
        public virtual int Iddobavljac { get; set; }
        public virtual Adresa Adresa { get; set; }
        public virtual string NazivDobavljaca { get; set; }
        public virtual string Oib { get; set; }
        public virtual string Kontakt { get; set; }

        public virtual IList<Artikl> Artikl { get; set; }
        public virtual IList<Nabava> Nabava { get; set; }
    }
}
