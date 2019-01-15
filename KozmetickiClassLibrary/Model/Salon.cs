using System;
using System.Text;
using System.Collections.Generic;


namespace KozmetickiClassLibrary.Model {
    
    public class Salon {
        public Salon() { }
        public virtual int IdSalon { get; set; }
        public virtual Adresa Adresa { get; set; }
        public virtual string Naziv { get; set; }
        public virtual string Oib { get; set; }
        public virtual string Username { get; set; }
        public virtual string Pwd { get; set; }
        public virtual IList<Artiklsalon> Artiklsalon { get; set; }
        public virtual IList<Nabava> Nabava { get; set; }
        public virtual IList<Salonusluga> Salonusluga { get; set; }
        public virtual IList<Zaposlenik> Zaposlenik { get; set; }
        public virtual IList<Narudzba> Narudzba { get; set; }
    }
}
