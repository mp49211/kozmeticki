using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KozmetickiClassLibrary.ViewModels
{
    public class SmjenaVM
    {

        public SmjenaVM()
        {
        }

        public int IdSmjena { get; set; }
        public string Naziv { get; set; }
        public string TimeStart { get; set; }
        public string TimeEnd { get; set; }
        
    }
}
