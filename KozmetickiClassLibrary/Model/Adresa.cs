using System;
using System.Text;
using System.Collections.Generic;


namespace KozmetickiClassLibrary.Model {
    
    public class Adresa {
        public Adresa() { }
        public virtual int IdAdresa { get; set; }
        public virtual Grad Grad { get; set; }
        public virtual string Nazivadrese { get; set; }
        public virtual IList<Dobavljac> Dobavljac { get; set; }
        public virtual IList<Salon> Salon { get; set; }
        public virtual IList<Zaposlenik> Zaposlenik { get; set; }
    }
}
