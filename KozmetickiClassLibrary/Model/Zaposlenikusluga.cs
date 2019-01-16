using System;
using System.Text;
using System.Collections.Generic;


namespace KozmetickiClassLibrary.Model {
    
    public class Zaposlenikusluga {
        public Zaposlenikusluga() { }
        public virtual int IdZaposlenikUsluga { get; set; }
        public virtual Zaposlenik Zaposlenik { get; set; }
        public virtual Usluga Usluga { get; set; }
    }
}
