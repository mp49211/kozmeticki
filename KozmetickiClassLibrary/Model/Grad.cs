using System;
using System.Text;
using System.Collections.Generic;


namespace KozmetickiClassLibrary.Model {
    
    public class Grad {
        public Grad() { }
        public virtual int IdGrad { get; set; }
        public virtual string NazivGrada { get; set; }
        public virtual int Pbr { get; set; }
        public virtual IList<Adresa> Adresa { get; set; }
    }
}
