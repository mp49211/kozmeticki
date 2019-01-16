using System;
using System.Text;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace KozmetickiClassLibrary.Model {

    public class Zaposlenik
    {
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

        //filtrirana lista(zaposlenik i dan), vrijeme(sati i minute) narudzbe, trajanje željene usluge
        public static Boolean provjeriZauzetost(IList<Narudzba> svenarudzbeZaposlenika, TimeSpan vrijemeNarudzbe, int trajanjeUsluge)
        {
            Boolean zauzet = false;
            try
            {
                TimeSpan zavrsetakMojeUsluge = vrijemeNarudzbe + TimeSpan.FromMinutes(trajanjeUsluge);
                foreach (var nar in svenarudzbeZaposlenika)
                {

                    TimeSpan vrijemePocetka = nar.Vrijeme.TimeOfDay;
                    int idU = nar.Usluga.Idusluga;
                    int duration = nar.Usluga.Trajanje;
                    TimeSpan vrijemeZavrsetka = vrijemePocetka + TimeSpan.FromMinutes(duration);
                    // MessageBox.Show(time + " " + zavrsetakMojeUsluge + " " + vrijemePocetka.ToString() + " " + vrijemeZavrsetka.ToString());

                    if (DoesOverlap(vrijemeNarudzbe, zavrsetakMojeUsluge, vrijemePocetka, vrijemeZavrsetka))
                    {
                        zauzet = true;
                        break;

                    }
                }

            }
            catch (FormatException exc)
            {

            }
            return zauzet;
        }
        public static bool DoesOverlap(TimeSpan start1, TimeSpan end1, TimeSpan start2, TimeSpan end2)
        {

            if (start1 > start2)
            {
                if (start1 >= end2)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else if (start1 < start2)
            {
                if (end1 <= start2)
                {
                    return false;
                }
                else
                {
                    return true;
                }

            }
            else
            {
                return true;
            }
        }
        public static decimal getZaposlenikProfitByMonth(ICollection<Narudzba> nar, int mjesec, int godina)
        {
            decimal profit = 0;
            foreach (var n in nar)
            {
                if (n.Vrijeme.Month == mjesec && n.Vrijeme.Year == godina)
                {
                    profit += n.Usluga.Cijena;
                }
            }
            return profit;
        }
    }
}
