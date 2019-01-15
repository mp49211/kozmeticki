using System;
using System.Text;
using System.Collections.Generic;


namespace KozmetickiClassLibrary.Model {
    
    public class Nabavaartikl {
        public virtual int IdNabavaArtikl { get; set; }
        public virtual Nabava Nabava { get; set; }
        public virtual Artikl Artikl { get; set; }
        public virtual int Kolicina { get; set; }
    }
}
