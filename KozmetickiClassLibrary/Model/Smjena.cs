using System;
using System.Text;
using System.Collections.Generic;


namespace KozmetickiClassLibrary.Model {
    
    public class Smjena {
        public Smjena() { }
        public virtual int IdSmjena { get; set; }
        public virtual string SmjenaVal { get; set; }
        public virtual string TimeStart { get; set; }
        public virtual string TimeEnd { get; set; }
        public virtual IList<Zaposlenik> Zaposlenik { get; set; }
    }
}
