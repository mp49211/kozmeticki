using System;
using System.Text;
using System.Collections.Generic;


namespace KozmetickiClassLibrary.Model {
    
    public class Nabava {
        public Nabava() { }
        public virtual int Idnabava { get; set; }
        public virtual Salon Salon { get; set; }
        public virtual Dobavljac Dobavljac { get; set; }
        public virtual DateTime Datum { get; set; }
        public virtual decimal UkupnaCijena { get; set; }
        public virtual IList<Nabavaartikl> Nabavaartikl { get; set; }
    }
}
