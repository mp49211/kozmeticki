using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KozmetickiClassLibrary.ViewModels
{
    public class ZaposlenikVrijemeVM
    {

        public ZaposlenikVrijemeVM() { }

        public int Id { get; set; }
        public int IdZaposlenik { get; set; }
        public DateTime Vrijeme { get; set; }
        public string Prikaz { get; set; }
    }
}
