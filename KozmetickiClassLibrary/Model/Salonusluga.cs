using System;
using System.Text;
using System.Collections.Generic;


namespace KozmetickiClassLibrary.Model {
    
    public class Salonusluga {
        public virtual int IdSalonUsluga { get; set; }
        public virtual Salon Salon { get; set; }
        public virtual Usluga Usluga { get; set; }
    }
}
