using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KozmetickiClassLibrary.Model;
namespace KozmetickiClassLibrary.ViewModels
{
    public class ZaposlenikAdresaVM
    {
        public ZaposlenikAdresaVM() {
        }
        public Zaposlenik zaposlenik { get; set; }
        public Adresa adresa { get; set; }
        public List<UslugaVM> usluge { get; set; }
    }
}
