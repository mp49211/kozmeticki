using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace KozmetickiClassLibrary.ViewModels
{
    public class NarudzbaVM
    {
        public NarudzbaVM()
        {
        }

        public int IdNarudzba { get; set; }
        [Required(ErrorMessage = "Potrebno je unijeti ime klijenta.")]
        public string Klijent { get; set; }
        [Required(ErrorMessage = "Potrebno je unijeti kontakt.")]
        public string Kontakt { get; set; }
        public int IdZaposlenik { get; set; }
        public int IdSalon { get; set; }
        public int IdUsluga { get; set; }
        public DateTime Vrijeme { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Datum { get; set; }


        public SalonVM Salon { get; set; }
        public ZaposlenikVM Zaposlenik { get; set; }
        public UslugaVM Usluga { get; set; }
        //public List<DateTime> Vremena { get; set; }
        //public List<ZaposlenikVM> Zaposlenici { get; set; }
        //public Dictionary<int, List<DateTime>> VremenaDict { get; set; }

        public List<ZaposlenikVrijemeVM> Vremena { get; set; }
        public int IdVrijeme { get; set; }
        
    }
}
