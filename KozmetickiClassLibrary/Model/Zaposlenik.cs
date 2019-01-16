using System;
using System.Text;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace KozmetickiClassLibrary.Model {
    
    public class Zaposlenik {
        public Zaposlenik()
        {
           
            this.Narudzba = new HashSet<Narudzba>();
        }

        public virtual int IdZaposlenik { get; set; }
        public virtual Adresa Adresa { get; set; }
        public virtual Salon Salon { get; set; }
        public virtual Smjena Smjena { get; set; }
        [Required(ErrorMessage = "Ime je obavezno polje.")]
        public virtual string Ime { get; set; }
        [Required(ErrorMessage = "Prezime je obavezno polje.")]
        public virtual string Prezime { get; set; }
        [Required(ErrorMessage = "OIB je obavezno polje.")]
        public virtual string Oib { get; set; }
        public virtual ICollection<Narudzba> Narudzba { get; set; }
        public virtual IList<Zaposlenikusluga> Zaposlenikusluga { get; set; }
    }
}
