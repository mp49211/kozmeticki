using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KozmetickiClassLibrary.ViewModels
{
    public class NarudzbeVM
    {
        public NarudzbeVM() { }

        public List<NarudzbaVM> narudzbe { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime datum { get; set; }
    }
}
