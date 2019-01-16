using System;
using System.Text;
using System.Collections.Generic;


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
        public virtual string Ime { get; set; }
        public virtual string Prezime { get; set; }
        public virtual string Oib { get; set; }
        public virtual ICollection<Narudzba> Narudzba { get; set; }
        public virtual IList<Zaposlenikusluga> Zaposlenikusluga { get; set; }

        public static decimal getZaposlenikProfitByMonth(ICollection<Narudzba> nar, int mjesec, int godina) {
            decimal profit = 0;
            foreach(var n in nar) {
                if (n.Vrijeme.Month == mjesec && n.Vrijeme.Year==godina) {
                    profit += n.Usluga.Cijena;

                }


            }
            return profit;

        }
    }
}
