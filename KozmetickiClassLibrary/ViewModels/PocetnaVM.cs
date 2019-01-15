using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace KozmetickiClassLibrary.ViewModels
{
    public class PocetnaVM
    {
        public PocetnaVM() { }

        public List<ZaposlenikVM> Zaposlenici { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Datum { get; set; }
        public int Broj { get; set; }
    }
}
